using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Employement_Project_MVC.Models
{
    public class JobsViewModel
    {
        public string JobTitle { get; set; }
        public IEnumerable<ApplayForJob> Applies { get; set; }
    }
    
}