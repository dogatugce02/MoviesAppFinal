using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using BLL.Controllers.Bases;
using BLL.Services;
using BLL.Models;
using BLL.DAL;
using Microsoft.AspNetCore.Authorization;

// Generated from Custom Template.

namespace MVC.Controllers
{
    public class MoviesController : MvcController
    {
        // Service injections:
        private readonly IMoviesService _moviesService;
        private readonly IDirectorService _directorService;

        public MoviesController(
            IMoviesService moviesService,
            IDirectorService directorService
        )
        {
            _moviesService = moviesService;
            _directorService = directorService;
        }

        // GET: Movies
        [AllowAnonymous]
        public IActionResult Index()
        {
            // Get collection service logic:
            var list = _moviesService.Query().ToList();
            return View(list);
        }

        // GET: Movies/Details/5
        public IActionResult Details(int id)
        {
            // Get item service logic:
            var item = _moviesService.Query();
            var result = item.SingleOrDefault(q => q.Record.Id == id);
            return View(result);
        }

        protected void SetViewData()
        {
            // Set ViewData for Directors dropdown
            ViewData["DirectorIds"] = new MultiSelectList(_directorService.Query().ToList(), "Id", "Name");
        }

        // GET: Movies/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            SetViewData();
            return View();
        }

        // POST: Movies/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult Create(MoviesModel movies)
        {
            if (ModelState.IsValid)
            {
                // Insert item service logic:
                BLL.DAL.Movies _model = new BLL.DAL.Movies();
                //_model = manuelMapping(movies);

                var result = _moviesService.Create(movies.Record);
                if (result.IsSuccessful)
                {                   

                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Details), new { id = movies.Id });
                }
                ModelState.AddModelError("", result.Message);
            }
            SetViewData();
            return View(movies);
        }

        // GET: Movies/Edit/5
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            // Get item to edit service logic:
            var item = _moviesService.Query().SingleOrDefault(q => q.Id == id);
            SetViewData();
            return View(item);
        }

        // POST: Movies/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(MoviesModel movies)
        {
            if (ModelState.IsValid)
            {
                BLL.DAL.Movies _model = new BLL.DAL.Movies();

                //_model = manuelMapping(movies);

                // Update item service logic:
                var result = _moviesService.Update(movies.Record);
                if (result.IsSuccessful)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Details), new { id = movies.Id });
                }
                ModelState.AddModelError("", result.Message);
            }
            SetViewData();
            return View(movies);
        }
        //private BLL.DAL.Movies manuelMapping(MoviesModel movies)
        //{
        //    BLL.DAL.Movies _model = new BLL.DAL.Movies();
        //    if (movies != null)
        //    {
        //        _model.TotalRevenue = movies.TotalRevenue;
        //        _model.Directors = movies.Directors;
        //        _model.Name = movies.Name;
        //        _model.DirectorId = movies.DirectorId;
        //        _model.Id = movies.Id;
        //        _model.ReleaseDate = movies.ReleaseDate;
        //    }
        //    return _model;
        //}

        // GET: Movies/Delete/5
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            // Get item to delete service logic:
            var item = _moviesService.Query().SingleOrDefault(q => q.Id == id);
            return View(item);
        }

        // POST: Movies/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            // Delete item service logic:
            var result = _moviesService.Delete(id);
            TempData["Message"] = result.Message;
            return RedirectToAction(nameof(Index));
        }
    }
}
