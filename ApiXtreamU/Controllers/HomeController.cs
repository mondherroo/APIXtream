using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ApiXtreamU.Models;
using RestSharp;
using System.Net;
using ApiXtream.Models;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using RestSharp.Deserializers;
using System.IO;
using RestSharp.Authenticators;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;

namespace ApiXtreamU.Controllers
{
    public class HomeController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private DataBContext dataBContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _config;

        public HomeController(IConfiguration config ,UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, DataBContext db)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            dataBContext = db;
            _config = config;
        }

        static async Task<string> getToken()
        {
            var client = new RestClient("https://localhost:44381/Token/Index");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("accept", "application/json");
            request.AddHeader("content-type", "application/json");
            request.AddJsonBody(new { Email = "mondheriron6@gmail.com", PasswordHash = "Azerty123*" });
            IRestResponse response = await client.ExecuteAsync(request);
            var ob = response.Content.Trim('"');
            return ob;
        }
        public async Task<IActionResult> Index(User userr)
        {
            
            var client = new RestClient($"https://localhost:44381");
            var request = new RestRequest("/Users/user", Method.POST);
            var user = new User
            {
              Username = "mondhero",
              Password = "tygfhtrt",
              Reseller = "admin",
              Expiration = new DateTime(2019, 12, 30),
              Active = 0,
              Conns = 1,
              AdminNotes ="ty",
              ResellerNotes ="uy"
            };
            request.AddJsonBody(user);
            request.AddHeader("content-type", "application/json");
            var tok = await getToken();
            request.AddHeader("Authorization", "Bearer " + tok);                    
            var response = await client.ExecuteAsync(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                ApiSuccess[] body = new ApiSuccess[]
                {
                new  ApiSuccess{ Result = true ,Created_Id = dataBContext.User.Max(p=>p.UserId), Username = user.Username, Password = user.Password } };
                request.RequestFormat = DataFormat.Json;
                request.AddJsonBody(body);
                var deserializer = new JsonDeserializer();
                var json = request.JsonSerializer.Serialize(body);
                return Ok(json);
            }
            else { 
            APIFailed[] Body = new APIFailed[]
            {
            new  APIFailed{ Result = false ,Error = "PARAMETER ERROR"} };
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(Body);
            var deserializerr = new JsonDeserializer();
            var jsonn = request.JsonSerializer.Serialize(Body);
            return Ok(jsonn);
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
