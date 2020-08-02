using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using nasa.Models;
using System.Net.Http;
using Newtonsoft.Json;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace nasa.Controllers
{
    public class RoverController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        //public string Index()
        {
            int _grid = 0;

            _grid = getGridSize(_grid);

            ViewData["GridSize"] = _grid;
            RoverMachine[] deployedRovers = getRovers();
            //ViewData["Rovers"] = getRovers();
            return View(deployedRovers);
        }

        private int getGridSize(int _grid)
        {
            using (var client = new System.Net.Http.HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5002/api/");
                //HTTP GET
                var responseTask = client.GetAsync("grid");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();

                    _grid = Int32.Parse(readTask.Result);
                }
                else //web api sent error response 
                {
                    //log response status here..

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }

            return _grid;
        }

        private RoverMachine[] getRovers()
        {
            RoverMachine[] _rovers = new RoverMachine[0];

            using (var client = new System.Net.Http.HttpClient())
            {

                client.BaseAddress = new Uri("http://localhost:5002/api/");
                //HTTP GET
                var responseTask = client.GetAsync("rovers");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();

                    string json = readTask.Result;

                    _rovers = JsonConvert.DeserializeObject<RoverMachine[]>(json);
                }
                else //web api sent error response 
                {
                    //log response status here..

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }

            return _rovers;
        }

    }
}
