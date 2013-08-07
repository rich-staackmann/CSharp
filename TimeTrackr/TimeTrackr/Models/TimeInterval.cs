using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TimeTrackr.Models
{
    [Table("TimeInterval")]
    public class TimeInterval
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; } //identity value

        [Required(ErrorMessage = "Please enter a start time in the format mm/dd/yy XX:XX am/pm")]
        public DateTime StartTime { get; set; }

        [Required(ErrorMessage = "Please enter a start time in the format mm/dd/yy XX:XX am/pm")]
        public DateTime EndTime { get; set; }

        [Required(ErrorMessage = "Please add a short description.")]
        [DataType(DataType.MultilineText)]
        public String Description { get; set; }

        public String Category { get; set; }

        //the virtual keyword allows entity to use advanced features like lazy-loading
        public virtual UserProfile IntervalOwner { get; set; }

    }
}