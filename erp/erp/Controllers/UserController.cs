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
    public class UserController : Controller
    {
        Function func = new Function();
        [HttpGet]
        // GET: User
        public ActionResult Index()
        {
            try
            {
                DataTable dtblUser = new DataTable();
                string query = "SELECT * FROM sys_user";
                //Connection to database
                func.SelectQuery(dtblUser, query);
                return View(dtblUser);
            }
            catch (Exception)
            {

                return RedirectToAction("Login");
            }
            
        }
        #region Register

       
        [HttpGet]
        // GET: User/Register
        public ActionResult Register()
        {
            //return model if you want to pass your values/data from database
            return View(new UserModel());
        }

        // POST: User/Create
        [HttpPost]
        public ActionResult Register(UserModel userModel)
        {
            try
            {
                // TODO: Add insert logic here
                string query = "INSERT INTO sys_user (username,password,email,phoneNumber) VALUES(@username,@password,@email,@phoneNumber)";
                //Connection to database
                func.ConnectionString();
                func.conn.Open();
                func.cmd = new SqlCommand(query, func.conn);
                func.cmd.Parameters.AddWithValue("@username", userModel.username);
                func.cmd.Parameters.AddWithValue("@password", userModel.password);
                func.cmd.Parameters.AddWithValue("@email", userModel.email);
                func.cmd.Parameters.AddWithValue("@phoneNumber", userModel.phoneNumber);
                func.cmd.ExecuteNonQuery();

                return RedirectToAction("Index");
            }
            catch(SqlException err)
            {
                ViewBag.ErrorMessage = err.ToString();
                return View();
            }
        }

        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            UserModel userModel = new UserModel();
            DataTable dtbluser = new DataTable();
            string query = "SELECT * FROM sys_user WHERE id = @id";
            func.SelectQueryWhere(dtbluser, query, "@id", id);

            //If has rows
            if (dtbluser.Rows.Count == 1)
            {
                userModel.id = Convert.ToInt32(dtbluser.Rows[0]["id"]);
                userModel.username = dtbluser.Rows[0]["username"].ToString();
                userModel.password = dtbluser.Rows[0]["password"].ToString();
                userModel.email = dtbluser.Rows[0]["email"].ToString();
                userModel.phoneNumber = dtbluser.Rows[0]["phoneNumber"].ToString();

                //return model to pass value/data to view
                return View(userModel);
            }
            else
            {
                return RedirectToAction("Index");
            }

        }

        



        // POST: User/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, UserModel userModel)
        {
            try
            {
                // TODO: Add update logic here
                string query = "UPDATE sys_user set username = @username,password = @password,email = @email,phoneNumber = @phoneNumber WHERE id =" + id;
                //Connection to database
                func.ConnectionString();
                func.conn.Open();
                func.cmd = new SqlCommand(query, func.conn);
                func.cmd.Parameters.AddWithValue("@username", userModel.username);
                func.cmd.Parameters.AddWithValue("@password", userModel.password);
                func.cmd.Parameters.AddWithValue("@email", userModel.email);
                func.cmd.Parameters.AddWithValue("@phoneNumber", userModel.phoneNumber);
                func.cmd.ExecuteNonQuery();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            // TODO: Add update logic here
            string query = "DELETE FROM sys_user WHERE id = @id";
            //Connection to database
            func.ConnectionString();
            func.conn.Open();
            func.cmd = new SqlCommand(query, func.conn);
            func.cmd.Parameters.AddWithValue("@id", id);
            func.cmd.ExecuteNonQuery();

            return RedirectToAction("Index");

        }

        #endregion


        public ActionResult Login()
        {
            return View();    
        }

        [HttpPost]
        public ActionResult Login(LoginModel loginModel)
        {
            DataTable dtbluser = new DataTable();
            string query = "SELECT a.*,b.user_roleName FROM sys_user a" +
                            " INNER JOIN sys_user_role_h b on" +
                            " a.userRoleID = b.user_roleID" + 
                             " WHERE a.username =@username and a.password = @password";
            func.ConnectionString();
            func.conn.Open();
            SqlDataAdapter sqlDa = new SqlDataAdapter(query, func.conn);
            sqlDa.SelectCommand.Parameters.AddWithValue("@username", loginModel.username);
            sqlDa.SelectCommand.Parameters.AddWithValue("@password", loginModel.password);
            sqlDa.Fill(dtbluser);


            //If has rows
            if (dtbluser.Rows.Count == 1)
            {
                ViewBag.Message = "success";
                Session["userID"] = dtbluser.Rows[0]["id"];
                Session["userName"] = dtbluser.Rows[0]["username"];
                Session["userRoleID"] = dtbluser.Rows[0]["userRoleID"];
                Session["userRoleName"] = dtbluser.Rows[0]["user_roleName"];
                return RedirectToAction("Index","Dashboard");
            }
            else
            {
                ViewBag.Message = "error";
            }
            return View();

        }
        public ActionResult Logout()
        {
            Session["userID"] = 0;
            Session["userName"] = "";
            Session["userRoleID"] = 0;
            Session["userRoleName"] = "";
            Session.Abandon();
            return RedirectToAction("Login");
        }
    }
}
