using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MossadMVC.Models;
using MossadServer.Models;

namespace MossadMVC.Controllers
{
    public class InfoController : Controller
    {
        private readonly HttpClient _httpClient;
        public InfoController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // GET: InfoController
        public async Task<IActionResult> Index()
        {
            Info info = await GetInformation();
            return View(info);
        }

        private async Task<Info> GetInformation()
        {
            Info info = new Info();

            List<Agent> agents = await GetAgentsAsync();
            info.SumTotalAgents = agents.Count;
            foreach (Agent agent in agents) 
            {
                if (agent.IsActive)
                    info.SumActiveAgents++;
            }

            List<Target> targets = await GetTargetsAsync();
            info.SumTotalTargets = targets.Count;
            foreach (Target target in targets)
            {
                if (target.IsActive)
                    info.SumActiveTargets++;
            }

            if (info.SumTotalTargets != 0)
                info.TotalRatio = (double)info.SumTotalAgents / info.SumTotalTargets;

            return info;
        }


        //מכאן והלאה פונקציות כפולות להוציא לתיקייה חיצונית
        public async Task<List<Agent>> GetAgentsAsync()
        {
            var response = await _httpClient.GetAsync("https://localhost:7255/Agents");
            response.EnsureSuccessStatusCode();
            var agents = await response.Content.ReadFromJsonAsync<List<Agent>>();
            //agents = await LoadActiveMissionId(agents);
            return agents;
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