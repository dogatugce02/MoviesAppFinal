using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
        using BLL.Controllers.Bases;
        using BLL.Services;
        using BLL.Models;

// Generated from Custom Template.

namespace MVC.Controllers
{
    public class MovieGenresController : MvcController
    {
        // Service injections:
        private readonly IMovieGenreService _movieGenreService;
    private readonly IGenreService _genreService;
    private readonly IMoviesService _movieService;

        /* Can be uncommented and used for many to many relationships. ManyToManyRecord may be replaced with the related entiy name in the controller and views. */
        //private readonly IManyToManyRecordService _ManyToManyRecordService;

        public MovieGenresController(
			IMovieGenreService movieGenreService
    , IGenreService genreService
    , IMoviesService movieService

            /* Can be uncommented and used for many to many relationships. ManyToManyRecord may be replaced with the related entiy name in the controller and views. */
            //, IManyToManyRecordService ManyToManyRecordService
        )
        {
_movieGenreService = movieGenreService;
    _genreService = genreService;
    _movieService = movieService;

            /* Can be uncommented and used for many to many relationships. ManyToManyRecord may be replaced with the related entiy name in the controller and views. */
            //_ManyToManyRecordService = ManyToManyRecordService;
        }

        // GET: MovieGenres
        public IActionResult Index()
        {
            // Get collection service logic:
            var list = _movieGenreService.Query().ToList();
            return View(list);
        }

        // GET: MovieGenres/Details/5
        public IActionResult Details(int id)
        {
            // Get item service logic:
            var item = _movieGenreService.Query().SingleOrDefault(q => q.Record.Id == id);
            return View(item);
        }

        protected void SetViewData()
        {
            // Related items service logic to set ViewData (Record.Id and Name parameters may need to be changed in the SelectList constructor according to the model):
        ViewData["GenreId"] = new SelectList(_genreService.Query().ToList(), "Record.Id", "Name");
        ViewData["MovieId"] = new SelectList(_movieService.Query().ToList(), "Record.Id", "Name");

            /* Can be uncommented and used for many to many relationships. ManyToManyRecord may be replaced with the related entiy name in the controller and views. */
            //ViewBag.ManyToManyRecordIds = new MultiSelectList(_ManyToManyRecordService.Query().ToList(), "Record.Id", "Name");
        }

        // GET: MovieGenres/Create
        public IActionResult Create()
        {
            SetViewData();
            return View();
        }

        // POST: MovieGenres/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(MovieGenreModel movieGenre)
        {
            if (ModelState.IsValid)
            {
    // Insert item service logic:
    var result = _movieGenreService.Create(movieGenre.Record);
    if (result.IsSuccessful)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Details), new { id = movieGenre.Record.Id });
                }
                ModelState.AddModelError("", result.Message);
            }
            SetViewData();
            return View(movieGenre);
        }

        // GET: MovieGenres/Edit/5
        public IActionResult Edit(int id)
        {
            // Get item to edit service logic:
            var item = _movieGenreService.Query().SingleOrDefault(q => q.Record.Id == id);
            SetViewData();
            return View(item);
        }

        // POST: MovieGenres/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(MovieGenreModel movieGenre)
        {
            if (ModelState.IsValid)
            {
    // Update item service logic:
    var result = _movieGenreService.Update(movieGenre.Record);
    if (result.IsSuccessful)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Details), new { id = movieGenre.Record.Id });
                }
                ModelState.AddModelError("", result.Message);
            }
            SetViewData();
            return View(movieGenre);
        }

        // GET: MovieGenres/Delete/5
        public IActionResult Delete(int id)
        {
            // Get item to delete service logic:
            var item = _movieGenreService.Query().SingleOrDefault(q => q.Record.Id == id);
            return View(item);
        }

        // POST: MovieGenres/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            // Delete item service logic:
            var result = _movieGenreService.Delete(id);
            TempData["Message"] = result.Message;
            return RedirectToAction(nameof(Index));
        }
    }
}
