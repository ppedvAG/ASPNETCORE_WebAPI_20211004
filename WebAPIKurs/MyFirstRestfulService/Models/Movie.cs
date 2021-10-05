using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyFirstRestfulService.Models
{
    public class Movie
    {
        public int Id { get; set; }
        
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
