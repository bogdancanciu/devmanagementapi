using DeviceManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DeviceManagementSystem.Controllers
{
    public class DevicesController : ApiController
    {
        public IEnumerable<Device> Get()
        {
            using (DeviceManagementDBContext dbContext = new DeviceManagementDBContext())
            {
                foreach(var cv in dbContext.Devices.ToList())
                {
                    if(cv.Id_User != null)
                    {
                        cv.User = dbContext.Users.FirstOrDefault(e => e.Id == cv.Id_User);
                    }
                    else
                    {
                        cv.User = null;
                    }
                }
                return dbContext.Devices.ToList();
            }
        }
        public Device Get(int id)
        {
            using (DeviceManagementDBContext dbContext = new DeviceManagementDBContext())
            {
                Device deviceToGet = dbContext.Devices.FirstOrDefault(e => e.Id == id);
                if (deviceToGet != null)
                {
                    if (deviceToGet.Id_User != null)
                    {
                        deviceToGet.User = dbContext.Users.FirstOrDefault(e => e.Id == deviceToGet.Id_User);
                    }
                    else
                    {
                        deviceToGet.User = null;
                    }
                }
                return dbContext.Devices.FirstOrDefault(e => e.Id == id);
            }
        }
        public int Post([FromBody] Device device)
        {
            using (DeviceManagementDBContext dbContext = new DeviceManagementDBContext())
            {
                if (!dbContext.Devices.Any(dbDevices => dbDevices.Name == device.Name && dbDevices.Manufacturer == device.Manufacturer && dbDevices.Type == device.Type
                && dbDevices.OS == device.OS && dbDevices.OSVersion == device.OSVersion && dbDevices.Processor == device.Processor && dbDevices.RAM == device.RAM))
                {
                    device.User = null;
                    dbContext.Devices.Add(device);
                    dbContext.SaveChanges();
                    return device.Id;
                }
                else
                {
                    return -1;
                }
            }
        }
        public HttpResponseMessage Put(int id, [FromBody] Device Device)
        {
            try
            {
                using (DeviceManagementDBContext dbContext = new DeviceManagementDBContext())
                {
                    var deviceToUpdate = dbContext.Devices.FirstOrDefault(e => e.Id == id);
                    if (deviceToUpdate == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                            "Device ID  " + id.ToString() + " not found.");
                    }
                    else
                    {
                        deviceToUpdate.Name = Device.Name;
                        deviceToUpdate.Manufacturer = Device.Manufacturer;
                        deviceToUpdate.Type = Device.Type;
                        deviceToUpdate.OS = Device.OS;
                        deviceToUpdate.OSVersion = Device.OSVersion;
                        deviceToUpdate.Processor = Device.Processor;
                        deviceToUpdate.RAM = Device.RAM;
                        deviceToUpdate.Id_User = Device.Id_User;
                        dbContext.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, deviceToUpdate);
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                using (DeviceManagementDBContext dbContext = new DeviceManagementDBContext())
                {
                    var deviceToDelete = dbContext.Devices.Where(e => e.Id == id).First();
                    if (deviceToDelete == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                            "Device ID " + id.ToString() + " not found.");
                    }
                    else
                    {
                        dbContext.Devices.Remove(deviceToDelete);
                        dbContext.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
