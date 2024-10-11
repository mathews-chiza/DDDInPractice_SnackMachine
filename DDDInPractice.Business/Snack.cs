namespace DDDInPractice.Business
{
    public class Snack : Entity
    {
        public virtual string Name { get; protected set; }

        public Snack()
        {
        }

        public Snack(string name) : this()
        {
            Name = name;
        }
    }
}
