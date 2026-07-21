using System.Diagnostics.CodeAnalysis;

namespace QueryStrings.Parable
{
    public class RangeDateQuery : IParsable<RangeDateQuery>
    {
        public DateOnly FromDate {  get; set; }
        public DateOnly ToDate {  get; set; }
        public static RangeDateQuery Parse(string value, IFormatProvider? provider)
        {
            if (!TryParse(value, provider, out RangeDateQuery? result))
                 throw new ArgumentException("NO PARSEING");

            return result;
        }

        public static bool TryParse([NotNullWhen(true)] string? value, IFormatProvider? provider, [MaybeNullWhen(false)] out RangeDateQuery result)
        {
          var segments =  value?.Split(
              ',',StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            if(segments?.Length == 2 
                && DateOnly.TryParse(segments[0], provider,out var fromDate)
                && DateOnly.TryParse(segments[01], provider,out var toDate))
            {
                result = new RangeDateQuery()
                {
                    FromDate = fromDate,
                    ToDate = toDate
                };
                return true;
            }
            result = new RangeDateQuery()
            {
                FromDate = default,
                ToDate = default
            };
            return false;
        }
    }
}
