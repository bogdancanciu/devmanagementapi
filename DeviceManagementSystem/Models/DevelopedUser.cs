using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeviceManagementSystem.Models
{
    public partial class User
    {
        public User(string name, string role, string location)
        {
            this.Name = name;
            this.Role = role;
            this.Location = location;
        }
    }
}