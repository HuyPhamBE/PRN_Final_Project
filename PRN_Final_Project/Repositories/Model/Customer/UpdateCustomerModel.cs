﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Model.Customer
{
    public class UpdateCustomerModel
    {
        public Guid cusId { get; set; }
        public string fullName { get; set; }
        public string phone { get; set; }
        public string address { get; set; }
        public DateTime dob { get; set; }
        public bool gender { get; set; }
        public string imageUrl { get; set; }
        public Guid accountID { get; set; }
    }
}
