﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TimeTrackr.Models
{
    //this class represents a basic interval of time that a user would want to track
    //this model has a many to one relationship with our UserProfile model
    [Table("Task")]
    public class Task
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

        [Required(ErrorMessage = "Please assign this task to a category.")]
        public String Category { get; set; }

        [ForeignKey("UserProfile")]
        public int UserID { get; set; } //foreign key

        //the virtual keyword allows entity to use advanced features like lazy-loading
        public virtual UserProfile UserProfile { get; set; }

    }
}