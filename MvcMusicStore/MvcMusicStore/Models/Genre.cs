using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcMusicStore.Models
{
    public class Genre
    {
        public string Name { get; set; } //this syntax auto-generates getter/setter
        public int GenreId { get; set; }
        public string Description { get; set; }
        public List<Album> Albums { get; set; }
    }
}