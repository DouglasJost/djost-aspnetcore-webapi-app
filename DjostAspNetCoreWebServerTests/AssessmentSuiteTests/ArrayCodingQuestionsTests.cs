using System;
using System.Linq;
using AssessmentSuiteLibrary.Services;

namespace DjostAspNetCoreWebServerTests.AssessmentSuiteTests
{
    [TestFixture]
    public class ArrayCodingQuestionsTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        [TestCase(new char[] { '1', '2' })]
        [TestCase(new char[] { '9', '5', '0', '3' })]
        [TestCase(new char[] { '9', '5', '0', '7' })]
        public void ArrayContainsOnlyDigits_IsTrue(char[] charArray)
        {
            var service = new ArrayCodingQuestionsService();
            Assert.That(service.ArrayContainsOnlyDigits(charArray), Is.True);
        }

        [Test]
        [TestCase(new char[] { '1', '2', 'a' })]
        [TestCase(new char[] { '9', '.', '0', '3' })]
        [TestCase(new char[] { '9', '5', '@', '7' })]
        public void ArrayContainsOnlyDigits_IsFalse(char[] charArray)
        {
            var service = new ArrayCodingQuestionsService();
            Assert.That(service.ArrayContainsOnlyDigits(charArray), Is.False);
        }

        [Test]
        [TestCase(new int[] { 5, 6, 7, 8 }, null)]
        [TestCase(new int[] { 5, 6, 7, 8, 32, -3 }, null)]
        [TestCase(new int[] { 5, 6, 7, 8, 32, 6 }, 6)]
        public void FindDuplicateNumber(int[] numbers, int? duplicateNumber)
        {
            var service = new ArrayCodingQuestionsService();
            Assert.That(service.FindDuplicateNumber(numbers), Is.EqualTo(duplicateNumber));
        }

        [Test]
        [TestCase(new int[] { 5, 7, 8, 3, 7, 5 }, new int[] { 5, 7 })]
        public void FindDuplicateNumbers(int[] numbers, int[] results)
        {
            var service = new ArrayCodingQuestionsService();
            var dups = service.FindDuplicateNumbers(numbers);

            Assert.That(dups.Count(), Is.EqualTo(results.Count()));
            Assert.That(dups[0], Is.EqualTo(results[0]));
            Assert.That(dups[1], Is.EqualTo(results[1]));
        }

        [Test]
        [TestCase(new int[] { 2, 50, -3, 0, -1, 1 }, -3, 50)]
        [TestCase(new int[] { 200, 300, 25, 301 }, 25, 301)]
        public void FindMinMaxNumbers(int[] numbers, int? minNumber, int? maxNumber)
        {
            var service = new ArrayCodingQuestionsService();
            service.FindMinMaxNumber(numbers, out int? min, out int? max);

            Assert.That(min, Is.EqualTo(minNumber));
            Assert.That(max, Is.EqualTo(maxNumber));
        }

        [Test]
        [TestCase(new int[] { 1, 2, 3, -1, 2, 77, 1 }, new int[] { 1, 2, 3, -1, 77 })]
        public void RemoveDuplicateNumbers(int[] numbers, int[] results)
        {
            var service = new ArrayCodingQuestionsService();
            var uniqueNumbers = service.RemoveDuplicateNumbers(numbers);

            Assert.That(uniqueNumbers.Count(), Is.EqualTo(results.Count()));

            for (int i = 0; i < results.Count(); i++)
            {
                Assert.That(uniqueNumbers[i], Is.EqualTo(results[i]));
            }
        }

        [Test]
        [TestCase(new int[] { 1, 2, 3, 4, 5, 5, 4, 3, 2, 1 }, new int[] { 1, 2, 3, 4, 5 })]
        [TestCase(new int[] { 1, 2, 2, 1 }, new int[] { 1, 2 })]
        public void RemoveDuplicateNumbersNoLibrary(int[] numbers, int[] results)
        {
            var service = new ArrayCodingQuestionsService();
            var uniqueNumbers = service.RemoveDuplicateNumbersNoLibraries(numbers);

            Assert.That(uniqueNumbers.Count(), Is.EqualTo(results.Count()));
            for (int i = 0; i < results.Count(); i++)
            {
                Assert.That(uniqueNumbers[i], Is.EqualTo(results[i]));
            }
        }

        [Test]
        [TestCase("hello", "olleh")]
        [TestCase("ohio", "oiho")]
        public void ReverseString(string str, string result)
        {
            var service = new ArrayCodingQuestionsService();
            var reversedStr = service.ReverseString(str);
            Assert.That(reversedStr, Is.EqualTo(result));
        }
    }
}
