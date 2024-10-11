using static DDDInPractice.Business.Money;

namespace DDDInPractice.Business
{
    public class SnackMachine : Entity
    {
        public virtual Money MoneyInside { get; protected set; }
        public virtual Money MoneyInTransaction { get; protected set; }
        public virtual IList<Slot> Slots { get; protected set; }

        public SnackMachine()
        {
        }

        public SnackMachine(IList<Slot> slots)
        {
            MoneyInside = None;
            MoneyInTransaction = None;
            Slots = new List<Slot>
            {
                new Slot(this, 1, null, 0, 0),
                new Slot(this, 2, null, 0, 0),
                new Slot(this, 3, null, 0, 0)
            };
        }

        public virtual void InsertMoney(Money money)
        {
            Money[] coinsAndNotes = { Cent, TenCent, Quarter, Dollar, FiveDollar, TwentyDollar };
            if (!coinsAndNotes.Contains(money))
                throw new InvalidOperationException();

            MoneyInTransaction += money;
        }

        public virtual void ReturnMoney()
        {
            MoneyInTransaction = None;
        }

        public virtual void BuySnack(int position)
        {
            var slot = Slots.Single(x => x.Position == position);
            slot.Quantity--;
            MoneyInside += MoneyInTransaction;
            MoneyInTransaction = None;
        }

        public void LoadSnacks(int position, Snack snack, int quantity, decimal price)
        {
            var slot = Slots.Single(x => x.Position == position);
            slot.Snack = snack;
            slot.Quantity = quantity;
            slot.Price = price;
        }
    }
}
