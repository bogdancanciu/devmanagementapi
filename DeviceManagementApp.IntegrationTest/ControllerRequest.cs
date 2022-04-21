using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;
using DeviceManagementSystem.Models;

namespace DeviceManagementApp.IntegrationTest
{
    public class ControllerRequest
    {
        private int serverPort = 44321;
        //User table related methods
        public async Task<JsonElement[]> GetLastUsersRecords(int number_of_records)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync("https://localhost:" + serverPort.ToString() + "/api/users").Result;
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            JsonElement root = JsonDocument.Parse(responseBody).RootElement;
            JsonElement[] toReturn = new JsonElement[number_of_records];
            int currentIndex = 0;
            for (int i = root.GetArrayLength() - number_of_records; i < root.GetArrayLength(); i++)
            {
                toReturn[currentIndex] = root[i];
                currentIndex++;
            }
            return toReturn;
        }
        public async Task<string> GetUserWithID(int user_id)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync("https://localhost:" + serverPort.ToString() + "/api/users/" + user_id.ToString()).Result;
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            return responseBody;
        }
        public async Task<User> PostUser(string name, string role, string location)
        {
            User toInsert = new User(name, role, location);
            string url = "https://localhost:" + serverPort.ToString() + "/api/users";
            HttpClient client = new HttpClient();
            var serializedData = JsonSerializer.Serialize(toInsert);
            var requestBody = new StringContent(serializedData, System.Text.Encoding.UTF8, "application/json");
            var response = client.PostAsync(url, requestBody).Result;
            int responseBody = int.Parse(await response.Content.ReadAsStringAsync());
            toInsert.Id = responseBody;
            return toInsert;
        }
        public void PutUser(User userToUpdate)
        {
            string url = "https://localhost:" + serverPort.ToString() + "/api/users/" + userToUpdate.Id.ToString();
            HttpClient client = new HttpClient();
            var serializedData = JsonSerializer.Serialize(userToUpdate);
            var requestBody = new StringContent(serializedData, System.Text.Encoding.UTF8, "application/json");
            var response = client.PutAsync(url, requestBody).Result;
        }
        public async void DeleteUser(int user_id)
        {
            string url = "https://localhost:" + serverPort.ToString() + "/api/users/" + user_id.ToString();
            HttpClient client = new HttpClient();
            var response = client.DeleteAsync(url).Result;
            var result = await response.Content.ReadAsStringAsync();
        }
        //Devices table related methods.
        public async Task<JsonElement[]> GetLastDevicesRecords(int number_of_records)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync("https://localhost:" + serverPort.ToString() + "/api/devices").Result;
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            JsonElement root = JsonDocument.Parse(responseBody).RootElement;
            JsonElement[] toReturn = new JsonElement[number_of_records];
            int currentIndex = 0;
            for (int i = root.GetArrayLength() - number_of_records; i < root.GetArrayLength(); i++)
            {
                toReturn[currentIndex] = root[i];
                currentIndex++;
            }
            return toReturn;
        }
        public async Task<string> GetDeviceWithID(int device_id)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync("https://localhost:" + serverPort.ToString() + "/api/devices/" + device_id.ToString()).Result;
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            return responseBody;
        }
        public async Task<Device> PostDevice(string name, string manufacturer, string type, string os, string os_Version, string processor, short ram)
        {
            Device toInsert = new Device(name, manufacturer, type, os, os_Version, processor, ram);
            string url = "https://localhost:" + serverPort.ToString() + "/api/devices";
            HttpClient client = new HttpClient();
            var serializedData = JsonSerializer.Serialize(toInsert);
            var requestBody = new StringContent(serializedData, System.Text.Encoding.UTF8, "application/json");
            var response = client.PostAsync(url, requestBody).Result;
            int responseBody = int.Parse(await response.Content.ReadAsStringAsync());
            toInsert.Id = responseBody;
            return toInsert;
        }
        public void PutDevice(Device deviceToUpdate)
        {
            string url = "https://localhost:" + serverPort.ToString() + "/api/devices/" + deviceToUpdate.Id.ToString();
            HttpClient client = new HttpClient();
            var serializedData = JsonSerializer.Serialize(deviceToUpdate);
            var requestBody = new StringContent(serializedData, System.Text.Encoding.UTF8, "application/json");
            var response = client.PutAsync(url, requestBody).Result;
        }
        public async void DeleteDevice(int device_id)
        {
            string url = "https://localhost:" + serverPort.ToString() + "/api/devices/" + device_id.ToString();
            HttpClient client = new HttpClient();
            var response = client.DeleteAsync(url).Result;
            var result = await response.Content.ReadAsStringAsync();
        }
    }
}
