using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SchoolAdmin.DataAccess.ViewModels
{
    public class StudentViewModel
    {
        [Required(ErrorMessage = "This field is required")]
        [DataType(DataType.Text, ErrorMessage = "Text Only")]
        [StringLength(20, ErrorMessage = "Max 20 digits")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DataType(DataType.Text, ErrorMessage = "Text Only")]
        [StringLength(20, ErrorMessage = "Max 20 digits")]
        public string MiddleName { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DataType(DataType.Text, ErrorMessage = "Text Only")]
        [StringLength(20, ErrorMessage = "Max 20 digits")]
        public string LastName { get; set; }
    }
}