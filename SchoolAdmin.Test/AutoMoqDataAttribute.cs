
using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;
using System;

namespace SchoolAdmin.Test
{
    public class AutoMoqDataAttribute : AutoDataAttribute
    {
        public AutoMoqDataAttribute()
            : base(new Func<IFixture>(() =>
            {
                return new Fixture()
            .Customize(new AutoMoqCustomization());
            }
            ))
        {
        }
    }
}
