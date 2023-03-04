using Popov.Fibonacci.Abstract;

namespace Popov.Fibomacci.Domain.Tests
{
    public class FibonacciServiceTest
    {
        static IEnumerable<TestCaseData> Cases
        {
            get
            {
                yield return new TestCaseData(new FibonacciNumberDto { Index = 0 })
                    .SetName("First")
                    .Returns(1);

                yield return new TestCaseData(new FibonacciNumberDto { Index = 1, Value = 1 })
                    .SetName("Second")
                    .Returns(1);

                yield return new TestCaseData(new FibonacciNumberDto { Index = 2, Value = 1 })
                    .SetName("Third")
                    .Returns(2);

                yield return new TestCaseData(new FibonacciNumberDto { Index = 13, Value = 233 })
                    .SetName("Fourteenth")
                    .Returns(377);

            }
        }

        private FibonacciService Service { get; set; }

        [OneTimeSetUp]
        public void Initialize()
        {
            Service = new FibonacciService();
        }

        [TestCaseSource(nameof(Cases))]
        public long GetNext_CorrectTest(FibonacciNumberDto current)
        {
            var next = Service.GetNext(current);

            return next.Value;
        }

        [Test]
        public void GetNext_OverflowedTest()
        {
            var preoverflowedNumber = new FibonacciNumberDto
            {
                CalcId = Guid.NewGuid(),
                Index = 92,
                Value = 7540113804746348544
            };

            Assert.Throws<OverflowException>(() => Service.GetNext(preoverflowedNumber));
        }
    }
}