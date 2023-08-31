namespace SQLiteWebApi.Model
{
    public class AppUser
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string CreatedDate { get; set; } //Format YYYY-MM-dd HH:mm:ss
    }
}
