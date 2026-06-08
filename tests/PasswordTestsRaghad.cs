using Xunit;
using mcvtodoapp.Models;

namespace tests
{
    public class PasswordTests
    {
        private readonly PasswordChecker _checker;

        public PasswordTests()
        {
            _checker = new PasswordChecker();
        }

        [Fact]
        public void Test_TooShort()
        {
            var result = _checker.CheckPasswordStrength("123");
            Assert.Equal("Weak: Too short", result);
        }

        [Fact]
        public void Test_MissingUppercase()
        {
            var result = _checker.CheckPasswordStrength("abcdefgh");
            Assert.Equal("Medium: Missing uppercase", result);
        }

        [Fact]
        public void Test_MissingLowercase()
        {
            var result = _checker.CheckPasswordStrength("ABCDEFGH");
            Assert.Equal("Medium: Missing lowercase", result);
        }

        [Fact]
        public void Test_MissingDigit()
        {
            var result = _checker.CheckPasswordStrength("aBcdEfGh");
            Assert.Equal("Medium: Missing digit", result);
        }

        [Fact]
        public void Test_StrongPassword()
        {
            var result = _checker.CheckPasswordStrength("aB1cDefG");
            Assert.Equal("Strong Password", result);
        }
    }
}