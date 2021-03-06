﻿using FitnessApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FitnessApp.Services
{
    public class FitnessDataStoreClient:IDataStoreClient
    {
        private readonly IHttpClientFactory _httpFactory;
        public FitnessDataStoreClient(IHttpClientFactory factoryInstance)
        {
            _httpFactory = factoryInstance;
        }
        public async Task<bool> WriteRecord(FitnessRecord record)
        {
            var client = _httpFactory.CreateClient("WRITER");
            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Post, "/api/fitness-data");
            var requestJson = JsonConvert.SerializeObject(record);
            message.Content = new StringContent(requestJson,Encoding.UTF8,"application/json");
            var response = await client.SendAsync(message);
            return response.IsSuccessStatusCode;
        }
        public async Task<List<FitnessRecord>> GetAllRecords()
        {
            var client = _httpFactory.CreateClient("READER");
            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Get, "/api/fitness-data");
            var response = await client.SendAsync(message);
            if (!response.IsSuccessStatusCode)
            {
                return new List<FitnessRecord>();
            }
            var json = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<FitnessRecord>>(json);
            return result;
        }
        public async Task<List<FitnessRecord>> GetRecordsByWorkoutType(string workoutType)
        {
            var client = _httpFactory.CreateClient("READER");
            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Get, $"/api/fitness-data/type/{workoutType}");
            var response = await client.SendAsync(message);
            if (!response.IsSuccessStatusCode)
            {
                return new List<FitnessRecord>();
            }
            var json = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<FitnessRecord>>(json);
            return result;
        }
        public async Task<FitnessRecord> GetRecordByTitle(string title)
        {
            var client = _httpFactory.CreateClient("Reader");
            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Get, $"/api/fitness-data/{title}");
            var response = await client.SendAsync(message);
            if (!response.IsSuccessStatusCode)
            {
                return new FitnessRecord();
            }
            var json = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<FitnessRecord>(json);
            return result;
        }
        public async Task<bool> UpdateRecord(string title, string newComment)
        {
            var client = _httpFactory.CreateClient("WRITER");
            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Patch, $"/api/fitness-data/{title}/comments");
            message.Content = new StringContent($"\"{newComment}\"", Encoding.UTF8, "application/json");
            var response = await client.SendAsync(message);
            return response.IsSuccessStatusCode;
        }
        public async Task<bool> DeleteRecord(string title)
        {
            var client = _httpFactory.CreateClient("WRITER");
            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Delete, $"/api/fitness-data/{title}");
            var response = await client.SendAsync(message);
            return response.IsSuccessStatusCode;
        }
        
    }
}
