namespace ToDoApp.WebApi.Authentication
{
    public class UserEditRequest
    {
        public string? Confirm { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? UserName { get; set; }
    }
}
