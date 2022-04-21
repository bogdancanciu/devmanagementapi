using DeviceManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DeviceManagementSystem.Controllers
{
    public class UsersController : ApiController
    {
        public IEnumerable<User> Get()
        {
            using (DeviceManagementDBContext dbContext = new DeviceManagementDBContext())
            {
                return dbContext.Users.ToList();
            }
        }
        public User Get(int id)
        {
            using (DeviceManagementDBContext dbContext = new DeviceManagementDBContext())
            {
                return dbContext.Users.FirstOrDefault(e => e.Id == id);
            }
        }
        public int Post([FromBody] User user)
        {
            using (DeviceManagementDBContext dbContext = new DeviceManagementDBContext())
            {
                dbContext.Users.Add(user);
                dbContext.SaveChanges();
                return user.Id;
            }
        }
        public HttpResponseMessage Put(int id, [FromBody] User user)
        {
            try
            {
                using (DeviceManagementDBContext dbContext = new DeviceManagementDBContext())
                {
                    var userToUpdate = dbContext.Users.FirstOrDefault(e => e.Id == id);
                    if (userToUpdate == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                            "User ID  " + id.ToString() + " not found.");
                    }
                    else
                    {
                        userToUpdate.Name = user.Name;
                        userToUpdate.Role = user.Role;
                        userToUpdate.Location = user.Location;
                        dbContext.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, userToUpdate);
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
                    var userToDelete = dbContext.Users.FirstOrDefault(e => e.Id == id);
                    if (userToDelete == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                            "User ID " + id.ToString() + " not found.");
                    }
                    else
                    {
                        dbContext.Users.Remove(userToDelete);
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
