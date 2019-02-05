using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BreakAway.Models.Sites
{
    public class IndexViewModel
    {
        public IReadOnlyList<SiteItem> Sites { get; set; }
    }

    public class SiteItem
    {
        public int Id { get; set; }
        public string Domain { get; set; }
        public bool IsActive { get; set; }
        public string Type { get; set; }
    }
}