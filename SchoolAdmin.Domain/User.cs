namespace SchoolAdmin.Domain
{
    public static class User
    {
        public enum UserType{ Teacher, Admin, Student }

        public class DefaultUser
        {
            public string Username { get; set; }
            public string Password { get; set; }
            public UserType Type { get; set; }
        }
        public class Admin : DefaultUser
        {
            public Admin()
            {
                this.Type = UserType.Admin;
            }
        }
    }
}