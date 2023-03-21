using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Employement_Project_MVC.Models
{
    public class Job
    {
        [Required]
        [Display(Name = "عنوان البحث عن الوظيفة")]
        [MaxLength(40,ErrorMessage ="لا تكتب اكتر من 50 حرف")]
        public string  SreachEngigJob { get; set; }
        [Required]
        [Display(Name ="السعر يبدا من")]
        [Range(5,10 ,ErrorMessage ="لا يجب ان يزيد السر 10 او ينقص عن5")]
        public int  MinPrice { set; get; }
        [Required]
        [Display(Name = "الي ")]
        [Range(10, 1000, ErrorMessage = "لا يجب ان يزيد السر 1000 او ينقص عن10")]
        public int MaxPrice { get; set; }
        public int Id { get; set; }
        [Display(Name ="اسم الوظيفة")]
        public string JobTtile { get; set; }
        [Display(Name = "وصف الوظيفة")]
        [AllowHtml]
        public string JobContent { get; set; }
        [Display(Name = "صورة الوظيفة")]
        public string JobImage { get; set; }
        [ForeignKey("cat")]
        [Display(Name ="نوغ الوظيفة")]
        public int CategoryId { get; set; }
        public string UaerID { set; get; }
        public  virtual ApplicationUser user { get; set; }
        public virtual Category cat { set; get; }
    }
}