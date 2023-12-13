using NUnit.Framework;
using StringCalculatorKata;
using static StringCalculatorTests.StringCalculatorKataTests.StringCalculatorTests.Add;
namespace StringCalculatorTests
{
    public class StringCalculatorKataTests
    {

        //Green bar patterns:
        //  - One to Many
        //  - Obvious
        //  - Backout
        //  - Learning Test
        //TDD Gears: Low, Medium, Revers
        //
        // Medium gear -> One number + Obvious -> No Numbers +Obvious -> Many numbers + One to many -> Demo Backout +Learning

        [TestFixture]
        public class StringCalculatorTests
        {
            
            public class Add
            {
               
                public class NoNumbers
                {
                    [TestCase(null, 0)]
                    [TestCase("", 0)]
                    [TestCase(" ", 0)]
                    [TestCase("  ", 0)]
                    public void ShouldReturn0(string numbers, int expected)
                    {
                        //Arrange
                        var sut = CreateStringCalculator();
                        //Act
                        var actual = sut.Add(numbers);
                        //Assert
                        Assert.AreEqual(expected, actual);
                    }
                }
                public class OneNumber
                {

                    [TestCase("1", 1)]
                    [TestCase("15", 15)]
                    [TestCase("999", 999)]
                    public void ShouldReturnThatNumber(string numbers, int expected)
                    {
                        //Arrange
                        var sut = CreateStringCalculator();
                        //Act
                        var actual = sut.Add(numbers);
                        //Assert
                        Assert.AreEqual(expected, actual);
                    }

                }


             
                public class ManyNumbers
                {

                    [TestCase("1,2", 3)]
                    [TestCase("5,15,20", 40)]
                    [TestCase("999,1,11,2", 1013)]
                    public void ShouldReturnThoseNumbers(string numbers, int expected)
                    {
                        //Arrange
                        var sut = CreateStringCalculator();
                        //Act
                        var actual = sut.Add(numbers);
                        //Assert
                        Assert.AreEqual(expected, actual);
                    }
                    [TestCase("3,4\n5", 12)]
                    [TestCase("10\n20\n30\n40", 100)]
                    [TestCase("5\n5", 10)]
                    public void SeparatedByNewLine_ShouldReturnTheSumOfThoseNumbers(string numbers, int expected)
                    {
                        //Arrange
                        StringCalculator sut = CreateStringCalculator();
                        //Act
                        var actual = sut.Add(numbers);
                        //Assert
                        Assert.AreEqual(expected, actual);
                    }
                }


                public class CustomeDelimiters
                {

                    [TestCase("//;\n1;2", 3)]
                    [TestCase("//|\n1|2|3|4", 10)]
                    public void ShouldReturnThatNumber(string numbers, int expected)
                    {
                        //Arrange
                        var sut = CreateStringCalculator();
                        //Act
                        var actual = sut.Add(numbers);
                        //Assert
                        Assert.AreEqual(expected, actual);
                    }

                }

                public class NegativeNumbers
                {
                    [TestCase("-1", "negatives not allowed: -1")]
                    [TestCase("1,-10,2", "negatives not allowed: -10")]
                    [TestCase("10,20,-30", "negatives not allowed: -30")]
                    public void OneNegative_ShouldThrowExceptionWithAllNegativesInMessage(string numbers, string expected)
                    {

                        //Arrange
                        var sut = CreateStringCalculator(); 
                        //Act
                        var exception = Assert.Throws<Exception>(() => sut.Add(numbers));
                        //Assert
                        Assert.IsNotNull(exception);
                        Assert.AreEqual(expected, exception?.Message);
                    }

                    [TestCase("-1,-5", "negatives not allowed: -1, -5")]
                    [TestCase("1,-10,-2", "negatives not allowed: -10, -2")]
                    [TestCase("10,20,-30,40,-50,-100", "negatives not allowed: -30, -50, -100")]
                    public void MultipleNegatives_ShouldThrowExceptionWithAllNegativesInMessage(string numbers, string expected)
                    {

                        //Arrange
                        var sut = CreateStringCalculator();
                        //Act
                        var exception = Assert.Throws<Exception>(() => sut.Add(numbers));
                        //Assert
                        Assert.AreEqual(expected, exception?.Message);
                    }
                }

                public class LargeNumbers {

                    [TestCase("2,1001", 2)]
                    [TestCase("1,2,1001,3,1002,4,1003,5", 15)]
                    [TestCase("1000,1", 1001)]
                    public void ShouldBeIgnored(string numbers, int expected)
                    {

                        //Arrange
                        var sut = CreateStringCalculator();
                        //Act
                        var actual = sut.Add(numbers);
                        //Assert
                        Assert.AreEqual(expected, actual);
                    }
                }


                public class ArbitraryLengthDelimiter
                {

                    [TestCase("//[***]\n1***2***3", 6)]
                    [TestCase("//[&&]\n1&&200&&5", 206)]
                    public void ShouldBeIgnored(string numbers, int expected)
                    {

                        //Arrange
                        var sut = CreateStringCalculator();
                        //Act
                        var actual = sut.Add(numbers);
                        //Assert
                        Assert.AreEqual(expected, actual);
                    }
                }


            }
            private static StringCalculator CreateStringCalculator()
            {
                //Arrange
                return new StringCalculator();
            }

        }



        // [Test]
        // public void Leaning()
        //{
        //var exception = Assert.Throws<Exception>(() => int.Parse(""));
        // Assert.AreEqual("Junk", exception.Message);
        //Assert.AreEquals(true, string.IsNullOrEmpty("  "));
        //}
    }
}
