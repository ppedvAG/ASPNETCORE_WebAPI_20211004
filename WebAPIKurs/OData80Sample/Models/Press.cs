﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OData80Sample.Models
{
    // Press
    public class Press
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public Category Category { get; set; }
    }

}
