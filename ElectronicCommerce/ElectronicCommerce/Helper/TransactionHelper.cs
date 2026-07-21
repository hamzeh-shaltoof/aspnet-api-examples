namespace ElectronicCommerce.Helper
{
    public static class TransactionHelper
    {
        private static readonly string[] Prefixes = new [] { "ch_", "pi_", "PAYID", "8a8294" };
        public static string GetRandomPrefix()
        {
            int randomNumber = Random.Shared.Next(0, Prefixes.Length);

             return Prefixes[randomNumber];
        }
    }
}
