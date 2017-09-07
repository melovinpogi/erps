using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace erp.Base
{
    public class Function
    {
        //Connection to database
        public string connString;
        public SqlConnection conn;
        public SqlDataReader dreader;
        public SqlCommand cmd;
        public string Domain;
        public void ConnectionString()
        {
            string Database = ConfigurationManager.AppSettings["database"].ToString();
            string Server = ConfigurationManager.AppSettings["server"].ToString();
            string Username = ConfigurationManager.AppSettings["username"].ToString();
            string Password = ConfigurationManager.AppSettings["password"].ToString();
            Domain = ConfigurationManager.AppSettings["domain"].ToString();


            connString = "Initial Catalog=" + Database + ";" + "Data Source=" + Server + ";User ID=" + Username + ";Password=" + Password + "; MultipleActiveResultSets=True";
            conn = new SqlConnection(connString);
        }

        public void SelectQuery(DataTable datatable, string query)
        {
            ConnectionString();
            conn.Open();
            SqlDataAdapter sqlDa = new SqlDataAdapter(query, conn);
            sqlDa.Fill(datatable);
            
        }

        public void SelectQueryWhere(DataTable datatable, string query,string columnName,int id)
        {
            ConnectionString();
            conn.Open();
            SqlDataAdapter sqlDa = new SqlDataAdapter(query, conn);
            sqlDa.SelectCommand.Parameters.AddWithValue(columnName, id);
            sqlDa.Fill(datatable);
            
        }

       
    }

}