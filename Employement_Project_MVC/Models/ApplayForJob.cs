using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Employement_Project_MVC.Models
{
    public class ApplayForJob
    {

        public int Id  { get; set; }
        [Display(Name ="الرسالة")]
        [AllowHtml]
        public string Message { get; set; }
        [Display(Name = "تاريخ التقديم")]
        public DateTime AplayOn  { get; set; }
        [Display(Name = "السيرة الذاتية")]
        public string CvApplay { get; set; }
        [Display(Name = "معرض الاعمال")]
        public string LinksGlary { get; set; }

        public bool Accept { get; set; }

     
        public int JobId { get; set; }
        public string Userid { get; set; }
 
        public virtual Job job { get; set; }
     
        
        public virtual ApplicationUser user { get; set; }
    }
    
}