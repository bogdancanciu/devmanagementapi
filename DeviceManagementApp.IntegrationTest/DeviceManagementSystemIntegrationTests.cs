using DeviceManagementSystem.Models;
using System;
using System.Text.Json;
using Xunit;

namespace DeviceManagementApp.IntegrationTest
{
    public class DeviceManagementSystemIntegrationTests
    {
        //Users table integration tests.
        [Fact]
        public async void PostUser_And_Check_Test()
        {
            /*Create a new user and Read the last user, checking if it's the same.
             The reason of using GetLastUsersRecord is mainly for future development purpose in case of
             testing multiple Create and Read scenarios.*/
            ControllerRequest cr = new ControllerRequest();
            User newUser =await cr.PostUser("Marius Vrancea", "Talent Acquisition", "Harghita");
            JsonElement[] getResponse = await cr.GetLastUsersRecords(1);

            Assert.Equal(newUser.Name, getResponse[0].GetProperty("Name").ToString());
            Assert.Equal(newUser.Role, getResponse[0].GetProperty("Role").ToString());
            Assert.Equal(newUser.Location, getResponse[0].GetProperty("Location").ToString());
        }
        [Fact]
        public async void PostUser_Put_Check_Test()
        {
            /*Create a new user, then Delete it, checking if it was actually deleted
            by sending a Get request to the /users/{justPostedUser} API endpoint.
            Note: We suppose the Post request works as expected if the previous test(PostUser_And_Check_Test) is passed.*/
            ControllerRequest cr = new ControllerRequest();
            User newUser = await cr.PostUser("Vasile Arghezi", "Electrical Engineer", "Sighet");
            string updatedName = "Gehorghe Filip";
            newUser.Name = updatedName;
            cr.PutUser(newUser);
            JsonElement[] getResponse = await cr.GetLastUsersRecords(1);

            Assert.Equal(newUser.Name, getResponse[0].GetProperty("Name").ToString());
            Assert.Equal(newUser.Role, getResponse[0].GetProperty("Role").ToString());
            Assert.Equal(newUser.Location, getResponse[0].GetProperty("Location").ToString());
        }
        [Fact]
        public async void PostUser_Delete_Check_Test()
        {
            /*Create a new user, then Update it, checking if it was actually updated
            by sending a Get request to the /users API endpoint.
            Note: We suppose the Post request works as expected if the previous test(PostUser_And_Check_Test) is passed.
                  The reason of using GetLastUsersRecord is mainly for future development purpose in case of
                  testing multiple Create and Update scenarios.*/
            ControllerRequest cr = new ControllerRequest();
            User newUser = await cr.PostUser("Vasile Arghezi", "Electrical Engineer", "Sighet");
            cr.DeleteUser(newUser.Id);
            string serverResponse = await cr.GetUserWithID(newUser.Id);

            Assert.Equal("null", serverResponse);
        }
        //Devices table integration tests.
        [Fact]
        public async void PostDevice_And_Check_Test()
        {
            /*Create a new device and Read the last device, checking if it's the same.
             The reason of using GetLastDevicesRecord is mainly for future development purpose in case of
             testing multiple Create and Read scenarios.*/
            ControllerRequest cr = new ControllerRequest();
            Device newDevice = await cr.PostDevice("Galaxy S21","Samsumg","Mobile Phone","Android","11.1","Exynos 2100",8);
            JsonElement[] getResponse = await cr.GetLastDevicesRecords(1);

            Assert.Equal(newDevice.Name, getResponse[0].GetProperty("Name").ToString());
            Assert.Equal(newDevice.Manufacturer, getResponse[0].GetProperty("Manufacturer").ToString());
            Assert.Equal(newDevice.Type, getResponse[0].GetProperty("Type").ToString());
            Assert.Equal(newDevice.OS, getResponse[0].GetProperty("OS").ToString());
            Assert.Equal(newDevice.OSVersion, getResponse[0].GetProperty("OSVersion").ToString());
            Assert.Equal(newDevice.Processor, getResponse[0].GetProperty("Processor").ToString());
            Assert.Equal(newDevice.RAM, short.Parse(getResponse[0].GetProperty("RAM").ToString()));
        }
        [Fact]
        public async void PostDevice_Put_Check_Test()
        {
            /*Create a new device, then Delete it, checking if it was actually deleted
            by sending a Get request to the /devices/{justPostedDevice} API endpoint.
            Note: We suppose the Post request works as expected if the previous test(PostDevice_And_Check_Test) is passed.*/
            ControllerRequest cr = new ControllerRequest();
            Device newDevice = await cr.PostDevice("Galaxy S10", "Samsumg", "Mobile Phone", "Android", "8.1", "Exynos 1600", 4);
            string updatedProcessor = "Snapdragon 888";
            newDevice.Processor = updatedProcessor;
            cr.PutDevice(newDevice);
            JsonElement[] getResponse = await cr.GetLastDevicesRecords(1);

            Assert.Equal(newDevice.Name, getResponse[0].GetProperty("Name").ToString());
            Assert.Equal(newDevice.Manufacturer, getResponse[0].GetProperty("Manufacturer").ToString());
            Assert.Equal(newDevice.Type, getResponse[0].GetProperty("Type").ToString());
            Assert.Equal(newDevice.OS, getResponse[0].GetProperty("OS").ToString());
            Assert.Equal(newDevice.OSVersion, getResponse[0].GetProperty("OSVersion").ToString());
            Assert.Equal(newDevice.Processor, getResponse[0].GetProperty("Processor").ToString());
            Assert.Equal(newDevice.RAM, short.Parse(getResponse[0].GetProperty("RAM").ToString()));
        }
        [Fact]
        public async void PostDevice_Delete_Check_Test()
        {
            /*Create a new device, then Update it, checking if it was actually updated
            by sending a Get request to the /devices API endpoint.
            Note: We suppose the Post request works as expected if the previous test(PostDevice_And_Check_Test) is passed.
                  The reason of using GetLastDevicesRecord is mainly for future development purpose in case of
                  testing multiple Create and Update scenarios.*/
            ControllerRequest cr = new ControllerRequest();
            Device newDevice = await cr.PostDevice("Galaxy S9", "Samsumg", "Mobile Phone", "Android", "7", "Exynos 1400", 3);
            cr.DeleteDevice(newDevice.Id);
            string serverResponse = await cr.GetDeviceWithID(newDevice.Id);

            Assert.Equal("null", serverResponse);
        }
    }
}
