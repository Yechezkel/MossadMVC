using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using MossadServer.Models;

namespace MossadMVC.Controllers
{
    public class TargetsController : Controller
    {
        private readonly HttpClient _httpClient;
        public TargetsController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IActionResult> Index()
        {
            return View(await GetTargetsAsync());
        }
        public async Task<List<Target>> GetTargetsAsync()
        {
            var response = await _httpClient.GetAsync("https://localhost:7255/Targets");
            response.EnsureSuccessStatusCode();
            var targets = await response.Content.ReadFromJsonAsync<List<Target>>();
            return targets;
        }
    }
}
