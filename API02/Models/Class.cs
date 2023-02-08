namespace API02.Models
{
    public class ReqResRoot
    {
        public int Page;
        public int Total;
        public int TotalPages;
        public int PerPage;
        public List<ReqResUser> Data;
    }

    public class ReqResUserRoot
    {
        public ReqResUser Data;
    }

    public class ReqResUser
    {
        public int Id;
        public string Email;
        public string FirstName;
        public string LastName;
        public string Avatar;
    }
}
