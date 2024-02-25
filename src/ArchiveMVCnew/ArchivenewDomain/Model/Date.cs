using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ArchivenewDomain.Model
{
    public partial class Date
    {
        [Required(ErrorMessage = "This field shouldn't be empty")]
        public int DateId { get; set; }

        [Required(ErrorMessage = "This field shouldn't be empty")]
        [Display(Name = "The full name of the person")]
        public string FullName { get; set; } = null!;

        [Required(ErrorMessage = "This field shouldn't be empty")]
        public string Title { get; set; } = null!;

        [Required(ErrorMessage = "This field shouldn't be empty")]
        public string Faculty { get; set; } = null!;

        public string? Department { get; set; } 

        public string? Format { get; set; } 

        [Display(Name = "The extent of material")]
        public string? ExtentOfMaterial { get; set; }

        [Display(Name = "The date of publication")]
        public DateOnly? Date1 { get; set; } 

        public virtual ICollection<DateReference> DateReferences { get; set; } = new List<DateReference>();
    }
}
