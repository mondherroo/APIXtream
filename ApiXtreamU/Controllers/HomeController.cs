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
using System.Web;
using RestSharp.Extensions;
using System.Threading;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.FileProviders;
using System.Net.Http;
using Microsoft.Net.Http.Headers;
using System.Net.Mime;
using RestSharp.Serialization.Json;
using System.Timers;

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
        
        /*static async Task<string> getToken()
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
        }*/        
        [Route("")]
        [Route("/panel_api.php")]
        [Route("/player_api.php")]
        [Route("/live/{username}/{password}/{streamId}.ts")]
        [Route("/movie/{username}/{password}/{streamId}.mp4")]
        [Route("/created_live/{username}/{password}/{streamId}.ts")]
        public async Task<IActionResult> Index(string Username,string Password)
        {
            var routeName = ControllerContext.ActionDescriptor.AttributeRouteInfo.Template;
            string url = Request.QueryString.Value;
            string path = Request.Path.Value;
            string[] urlpar = url.Split("?/");
            var urlparams =  string.Join("", urlpar);
            if (urlparams != "" || !urlparams.Contains("action"))
            {
                if (routeName == "panel_api.php")
                {
                    if (urlparams != "")
                    {
                        var client = new RestClient($"http://rb-group-server.com:25461/panel_api.php{urlparams}");
                        RestRequest request = new RestRequest(Method.GET);
                        var response = await client.ExecuteAsync(request);                      
                        var obj = JsonConvert.DeserializeObject<XtreamPanel>(response.Content);                        
                        var xtp = new XtreamPanel();
                        xtp.User_info = obj.User_info;
                        xtp.Server_info = obj.Server_info;
                        xtp.Categories = obj.Categories;
                        xtp.Available_Channels = obj.Available_Channels;
                        string ob = JsonConvert.SerializeObject(xtp);
                        return Content(ob, "application/json");
                    }
                    else {
                        var client = new RestClient($"http://rb-group-server.com:25461/player_api.php?username={Username}&password={Password}");
                        RestRequest request = new RestRequest(Method.GET);
                        var response = await client.ExecuteAsync(request);
                        var obj = JsonConvert.DeserializeObject<PlayerApi>(response.Content);
                        return Ok(obj);
                    }
                }               
                else if (routeName == "player_api.php")
                {
                    var client = new RestClient($"http://rb-group-server.com:25461/player_api.php?username={Username}&password={Password}");
                    RestRequest request = new RestRequest(Method.GET);
                    var response = await client.ExecuteAsync(request);
                    var obj = JsonConvert.DeserializeObject<PlayerApi>(response.Content);
                    return Ok(obj);
                }
                else if (routeName.Contains("movie"))
                {
                    return Redirect($"http://rb-group-server.com:25461{ path }");
                }
                else if (routeName.Contains("live"))
                {
                    //string streamid = Request.Path.Value.Split('/').Last();
                    return Redirect($"http://rb-group-server.com:25461{ path }");
                }
                else if (routeName.Contains("created_live"))
                {
                    /*string streamid = Request.Path.Value.Split('/').Last();
                    string tempFile = Path.GetTempPath() + $"{streamid}";
                    using (WebClient myWebClient = new WebClient())
                    {
                        do
                        {
                            myWebClient.DownloadFileAsync(new Uri($"http://rb-group-server.com:25461{ path }"), tempFile);

                            byte[] buffer;
                            FileStream fileStream = new FileStream(tempFile, FileMode.OpenOrCreate, FileAccess.Read, FileShare.ReadWrite);
                            try
                            {
                                int length = (int)fileStream.Length;
                                buffer = new byte[length];
                                int count;
                                int sum = 0;
                                while ((count = fileStream.Read(buffer, sum, length - sum)) > 0)
                                {
                                    sum += count;
                                }                                
                            }
                            catch (Exception)
                            {
                                //fileStream.Close();
                                throw;
                            }

                            return File(buffer, "video/mp2t");
                        } while (tempFile.Length >= 0);
                    }*/
                    return Redirect($"http://rb-group-server.com:25461{ path }");
                }               
                else if (urlparams != "" && !urlparams.Contains("action"))
                {

                    var client = new RestClient($"http://rb-group-server.com:25461/");
                    RestRequest request = new RestRequest($"{urlparams}", Method.GET);
                    var response = await client.ExecuteAsync(request);
                    var obj = JsonConvert.DeserializeObject<PlayerApi>(response.Content);
                    return Ok(obj);
                }
                else if (urlparams == "" && !urlparams.Contains("action"))
                    return Ok("Access Denied");
            }           
            if (urlparams.Contains("action=get_live_categories"))
            {
                var client = new RestClient($"http://rb-group-server.com:25461/");
                RestRequest request = new RestRequest($"{urlparams}", Method.GET);
                var response = await client.ExecuteAsync(request);
                return Content(response.Content, "application/json");
            }
            if (urlparams.Contains("action=get_vod_categories"))
            {
                var client = new RestClient($"http://rb-group-server.com:25461/");
                RestRequest request = new RestRequest($"{urlparams}", Method.GET);
                var response = await client.ExecuteAsync(request);
                return Content(response.Content, "application/json");
            }
            if (urlparams.Contains("action=get_series_categories"))
            {
                var client = new RestClient($"http://rb-group-server.com:25461/");
                RestRequest request = new RestRequest($"{urlparams}", Method.GET);
                var response = await client.ExecuteAsync(request);
                return Content(response.Content, "application/json");
            }
            if (urlparams.Contains("action=get_live_streams"))
            {
                var client = new RestClient($"http://rb-group-server.com:25461/");
                RestRequest request = new RestRequest($"{urlparams}", Method.GET);
                var response = await client.ExecuteAsync(request);
                return Content(response.Content, "application/json");
            }
            if (urlparams.Contains("action=get_short_epg&stream_id"))
            {
                string stream_id = Request.Query["stream_id"].ToString();
                string username = Request.Query["/player_api.php?username"].ToString();
                string password = Request.Query["password"].ToString();
                return Redirect($"http://rb-group-server.com:25461/live/{username}/{password}/{stream_id}.ts");
            }
            if (urlparams.Contains("action=get_vod_streams&category_id"))
            {
                
                var client = new RestClient($"http://rb-group-server.com:25461/");
                RestRequest request = new RestRequest($"{urlparams}", Method.GET);
                var response = await client.ExecuteAsync(request);
                return Content(response.Content, "application/json");
                
            }
            if (urlparams.Contains("action=get_vod_info&vod_id"))
            {
                string vod_id = Request.Query["vod_id"].ToString();
                string username = Request.Query["/player_api.php?username"].ToString();
                string password = Request.Query["password"].ToString();
                var client = new RestClient($"http://rb-group-server.com:25461/");
                RestRequest request = new RestRequest($"{urlparams}", Method.GET);
                var response = await client.ExecuteAsync(request);
                if (response != null)
                {
                    return Content(response.Content, "application/json");
                }
                /*string tempFile = Path.GetTempPath() + $"{vod_id}";
                WebClient myWebClient = new WebClient();
                myWebClient.DownloadFileAsync(new Uri($"http://rb-group-server.com:25461/movie/{username}/{password}/{vod_id}.mp4"), tempFile);
                byte[] buffer;
                FileStream fileStream = new FileStream(tempFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                try
                {
                    int length = (int)fileStream.Length;
                    buffer = new byte[length];
                    int count;
                    int sum = 0;
                    while ((count = fileStream.Read(buffer, sum, length - sum)) > 0)
                        sum += count;
                }
                finally
                {
                    fileStream.Close();
                }
                return File(buffer, "video/mp4");*/
            }
            if (urlparams.Contains("action=get_series&category_id"))
            {
                var client = new RestClient($"http://rb-group-server.com:25461/");
                RestRequest request = new RestRequest($"{urlparams}", Method.GET);
                var response = await client.ExecuteAsync(request);
                return Content(response.Content, "application/json");

            }
            if (urlparams.Contains("action=get_series_info&series_id"))
            {
                string series_id = Request.Query["series_id"].ToString();
                string username = Request.Query["/player_api.php?username"].ToString();
                string password = Request.Query["password"].ToString();
                var client = new RestClient($"http://rb-group-server.com:25461/");
                RestRequest request = new RestRequest($"{urlparams}", Method.GET);
                var response = await client.ExecuteAsync(request);
                if (response != null)
                {
                    return Content(response.Content, "application/json");
                }
                /*string tempFile = Path.GetTempPath() + $"{series_id}";
                WebClient myWebClient = new WebClient();
                myWebClient.DownloadFileAsync(new Uri($"http://rb-group-server.com:25461/movie/{username}/{password}/{series_id}.mp4"), tempFile);
                byte[] buffer;
                FileStream fileStream = new FileStream(tempFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                try
                {
                    int length = (int)fileStream.Length;
                    buffer = new byte[length];
                    int count;
                    int sum = 0;
                    while ((count = fileStream.Read(buffer, sum, length - sum)) > 0)
                        sum += count;
                }
                finally
                {
                    fileStream.Close();
                }
                return File(buffer, "video/mp4");*/
            }
            return Ok("Access Denied");                        
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
