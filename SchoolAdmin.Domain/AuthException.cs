using System;

namespace SchoolAdmin.Domain
{
    public class AuthException : Exception
    {
        public AuthException(string err) : base(err)
        {               
        }
    }
}
