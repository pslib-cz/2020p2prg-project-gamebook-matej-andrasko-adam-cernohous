using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace gamebook.Models
{
    public class Location
    {
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public string Pic { get; set; }
        public List<string> Sound { get; set; }
        public string StoryLine { get; set; }
    }
}
