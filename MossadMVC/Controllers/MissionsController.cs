using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using MossadServer.Models;

namespace MossadMVC.Controllers
{
    public class MissionsController : Controller
    {
        private readonly HttpClient _httpClient;
        public MissionsController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        
        //// GET: Missions
        //public async Task<IActionResult> Index()
        //{
        //    return View(await GetMissionsAsync(null));
        //}


        // GET: Missions
        public async Task<IActionResult> Index()
        {
            return View(await GetMissionsAsync());
        }


        

        // GET: Missions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var mission = await GetMissionAsync((int)id);
            if (mission == null)
            {
                return NotFound();
            }
            return View(mission);
        }



        //// GET: Missions/Create
        //public IActionResult Create()
        //{
        //    return View(); 
        //}


        public async Task<IActionResult> Confirm(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Mission? mission = await GetMissionAsync((int) id);
            if (mission == null)
            {
                return RedirectToAction("Index");
            }
            mission.Confirmed=true;
            mission.CreationTime= DateTime.Now;
            mission.Agent.IsActive=true;
            var response = await _httpClient.PutAsJsonAsync($"https://localhost:7255/Missions/confirm/{id}", mission);
            response.EnsureSuccessStatusCode();//לקבל האם הצליח או לא ואם התרחק כבר 200 מטר
            return RedirectToAction("Index");
        }

        // GET: Missions
        public async Task<IActionResult> GetMissionsByStatus(string status)
        {
            ViewBag.Status = status;
            return View(await GetMissionsAsync(status));
        }






















        public async Task<List<Mission>> GetMissionsAsync()
        {
            string url = "https://localhost:7255/Missions";
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var mission = await response.Content.ReadFromJsonAsync<List<Mission>>();
            return mission;
        }

        public async Task<Mission> GetMissionAsync(int id)
        {
            string url = "https://localhost:7255/Missions/" + id;
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var mission = await response.Content.ReadFromJsonAsync<List<Mission>>();
            if (mission != null)
                return mission[0];
            return null;
        }

        public async Task<List<Mission>> GetMissionsAsync(string status)
        {
            string url = "https://localhost:7255/Missions/" + status;
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var mission = await response.Content.ReadFromJsonAsync<List<Mission>>();
            return mission;
        }




















        //// POST: Missions/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,CreationTime,ExecutionTime,Confirmed,TimeLeft")] Mission mission)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(mission);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(mission);
        //}

        //// GET: Missions/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var mission = await _context.Mission.FindAsync(id);
        //    if (mission == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(mission);
        //}

        //// POST: Missions/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,CreationTime,ExecutionTime,Confirmed,TimeLeft")] Mission mission)
        //{
        //    if (id != mission.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(mission);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!MissionExists(mission.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(mission);
        //}

        //// GET: Missions/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var mission = await _context.Mission
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (mission == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(mission);
        //}

        //// POST: Missions/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var mission = await _context.Mission.FindAsync(id);
        //    if (mission != null)
        //    {
        //        _context.Mission.Remove(mission);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool MissionExists(int id)
        //{
        //    return _context.Mission.Any(e => e.Id == id);
        //}
    }
}
