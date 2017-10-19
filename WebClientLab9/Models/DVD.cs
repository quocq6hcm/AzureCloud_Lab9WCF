using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebClientLab9.Models
{
    public class DVD
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int YearOfRelease { get; set; }

        public string Title { get; set; }

        public string Genre { get; set; }
    }
}