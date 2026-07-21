namespace ElectronicCommerce.Helpers
{
    public class ValidationHelpers
    {
        public static bool IsAdult(DateOnly? birthDate)
        {
            var today = DateOnly.FromDateTime(DateTime.Today);

            // If birthDate Is Greater  => User Is Under 18
            return birthDate <= today.AddYears(-18);
        }
        public static bool IsUrl(string? url)
            => Uri.TryCreate(url, UriKind.Absolute, out _);
    }
}
