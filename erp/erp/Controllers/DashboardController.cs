using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using erp.Base;
using erp.Models;

namespace erp.Controllers
{
    public class DashboardController : Controller
    {
        Function func = new Function();
        string content;
        // GET: Dashboard
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult _Sidebar()
        {
            Menu menu = new Menu();
            func.ConnectionString();
            func.conn.Open();
            int userRole = Convert.ToInt32( Session["userRoleID"] );
            DataTable dtblmenu = new DataTable();
            string query = "exec pr_sidemenu @roleID";
            
            SqlDataAdapter sqlDa = new SqlDataAdapter(query, func.conn);
            sqlDa.SelectCommand.Parameters.AddWithValue("@roleID", userRole);
            sqlDa.Fill(dtblmenu);
            ViewBag.Message = userRole;

            for (int i = 0; i < dtblmenu.Rows.Count; i++)
            {
                content += "<li id='" + dtblmenu.Rows[i]["module_groupID"] + "'>";
                content += "    <a><i class='" + dtblmenu.Rows[i]["class_icon"] + "'></i>" + dtblmenu.Rows[i]["module_groupName"] + "</a>";
                content += " </li>";
            }


            return Content(content);
        }

        [HttpGet]
        public ActionResult FrontMenu(int roleid, int modulegroupid)
        {
            func.ConnectionString();
            func.conn.Open();
            int userRole = Convert.ToInt32(Session["userRoleID"]);
            DataTable dtblmenu = new DataTable();
            DataTable dtblmenux = new DataTable();
            string query = "exec pr_menulist @roleID,@modulegroupid";
            string query1 = "SELECT	DISTINCT b.module_subgroupName" +
                            " FROM sys_module a " +
                            " inner join sys_module_subgroup b on" +
                            " b.module_subgroupID = a.module_subgroupID " +
                            " inner join sys_module_group c on " +
                            " c.module_groupID = a.module_groupID " +
                            " WHERE c.module_groupID = @modulegroupid ";

            //execute first the query1
            SqlDataAdapter sqlDa = new SqlDataAdapter(query1, func.conn);
            sqlDa.SelectCommand.Parameters.AddWithValue("@modulegroupid", modulegroupid);
            sqlDa.Fill(dtblmenu);

            //execute the query
            SqlDataAdapter sqlDax = new SqlDataAdapter(query, func.conn);
            sqlDax.SelectCommand.Parameters.AddWithValue("@roleID", roleid);
            sqlDax.SelectCommand.Parameters.AddWithValue("@modulegroupid", modulegroupid);
            sqlDax.Fill(dtblmenux);

            //then loop
            for (int i = 0; i < dtblmenu.Rows.Count; i++)
            {
                content += "<h3>" + dtblmenu.Rows[i]["module_subgroupName"] + "</h3>";
                for (int x = 0; x < dtblmenux.Rows.Count; x++)
                {
                    content += "<li id='" + dtblmenux.Rows[x]["moduleID"] + "'>";
                    content += "    <a href='"+ func.Domain + "/" + dtblmenux.Rows[x]["linkController"] + "/" + dtblmenux.Rows[x]["linkAction"] + "'><i class=''></i>" + dtblmenux.Rows[x]["module_name"] + "</a>";
                    content += " </li>";
                }
            }
          

            return Content(content);
        }
    }
}