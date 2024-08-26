using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using MossadServer.Models;

namespace MossadMVC.Controllers
{
    public class AgentsController : Controller
    {
        private readonly HttpClient _httpClient;
        public AgentsController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IActionResult> Index()
        {
            return View(await GetAgentsAsync());
        }
        public async Task<List<Agent>> GetAgentsAsync()
        {
            var response = await _httpClient.GetAsync("https://localhost:7255/Agents");
            response.EnsureSuccessStatusCode();
            var agents = await response.Content.ReadFromJsonAsync<List<Agent>>();
            agents = await LoadActiveMissionId(agents);
            return agents;
        }
        private async Task<List<Agent>> LoadActiveMissionId(List<Agent> agents)
        {
            if (agents != null)
            {
                var response = await _httpClient.GetAsync("https://localhost:7255/Missions/Active");
                response.EnsureSuccessStatusCode();
                var missions = await response.Content.ReadFromJsonAsync<List<Mission>>();
                foreach (Agent agent in agents)
                {
                    if (agent.IsActive)
                    {
                        foreach (Mission missoin in missions)
                        {
                            if (agent.Id == missoin.Agent.Id)
                                agent.ActiveMissionId = missoin.Id;
                        }
                    }
                }
            }
            return agents;
        }
    }
}
