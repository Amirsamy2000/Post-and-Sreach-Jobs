using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Employement_Project_MVC.Models
{
    public class Accepted
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name ="محتوي الرسالة")]
        public string Message { get; set; }
        
        public DateTime AccpetedOn { get; set; }
        [ForeignKey("Publisher")]
        public string PublisherId { get; set; }
        [ForeignKey("Applay")]
        public int ApplayId { set; get; }

        public virtual List<ApplicationUser> Publisher { get; set; }
        public  virtual List<ApplayForJob >Applay { get; set; }


    }
}