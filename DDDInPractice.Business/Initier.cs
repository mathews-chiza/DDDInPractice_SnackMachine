namespace DDDInPractice.Business
{
    public class Initier
    {
        public static void Init(string connectionString)
        {
            SessionFactory.Init(connectionString);
        }
    }
}
