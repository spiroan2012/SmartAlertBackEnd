using Core.Entities;
using Core.Interfaces;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Infrastructure.Utilities;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Infrastructure.Services
{
    public class FirebaseService : IFirebaseService
    {
        private IFirebaseClient _firebaseClient;
        public FirebaseService(IConfiguration configuration)
        {
            var options = configuration.GetSection("FirebaseSettings").Get<FirebaseSettings>();
            IFirebaseConfig conf = new FirebaseConfig
            {
                AuthSecret = options.AuthSecret,
                BasePath = options.BasePath
            };

            _firebaseClient = new FireSharp.FirebaseClient(conf);

        }

        public async Task<Dictionary<string, FirebaseUser>> GetAllUsers()
        {


            FirebaseResponse response = await _firebaseClient.GetAsync("Users");
            Dictionary<string, FirebaseUser> users = response.ResultAs<Dictionary<string, FirebaseUser>>();
            return users;
            //dynamic data = JsonConvert.DeserializeObject<dynamic>(response.Body);

            //var list = new List<FirebaseUser>();

            //if (data != null)
            //{
            //    foreach (var user in data)
            //    {
            //        list.Add(JsonConvert.DeserializeObject<FirebaseUser>(((JProperty)user).Value.ToString()));
            //    }
            //}

            //return list;
        }
    }
}
