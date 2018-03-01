using System;

namespace SchoolAdmin.Domain
{
    public class UpdateException : Exception
    {
        public UpdateException(string err) : base(err)
        {
        }
    }
}
