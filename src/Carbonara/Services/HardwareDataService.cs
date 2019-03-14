using System;
using System.Threading.Tasks;
using Carbonara.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Linq;
using Carbonara.Models.MiningHardware;
using System.Collections.Generic;

namespace Carbonara.Services
{
    public class HardwareDataService
    {
        private readonly IConfiguration _configuration;

        public HardwareDataService()
        {
        }

        public HardwareDataService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<MiningDevice> GetHardwareList()
        {
            var devices = _configuration.GetSection("MiningHardware").Get<List<MiningDevice>>();
            return devices;
        }


    }
}