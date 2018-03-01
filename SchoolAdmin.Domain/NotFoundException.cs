using System;

namespace SchoolAdmin.Domain
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string v) : base(v)
        {
        }
    }
}
