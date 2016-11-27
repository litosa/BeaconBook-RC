using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BeaconBookingTest
{
    public class BeaconTest
    {
        public BeaconTest()
        {
            
        }
        [Fact]
        public void PassingTest()
        {
            Assert.Equal(4, Add(2, 2));
        }

        [Theory]
        [InlineData(4, 5, 9)]
        [InlineData(2, 3, 5)]
        public void TestAddNumbers(int x, int y, int expectedResult)
        {
            var result = Add(x, y);
            Assert.Equal(expectedResult, result);
        }

        int Add(int x, int y)
        {
            return x + y;
        }
    }
}