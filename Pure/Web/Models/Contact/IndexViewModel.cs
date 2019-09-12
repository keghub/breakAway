namespace BreakAway.Models.Contact
{
    public class IndexViewModel
    {
        public ContactItem[] Contacts { get; set; }
        public FilterItem FilterViewModel { get; set; }
        public PageItem PageViewModel { get; set; }
    }

    public class ContactItem
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Title { get; set; }

    }
     
    public class PageItem
    {
        public int Page { get; set; }
        public int TotalPage { get; set; }
    }

    public class FilterItem
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
    }
}