using DDDInPractice.Business;
using FluentAssertions;

namespace DDDInPractice.Tests
{
    public class MoneySpecs
    {
        [Fact]
        public void SumOfTwoMoneysProducesCorrectResult()
        {
            var money1 = new Money(1, 2, 3, 4, 5, 6);
            var money2 = new Money(1, 2, 3, 4, 5, 6);

            var sum = money1 + money2;

            sum.OneCentCount.Should().Be(2);
            sum.TenCentCount.Should().Be(4);
            sum.QuarterCount.Should().Be(6);
            sum.OneDollarCount.Should().Be(8);
            sum.FiveDollarCount.Should().Be(10);
            sum.TwentyDollarCount.Should().Be(12);
        }

        [Fact]
        public void TwoMoneyInstancesEqualIfTheyHaveContainSameMoneyAmount()
        {
            var money1 = new Money(1, 2, 3, 4, 5, 6);
            var money2 = new Money(1, 2, 3, 4, 5, 6);

            money1.Should().Be(money2);
            money1.GetHashCode().Should().Be(money2.GetHashCode());
        }

        [Fact]
        public void TwoMoneyInstancesDoNotEqualIfTheyContainDifferenctAmounts()
        {
            var dollar = new Money(0, 0, 0, 1, 0, 0);
            var hundredCents = new Money(0, 10, 0, 0, 0, 0);

            dollar.Should().NotBe(hundredCents);
            dollar.GetHashCode().Should().NotBe(hundredCents.GetHashCode());
        }

        [Theory]
        [InlineData(-1, 0, 0, 0, 0, 0)]
        [InlineData(0, -2, 0, 0, 0, 0)]
        [InlineData(0, 0, -3, 0, 0, 0)]
        [InlineData(0, 0, 0, -4, 0, 0)]
        [InlineData(0, 0, 0, 0, -5, 0)]
        [InlineData(0, 0, 0, 0, 0, -6)]
        public void MoneyCannotCreateMoneyWithNegativeValue(
            int oneCentCount,
            int tenCentCOunt,
            int quarterCount,
            int oneDollarCount,
            int fiveDollarCount,
            int twentyDollaryCount)
        {
            Action action = () => new Money(
                oneCentCount,
                tenCentCOunt,
                quarterCount,
                oneDollarCount,
                fiveDollarCount,
                twentyDollaryCount);

            action.Should().Throw<InvalidOperationException>();
        }

        [Theory]
        [InlineData(0, 0, 0, 0, 0, 0, 0)]
        [InlineData(1, 0, 0, 0, 0, 0, 0.01)]
        //[InlineData(1, 2, 0, 0, 0, 0, 0.21)]
        [InlineData(1, 2, 3, 0, 0, 0, 0.96)]
        [InlineData(1, 2, 3, 4, 0, 0, 4.96)]
        [InlineData(1, 2, 3, 4, 5, 0, 29.96)]
        [InlineData(1, 2, 3, 4, 5, 6, 149.96)]
        [InlineData(11, 0, 0, 0, 0, 0, 0.11)]
        [InlineData(110, 0, 0, 0, 100, 0, 501.1)]
        public void AmountIsCalculatedCorrectly(
             int oneCentCount,
            int tenCentCOunt,
            int quarterCount,
            int oneDollarCount,
            int fiveDollarCount,
            int twentyDollaryCount,
            double expectedAmount)
        {
            var money = new Money(
                oneCentCount,
                tenCentCOunt,
                quarterCount,
                oneDollarCount,
                fiveDollarCount,
                twentyDollaryCount);

            money.Amount.Should().Be(expectedAmount);
        }

        [Fact]
        public void SubtractionOfTwoMoneysProducesCorrectResult()
        {
            var money1 = new Money(10, 10, 10, 10, 10, 10);
            var money2 = new Money(1, 2, 3, 4, 5, 6);

            var result = money1 - money2;

            result.OneCentCount.Should().Be(9);
            result.TenCentCount.Should().Be(8);
            result.QuarterCount.Should().Be(7);
            result.OneDollarCount.Should().Be(6);
            result.FiveDollarCount.Should().Be(5);
            result.TwentyDollarCount.Should().Be(4);
        }

        [Fact]
        public void CannotSubtractMoreThanExists()
        {
            var money1 = new Money(0, 1, 0, 0, 0, 0);
            var money2 = new Money(1, 0, 0, 0, 0, 0);

            Action action = () =>
            {
                var money = money1 - money2;
            };

            action.Should().Throw<InvalidOperationException>();
        }
    }
}