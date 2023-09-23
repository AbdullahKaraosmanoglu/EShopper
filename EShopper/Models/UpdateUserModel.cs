using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EShopper.Models
{
    public class UpdateUserModel
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
    }
}