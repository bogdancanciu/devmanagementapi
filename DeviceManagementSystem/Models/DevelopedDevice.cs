using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeviceManagementSystem.Models
{
    public partial class Device
    {
        public Device() {
            this.User = null;
        }
        public Device(string name, string manufacturer, string type, string os, string os_version, string processor, short ram)
        {
            this.Name = name;
            this.Manufacturer = manufacturer;
            this.Type = type;
            this.OS = os;
            this.OSVersion = os_version;
            this.Processor = processor;
            this.RAM = ram;
        }
    }
}