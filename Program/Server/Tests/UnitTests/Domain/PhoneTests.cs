using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Domain.Entity;
using Xunit;

namespace UnitTests.Domain
{
    public class PhoneTests
    {
        [Theory]
        [InlineData("")]
        [InlineData("       ")]
        [InlineData("+7123xxx789")]
        [InlineData("+1234567890")]
        [InlineData("+71)234(56789")]
        [InlineData("+712()3456789")]
        [InlineData("+7-1-2-3-4-5-6-7-8-9")]
        public void Phone_New_ThrowException(string phoneString)
        {
            Assert.Throws<ArgumentException>(() => new Phone(phoneString));
        }
        [Theory]
        [InlineData("+71234567890")]
        [InlineData("+7(123)4567890")]
        [InlineData("81234567890")]
        [InlineData("8(123)4567890")]
        public void Phone_New_ReturnValue(string phoneString)
        {
            Phone phone = new (phoneString);
            Assert.Equal(phoneString, phone.Value);
        }
        [Theory]
        [InlineData("+71234567890")]
        [InlineData("+7(123)4567890")]
        [InlineData("81234567890")]
        [InlineData("8(123)4567890")]
        public void Phone_ToString_ReturnValue(string phoneString)
        {
            Phone phone = new (phoneString);
            Assert.Equal(phoneString, phone.ToString());
        }
    }
}