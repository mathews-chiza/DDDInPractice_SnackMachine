using DDDInPractice.Business;
using FluentAssertions;

using static DDDInPractice.Business.Money;

namespace DDDInPractice.Tests
{
    public class SnackMachineSpecs
    {
        [Fact]
        public void ReturnMoneyEmptiesMoneyInTransaction()
        {
            var snackMachine = new SnackMachine();
            snackMachine.InsertMoney(Dollar);

            snackMachine.ReturnMoney();

            snackMachine.MoneyInTransaction.Should().Be(None);
        }

        [Fact]
        public void InsertedMoneyGoesToMoneyInTransaction()
        {
            var snackMachine = new SnackMachine();

            snackMachine.InsertMoney(Cent);
            snackMachine.InsertMoney(Dollar);

            snackMachine.MoneyInTransaction.Amount.Should().Be(1.01);
        }

        [Fact]
        public void CannotInsertMoreThanOneCoinOrNoteAtATime()
        {
            SnackMachine snackMachine = new();
            var twoCent = Cent + Cent;

            Action action = () => snackMachine.InsertMoney(twoCent);

            action.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void BuySnackTradesInsertedMoneyForASnack()
        {
            SnackMachine snackMachine = new();
            snackMachine.LoadSnacks(1, new Snack("Some snack"), 10, 1);
            snackMachine.InsertMoney(Dollar);

            snackMachine.BuySnack(1);

            snackMachine.MoneyInTransaction.Should().Be(None);
            snackMachine.MoneyInside.Should().Be(Dollar + Dollar);
            snackMachine.Slots.Single(x => x.Position == 1).Should().Be(9);
        }
    }
}
