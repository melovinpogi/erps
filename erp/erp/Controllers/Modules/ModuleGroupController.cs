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

namespace erp.Controllers.Modules
{
    public class ModuleGroupController : Controller
    {
        Function func = new Function();
        [HttpGet]
        // GET: ModuleGroup
        public ActionResult Index()
        {
            try
            {
                DataTable dtbl = new DataTable();
                string query = "SELECT * FROM sys_module_group";
                //Connection to database
                func.SelectQuery(dtbl, query);
                return View(dtbl);
            }
            catch (Exception)
            {
                return RedirectToAction("Login", "User");
            }
        }

        // GET: ModuleGroup/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ModuleGroup/Create
        public ActionResult Create()
        {
            //return model if you want to pass your values/data from database
            return View(new ModuleGroup());
        }

        // POST: ModuleGroup/Create
        [HttpPost]
        public ActionResult Create(ModuleGroup moduleGroup)
        {
            try
            {
                // TODO: Add insert logic here
                string query = "INSERT INTO sys_module_group(module_groupName,module_groupDesc,sequence,class_icon) VALUES(@module_groupName,@module_groupDesc,@sequence,@class_icon)";
                //Connection to database
                func.ConnectionString();
                func.conn.Open();
                func.cmd = new SqlCommand(query, func.conn);
                func.cmd.Parameters.AddWithValue("@module_groupName", moduleGroup.module_groupName);
                func.cmd.Parameters.AddWithValue("@module_groupDesc", moduleGroup.module_groupDesc);
                func.cmd.Parameters.AddWithValue("@sequence", moduleGroup.sequence);
                func.cmd.Parameters.AddWithValue("@class_icon", moduleGroup.class_icon);
                func.cmd.ExecuteNonQuery();

                return RedirectToAction("Index");
            }
            catch (SqlException err)
            {
                ViewBag.ErrorMessage = err.ToString();
                return View();
            }
        }

        // GET: ModuleGroup/Edit/5
        public ActionResult Edit(int id)
        {
            ModuleGroup moduleGroup = new ModuleGroup();
            DataTable dtbl = new DataTable();
            string query = "SELECT * FROM sys_module_group WHERE module_groupID = @id";
            func.SelectQueryWhere(dtbl, query, "@id", id);

            //If has rows
            if (dtbl.Rows.Count == 1)
            {
                moduleGroup.module_groupID = Convert.ToInt32 (dtbl.Rows[0]["module_groupID"]);
                moduleGroup.module_groupName = dtbl.Rows[0]["module_groupName"].ToString();
                moduleGroup.module_groupDesc = dtbl.Rows[0]["module_groupDesc"].ToString();
                moduleGroup.sequence = Convert.ToInt32(dtbl.Rows[0]["sequence"]);
                moduleGroup.class_icon = dtbl.Rows[0]["class_icon"].ToString();

                //return model to pass value/data to view
                return View(moduleGroup);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        // POST: ModuleGroup/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, ModuleGroup moduleGroup)
        {
            try
            {
                // TODO: Add update logic here
                string query = "UPDATE sys_module_group SET module_groupName @module_groupName= ,module_groupDesc= @module_groupDesc,sequence=@sequence ,class_icon=@class_icon  WHERE id =" + id;
                //Connection to database
                func.ConnectionString();
                func.conn.Open();
                func.cmd = new SqlCommand(query, func.conn);
                func.cmd.Parameters.AddWithValue("@module_groupName", moduleGroup.module_groupName);
                func.cmd.Parameters.AddWithValue("@module_groupDesc", moduleGroup.module_groupDesc);
                func.cmd.Parameters.AddWithValue("@sequence", moduleGroup.sequence);
                func.cmd.Parameters.AddWithValue("@class_icon", moduleGroup.class_icon);
                func.cmd.ExecuteNonQuery();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ModuleGroup/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ModuleGroup/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
