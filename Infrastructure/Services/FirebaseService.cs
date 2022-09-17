using Core.Entities;
using Core.Interfaces;
using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Infrastructure.Utilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Nest;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class FirebaseService : IFirebaseService
    {
        private  IFirebaseClient _firebaseClient;
        public FirebaseService( IConfiguration configuration)
        {
            var options = configuration.GetSection("FirebaseSettings").Get<FirebaseSettings>();
            IFirebaseConfig conf = new FirebaseConfig
            {
                AuthSecret = options.AuthSecret,
                BasePath = options.BasePath
            };

            _firebaseClient = new FireSharp.FirebaseClient(conf);

        }

        public async Task<IReadOnlyList<FirebaseUser>> GetAllUsers()
        {


            var response = await _firebaseClient.GetAsync("Users");

            dynamic data = JsonConvert.DeserializeObject<dynamic>(response.Body);

            var list = new List<FirebaseUser>();

            if(data != null)
            {
                foreach(var user in data)
                {
                    list.Add(JsonConvert.DeserializeObject<FirebaseUser>(((JProperty)user).Value.ToString()));
                }
            }

            return list;
        }
    }
}
