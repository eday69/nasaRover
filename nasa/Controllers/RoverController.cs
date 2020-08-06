using System;
using Microsoft.AspNetCore.Mvc;
using nasa.Models;
using Newtonsoft.Json;


namespace nasa.Controllers
{
    public class RoverController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            int _grid = 0;

            _grid = getGridSize(_grid);

            if (_grid == 0)
            {
                return View("Landing");
            }
            else
            {
                ViewData["GridSize"] = _grid;
                RoverMachine[] deployedRovers = getRovers();

                return View("Index", deployedRovers);
            }
        }

        private int getGridSize(int _grid)
        {
            using (var client = new System.Net.Http.HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5002/api/");
                var responseTask = client.GetAsync("grid");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();

                    _grid = Int32.Parse(readTask.Result);
                }
                else
                {
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
                else 
                {
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }

            return _rovers;
        }

    }
}
