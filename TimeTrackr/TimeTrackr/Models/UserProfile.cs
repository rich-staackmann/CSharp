using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TimeTrackr.Models
{
    //this is the user profile model provided with the default MVC application
    //I moved it from its original model file and deleted the context it was declared in
    //I needed control over this model so that Users will have the time they track linked to their account only
    [Table("UserProfile")]
    public class UserProfile
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        public string UserName { get; set; }

        //our custom model, declared virtual for lazy loading
        public virtual ICollection<Task> Tasks { get; set; }
    }
}