using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcMusicStore.Models; //we don’t need to type MvcMusicStore.Models.Album every time we want to use the album class

namespace MvcMusicStore.Controllers
{
    public class StoreController : Controller
    {
        //this is an instance of our Entity Framework(orm) class
        MusicStoreEntities storeDB = new MusicStoreEntities();
        //
        // GET: /Store/
        public ActionResult Index()
        {
            var genres = storeDB.Genres.ToList();
            return View(genres);
        }
        //GET: /Store/Browse?genre=XXX
        public ActionResult Browse(string genre) //genre will be a url query string that this method accepts
        {
            //htmlencode will sanitize user input from the query string

            //retrieve a genre and its associated albums from DB
            var genreModel = storeDB.Genres.Include("Albums").Single(g => g.Name == genre);
            return View(genreModel);
        }
        //GET: /Store/Details/id
        public ActionResult Details(int id)
        {
            //asp.net automatically interperets a number after the url as an ID value
            //ASP.NET MVC’s default routing convention is to treat the segment of a URL after the action method name as a parameter named “ID”
            var album = storeDB.Albums.Find(id);
            return View(album);
        }
        //
        // GET: /Store/GenreMenu
        [ChildActionOnly]
        public ActionResult GenreMenu()
        {
            var genres = storeDB.Genres.ToList();
            return PartialView(genres);
        }

    }
}
