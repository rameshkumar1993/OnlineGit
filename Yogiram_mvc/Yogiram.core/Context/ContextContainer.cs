using System;
using System.Collections.Generic;
using System.Data.EntityClient;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yogiram.core.DataSource;
using Yogiram.core.Models;
using System.Web;
using Yogiram.core.Service;
using Yogiram.core.Resources;
using Yogiram.core.Handler;
using System.Globalization;
using Yogiram.core.Mvc;
using System.Resources;

namespace Yogiram.core.Context
{
    public class ContextContainer
    {
        private CoreEntities _CoreDB;
        private int? _CompanyId { get; set; }
        private int? _UserId { get; set; }
        private string _UserName { get; set; }
        private string _EmployeeCode { get; set; }
        private Guid[] _MenuId { get; set; }
        private RoleService _RoleService;
        private MenuService _MenuService;
        private LoginService _LoginService;
        public TerminologyProcessor Terminology { get; private set; }
        
        public virtual CoreEntities CoreEntities
        {
            get
            {
                if (_CoreDB == null)
                {
                    _CoreDB = CoreEntities.Create();
                }

                return _CoreDB;
            }
        }

        public virtual String CoreConnectionKey
        {
            get
            {
                return Constant.ConnStringName;
            }
        }

        //public virtual int? UserId
        //{
        //    get
        //    {
        //        if (HttpContext.Current == null)
        //            return 0;
        //        var sessionId = HttpContext.Current.Session[session.UserId];
        //        if (sessionId != null)
        //            return sessionId as int?;

        //        return null;
        //    }
        //}

        public virtual int? CompanyId
        {
            get
            {
                if (_CompanyId == null && HttpContext.Current != null)
                {
                    int? CompanyId = HttpContext.Current.Session[session.CompanyId] as int?;

                    if (CompanyId != null)
                        _CompanyId = CompanyId;
                    else if (HttpContext.Current.Session[session.CompanyId] is Int64)
                        _CompanyId = Convert.ToInt32((Int64)HttpContext.Current.Session[session.CompanyId]);
                }

                return _CompanyId;
            }
            set
            {
                _CompanyId = value;
            }
        }

        public virtual int? UserId
        {
            get
            {
                if (_UserId == null && HttpContext.Current != null)
                {
                    int? userId = HttpContext.Current.Session[session.UserId] as int?;

                    if (userId != null)
                        _UserId = userId;
                    else if (HttpContext.Current.Session[session.UserId] is Int64)
                        _UserId = Convert.ToInt32((Int64)HttpContext.Current.Session[session.UserId]);
                }

                return _UserId;
            }
            internal set
            {
                _UserId = value;
            }
        }

        public virtual string UserName
        {
            get
            {
                if (_UserName == null && HttpContext.Current != null)
                {
                    string UserName = HttpContext.Current.Session[session.UserName] as string;

                    if (_UserName != null)
                        _UserName = UserName;
                    else if (HttpContext.Current.Session[session.UserName] is string)
                        _UserName = Convert.ToString(HttpContext.Current.Session[session.UserName]);
                }

                return _UserName;
            }
            internal set
            {
                _UserName = value;
            }
        }

        public virtual MenuService MenuService
        {
            get
            {
                if (_MenuService == null)
                    _MenuService = new MenuService(this);

                return _MenuService;
            }
        }

        public virtual string EmployeeCode
        {
            get
            {
                if (_EmployeeCode == null && HttpContext.Current != null)
                {
                    string EmployeeCode = HttpContext.Current.Session[session.EmployeeCode] as string;

                    if (_EmployeeCode != null)
                        _EmployeeCode = EmployeeCode;
                    else if (HttpContext.Current.Session[session.EmployeeCode] is string)
                        _EmployeeCode = Convert.ToString(HttpContext.Current.Session[session.EmployeeCode]);
                }

                return _EmployeeCode;
            }

            internal set
            {
                _EmployeeCode = value;
            }
        }

        public virtual Guid[] MenuId
        {
            get
            {
                if (_MenuId == null && HttpContext.Current != null)
                {
                    Guid[] MenuId = HttpContext.Current.Session[session.MenuId] as Guid[];

                    if (_MenuId != null)
                        _MenuId = MenuId;
                    else if (HttpContext.Current.Session[session.MenuId] is Guid[])
                        _MenuId = (Guid[])HttpContext.Current.Session[session.MenuId];
                }

                return _MenuId;
            }

            internal set
            {
                _MenuId = value;
            }
        }

        public virtual RoleService RoleService
        {
            get
            {
                if (_RoleService == null)
                    _RoleService = new RoleService(this);

                return _RoleService;
            }
        }

        public virtual LoginService LoginService
        {
            get
            {
                if (_LoginService == null)
                    _LoginService = new LoginService(this);

                return _LoginService;
            }
        }

        public virtual ModuleHandler Module { get; internal set; }

    }
}
