using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace erp.Models
{
    public class Menu
    {
        public int userid { get; set; }
        public int user_roleID { get; set; }
        public int moduleID { get; set; }
        public string username { get; set; }
        public string user_roleName { get; set; }
        public string module_groupName { get; set; }
        public string module_subgroupName { get; set; }
        public string module_name { get; set; }
        public string class_icon { get; set; }
        public string linkAction { get; set; }
        public string linkController { get; set; }
    }

    public class FrontMenu
    {
        public int moduleID { get; set; }
        public string module_name { get; set; }
        public string module_subgroupName { get; set; }
        public string linkAction { get; set; }
        public string linkController { get; set; }
    }
}