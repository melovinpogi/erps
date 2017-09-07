using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace erp.Models
{
    public class ModuleModel
    {
        //
    }
    public class ModuleGroup
    {
        public int module_groupID { get; set; }
        [DisplayName("Group Name")]
        [Required(ErrorMessage = "This field is required.")]
        public string module_groupName { get; set; }
        [DisplayName("Group Description")]
        [Required(ErrorMessage = "This field is required.")]
        public string module_groupDesc { get; set; }
        [DisplayName("Sequence")]
        public int sequence { get; set; }
        [DisplayName("Class Icon")]
        [Required(ErrorMessage = "This field is required.")]
        public string class_icon { get; set; }

    }

    public class ModuleSubGroup
    {
        public int module_subgroupID { get; set; }
        [DisplayName("SubGroup Name")]
        [Required(ErrorMessage = "This field is required.")]
        public string module_subgroupName { get; set; }
        [DisplayName("SubGroup Description")]
        [Required(ErrorMessage = "This field is required.")]
        public string module_subgroupDesc { get; set; }
        [DisplayName("Sequence")]
        public int sequence { get; set; }
    }
}