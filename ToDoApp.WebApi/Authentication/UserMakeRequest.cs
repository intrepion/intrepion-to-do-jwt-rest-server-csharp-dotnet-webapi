namespace ToDoApp.WebApi.Authentication
{
    public class UserMakeRequest
    {
        public string? Confirm { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? UserName { get; set; }
    }
}
