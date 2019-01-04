using System;
using Xunit;
using Prime.Service;

namespace Prime.UnitTests.Services
{
    public class PrimeService_IsPrimeShould
    {
        private readonly PrimeService _primeService;

        public PrimeService_IsPrimeShould(){
            _primeService = new PrimeService();
        }

        [Fact]
        public void ReturnFalseGivenValueOf1()
        {
            var result = _primeService.isPrime(1);

            Assert.False(result, "1 should not be prime");            
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(1)]
        public void ReturnFalseGivenValueLessThan2(int value){
            var result = _primeService.isPrime(value);

            Assert.False(result, $"{value} should not be prime");  
        }
    }
}
