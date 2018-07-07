using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Yogiram.core.Context;
using Yogiram.core.DataSource;
using Yogiram.core.Models;
using Yogiram.core.Resources;

namespace Yogiram.core.Mvc
{
    public class BaseController : Controller
    {
        public ContextContainer Context { get; private set; }

        private static readonly Guid Menu_CompanyMaster = Guid.Parse("94D4A131-A441-46EC-B027-5363BF417233");

        protected bool SkipMenuLoad { get; set; }

        protected CoreEntities CoreContext;

        //protected ILog log;

        public BaseController()
        {
            CoreContext = CoreEntities.Create();
            CoreContext.Configuration.ProxyCreationEnabled = false;
            Context = new ContextContainer();
        }

        protected override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            //var version = ModulesConfig.GetModuleVersion(Context);
            //if (version != null)
            //    Response.AppendHeader("OM-Version", version.CodeVersion.ToString());

            if (filterContext.Result is ViewResult)
            {
                Response.Cache.SetCacheability(HttpCacheability.NoCache);  // HTTP 1.1.
                Response.Cache.AppendCacheExtension("no-store, must-revalidate");
                Response.AppendHeader("Pragma", "no-cache"); // HTTP 1.0.
                Response.AppendHeader("Expires", "0"); // Proxies.

                if (Context.UserId != null && !SkipMenuLoad) //&& !SkipMenuLoad
                {
                    Guid? ModuleId;
                    Guid[] MenuId = Context.MenuId;

                    if (MenuId == null)                    
                        ModuleId = (from a in CoreContext.AT_Menu where a.MenuId == Menu_CompanyMaster select a.ModuleId).FirstOrDefault();                    
                    else                    
                        ModuleId = (from a in CoreContext.AT_Menu where MenuId.FirstOrDefault()==a.MenuId select a.ModuleId).FirstOrDefault();
                    

                    ViewBag.MenuItems = LoadMenu(ModuleId);

                    ViewBag.MainMenus = (from a in CoreContext.AT_Menu
                                         join b in CoreContext.AT_UserInRoles on a.ModuleId equals b.ModuleId
                                         orderby a.Sort
                                         where a.Url == null && b.UserId == Context.UserId && a.ModuleId==ModuleId
                                         select new MenuItem { MenuId = a.MenuId, MenuName = a.MenuName }).ToList();

                    //ViewBag.Menu = LoadMenu();
                    //Context.MenuService.GetMenuItem(0, new Guid());

                    //ViewBag.UserMenu = GetUserMenu();

                    //ViewBag.Breadcrumb = GetBreadcrumb();

                    //ViewBag.Toolbox = BuildToolbox();

                    //if (!SkipVisitEntry)
                    //{
                    //    Task task = AddUserVisitAsync();
                    //}
                }
                else
                {
                    ViewBag.MenuItem = new List<MenuItem>();
                    ViewBag.MainMenus = new List<MenuItem>();
                    //ViewBag.Breadcrumb = new List<BreadcrumbItem>();
                    //ViewBag.Toolbox = new ToolboxModel();
                }
            }

            base.OnResultExecuting(filterContext);
        }

        protected virtual List<MenuItem> LoadMenu(Guid? ModuleId)
        {
            List<MenuItem> menuItems = (from a in CoreContext.AT_Menu
                                        join b in CoreContext.AT_UserInRoles on a.ModuleId equals b.ModuleId into GroupJoin
                                        orderby a.Sort
                                        where a.ParentMenuId != null && a.ModuleId == ModuleId
                                        select new MenuItem { MenuId = a.MenuId, MenuName = a.MenuName, ParentId=a.ParentMenuId, Url = a.Url }).ToList();

            return menuItems;
        }

        protected ActionResult DataTable(DtParams dt, IEnumerable<dynamic> query)
        {
            var count = (from a in query select a).Count();

            Response response = new Response()
            {
                draw = dt.draw,
                recordsTotal = count
            };

            query = dt.Order(query);

            query = dt.Filter(query);

            var filterCount = query.Count();

            var data = dt.Take(query).ToList();

            response.data = data;
            response.recordsFiltered = filterCount;

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        protected virtual ActionResult Success(String message)
        {
            return Json(new SuccessJsonModel { msg = message }, JsonRequestBehavior.AllowGet);
        }

        protected virtual ActionResult Error(String message)
        {
            return Json(new ErrorJsonModel { error = message }, JsonRequestBehavior.AllowGet);
        }

        protected virtual string EmployeeCode()
        {
            string EmployeeCode = "";

            var lastemployee = (from a in CoreContext.EmployeeMaster.OrderByDescending(c => c.EmployeeId) where a.CompanyId == Context.CompanyId select a).FirstOrDefault();

            if (lastemployee == null)
                EmployeeCode = "STV" + DateTime.Now.Year + "001";
            else
                EmployeeCode = "STV" + DateTime.Now.Year + (Convert.ToInt32(lastemployee.EmployeeCode.Substring(7, lastemployee.EmployeeCode.Length - 7)) + 1).ToString("D3");

            return EmployeeCode;

        }

        protected virtual string CreatePassword(int PasswordLength)
        {
            string _allowedChars = "0123456789abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ!@#$%&";
            Random randNum = new Random();
            char[] chars = new char[PasswordLength];
            int allowedCharCount = _allowedChars.Length;
            for (int i = 0; i < PasswordLength; i++)
            {
                chars[i] = _allowedChars[(int)((_allowedChars.Length) * randNum.NextDouble())];
            }
            return new string(chars);
        }

        protected virtual String G(String Key)
        {
            ResourceManager rm = new ResourceManager(typeof(Global));
            string GlobalString = rm.GetString(Key);
            return GlobalString;
        }


    }
}
