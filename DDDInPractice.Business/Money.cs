namespace DDDInPractice.Business
{
    public class Money : ValueObject<Money>
    {
        public int OneCentCount { get; }
        public int TenCentCount { get; }
        public int QuarterCount { get; }
        public int OneDollarCount { get; }
        public int FiveDollarCount { get; }
        public int TwentyDollarCount { get; }

        public static readonly Money None = new(0, 0, 0, 0, 0, 0);
        public static readonly Money Cent = new(1, 0, 0, 0, 0, 0);
        public static readonly Money TenCent = new(0, 1, 0, 0, 0, 0);
        public static readonly Money Quarter = new(0, 0, 1, 0, 0, 0);
        public static readonly Money Dollar = new(0, 0, 0, 1, 0, 0);
        public static readonly Money FiveDollar = new(0, 0, 0, 0, 1, 0);
        public static readonly Money TwentyDollar = new(0, 0, 0, 0, 0, 1);

        public Money()
        {   
        }

        public Money(
            int oneCentCount,
            int tenCentCount,
            int quarterCount,
            int oneDollarCount,
            int fiveDollarCount,
            int twentyDollarCount) : this()
        {
            if (oneCentCount < 0)
                throw new InvalidOperationException();
            if (tenCentCount < 0)
                throw new InvalidOperationException();
            if (quarterCount < 0)
                throw new InvalidOperationException();
            if (fiveDollarCount < 0)
                throw new InvalidOperationException();
            if (oneDollarCount < 0)
                throw new InvalidOperationException();
            if (fiveDollarCount < 0)
                throw new InvalidOperationException();
            if (twentyDollarCount < 0)
                throw new InvalidOperationException();

            OneCentCount = oneCentCount;
            TenCentCount = tenCentCount;
            QuarterCount = quarterCount;
            OneDollarCount = oneDollarCount;
            FiveDollarCount = fiveDollarCount;
            TwentyDollarCount = twentyDollarCount;
        }

        public static Money operator -(Money a, Money b)
        {
            return new Money(
                a.OneCentCount - b.OneCentCount,
                a.TenCentCount - b.TenCentCount,
                a.QuarterCount - b.QuarterCount,
                a.OneDollarCount - b.OneDollarCount,
                a.FiveDollarCount - b.FiveDollarCount,
                a.TwentyDollarCount - b.TwentyDollarCount);
        }

        public static Money operator +(Money a, Money b)
        {
            var sum = new Money(
                a.OneCentCount + b.OneCentCount,
                a.TenCentCount + b.TenCentCount,
                a.QuarterCount + b.QuarterCount,
                a.OneDollarCount + b.OneDollarCount,
                a.FiveDollarCount + b.FiveDollarCount,
                a.TwentyDollarCount + b.TwentyDollarCount);

            return sum;
        }

        protected override bool EqualsCore(Money other)
        {
            return OneCentCount == other.OneCentCount
                && TenCentCount == other.TenCentCount
                && QuarterCount == other.QuarterCount
                && OneDollarCount == other.OneDollarCount
                && FiveDollarCount == other.FiveDollarCount
                && TwentyDollarCount == other.TwentyDollarCount;
        }

        protected override int GetHashCodeCore()
        {
            unchecked
            {
                var hashCode = OneCentCount;
                hashCode = (hashCode * 397) ^ TenCentCount;
                hashCode = (hashCode * 397) ^ QuarterCount;
                hashCode = (hashCode * 397) ^ OneDollarCount;
                hashCode = (hashCode * 397) ^ FiveDollarCount;
                hashCode = (hashCode * 397) ^ TwentyDollarCount;
                return hashCode;
            }
        }

        public double Amount =>
            OneCentCount * 0.01 +
            TenCentCount * 0.1 +
            QuarterCount * 0.25 +
            OneDollarCount +
            FiveDollarCount * 5 +
            TwentyDollarCount * 20;
    }
}
