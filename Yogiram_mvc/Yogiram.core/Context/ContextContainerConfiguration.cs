using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Routing;
using Yogiram.core.Models;

namespace Yogiram.core.Context
{
    public class ContextContainerConfiguration
    {
        private static Dictionary<int, WeakReference<HttpContextBase>> ThreadHttpContext = new Dictionary<int, WeakReference<HttpContextBase>>();

        private static ContextContainerConfiguration _Singleton;

        private String _ContextContainerAppSetting;

        public static ContextContainerConfiguration Get
        {
            get
            {
                if (_Singleton == null)
                    _Singleton = new ContextContainerConfiguration();

                return _Singleton;
            }
        }

        public virtual ContextContainer GetCurrentContext()
        {
            HttpContextBase Current = ContextContainerConfiguration.Get.Current;

            if (Current == null)
                return null;

            if (Current.Items.Contains(Common.ContextContainer))
            {
                var Context = Current.Items[Common.ContextContainer] as ContextContainer;
                return Context;
            }

            return CreateContext(Current.Request.RequestContext);
        }

        public virtual String ContextContainerAppSetting
        {
            get
            {
                if (_ContextContainerAppSetting == null)
                    _ContextContainerAppSetting = ConfigurationManager.AppSettings[Common.ContextContainer];
                return this._ContextContainerAppSetting;
            }
        }

        public virtual HttpContextBase Current
        {
            get
            {
                if (HttpContext.Current != null)
                    return new HttpContextWrapper(HttpContext.Current);

                if (ThreadHttpContext.ContainsKey(Thread.CurrentThread.ManagedThreadId))
                {
                    HttpContextBase Context;

                    if (ThreadHttpContext[Thread.CurrentThread.ManagedThreadId].TryGetTarget(out Context))
                        return Context;

                    ThreadHttpContext.Remove(Thread.CurrentThread.ManagedThreadId);
                }

                return null;
            }
        }

        internal void SetContextContainer(RequestContext RequestContext, ContextContainer Context)
        {
            if (!RequestContext.HttpContext.Items.Contains(Common.ContextContainer))
                RequestContext.HttpContext.Items.Add(Common.ContextContainer, Context);
        }

        internal ContextContainer CreateContext(RequestContext RequestContext)
        {
            var exports = ModulesConfig.CompositionContainer.GetExports<ContextContainer, ExportContextContainerView>();

            var appSettings = ContextContainerAppSetting;

            var type = (from a in exports where a.Metadata.Name == appSettings select a).FirstOrDefault();

            if (type == null || type.Value == null)
                throw new ApplicationException("ContextContainer: " + appSettings + " is invalid or implementation does not exist.");

            var Context = type.Value;

            SetContextContainer(RequestContext, Context);

            Context.RequestContext = RequestContext;
            Context.HttpContext = RequestContext.HttpContext;

            //Context.Init();

            return Context;
        }
    }


}
