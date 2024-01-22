using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotTrainigator.UI.Services
{
    public class TimetablesService
    {
        private readonly HttpClient? _httpClient;
        private string? _db_client_id;
        private string? _db_api_key;
        public TimetablesService()
        {
            if (ReadConfig())
            {
                _httpClient = new HttpClient
                {
                    BaseAddress = new Uri("https://apis.deutschebahn.com/db-api-marketplace/apis/timetables/v1/station/")
                };
                _httpClient.DefaultRequestHeaders.Add("Accept", "application/xml");
                _httpClient.DefaultRequestHeaders.Add("DB-Client-Id", _db_client_id);
                _httpClient.DefaultRequestHeaders.Add("DB-Api-Key", _db_api_key);
            }
        }

        private bool ReadConfig()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("secrets.json", true)
                .Build();

            var apiSection = config.GetSection("DB_API");

            _db_client_id = apiSection.GetSection("Client_Id").Value;
            _db_api_key = apiSection.GetSection("Client_Key").Value;

            return !String.IsNullOrEmpty(_db_client_id) && !String.IsNullOrEmpty(_db_api_key);

        }
    }
}
