namespace Fintranet.Services.CongestionTax.Infrastructure.Exceptions;

public class CongestionTaxDomainException : Exception
{
    public CongestionTaxDomainException()
    { }

    public CongestionTaxDomainException(string message)
        : base(message)
    { }

    public CongestionTaxDomainException(string message, Exception innerException)
        : base(message, innerException)
    { }
}


public static  class CongestionTaxDomainExceptionExtensions
{
    public static T NotNull<T>([NotNull] this T? value, [CallerArgumentExpression("value")] string paramName = "")
        where T : class =>
        value is null ? throw new CongestionTaxDomainException(string.Format(ValidationMessages.IS_NOT_NULL, paramName)) : value;

    public static string NotNullOrWhiteSpace([NotNull] this string? value, [CallerArgumentExpression("value")] string paramName = "") =>
    (string.IsNullOrWhiteSpace(value) || string.IsNullOrEmpty(value))
            ? throw new CongestionTaxDomainException(string.Format(ValidationMessages.IS_NOT_NULL_EMPTY_WHITESPACE, paramName))
            : value;

    public static int NotNegative(this int value, [CallerArgumentExpression("value")] string paramName = "") =>
    value < 0
            ? throw new CongestionTaxDomainException(string.Format(ValidationMessages.IS_NOT_NEGATIVE, paramName))
            : value;
    public static decimal NotNegative(this decimal value, [CallerArgumentExpression("value")] string paramName = "") =>
   value < 0
           ? throw new CongestionTaxDomainException(string.Format(ValidationMessages.IS_NOT_NEGATIVE, paramName))
           : value;
    public static double NotNegative(this double value, [CallerArgumentExpression("value")] string paramName = "") =>
   value < 0
           ? throw new CongestionTaxDomainException(string.Format(ValidationMessages.IS_NOT_NEGATIVE, paramName))
           : value;

}
