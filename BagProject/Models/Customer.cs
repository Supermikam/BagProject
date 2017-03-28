﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BagProject.Models
{
    public class Customer
    {
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string HomePhone { get; set; }
        public string WorkPhone { get; set; }
        [Required]
        public string MobilePhone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public bool Active { get; set; }
        public string Password { get; set; }

        public List<Order> Orders { get; set; }
    }
}