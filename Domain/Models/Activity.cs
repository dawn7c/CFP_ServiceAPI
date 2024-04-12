﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Activity 
    {
        public readonly Guid BaseEntity;

        public int Id { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
    }
}
