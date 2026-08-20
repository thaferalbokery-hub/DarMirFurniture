using System.Globalization;

namespace DarMirFurniture.Localization;

/// <summary>
/// Central currency configuration for the whole application.
/// Every monetary value is stored, calculated and displayed in Yemeni Riyal (YER).
/// Changing the values here changes the currency across the entire project.
/// </summary>
public static class CurrencySettings
{
    /// <summary>ISO currency code.</summary>
    public const string CurrencyCode = "YER";

    /// <summary>Currency symbol shown next to every amount.</summary>
    public const string CurrencySymbol = "ر.ي";

    /// <summary>Orders at or above this subtotal get free shipping (in YER).</summary>
    public const decimal FreeShippingThreshold = 300_000m;

    /// <summary>Flat shipping fee applied below the free shipping threshold (in YER).</summary>
    public const decimal StandardShippingCost = 3_500m;

    /// <summary>
    /// Numbers are always grouped with ASCII digits (25,000) so amounts stay readable
    /// inside an RTL layout while remaining valid for calculations and model binding.
    /// </summary>
    private static readonly CultureInfo NumberCulture = CultureInfo.InvariantCulture;

    /// <summary>Formats an amount as "25,000 ر.ي".</summary>
    public static string Format(decimal amount) =>
        $"{amount.ToString("N0", NumberCulture)} {CurrencySymbol}";

    /// <summary>Formats a nullable amount, returning a dash when no value exists.</summary>
    public static string Format(decimal? amount) =>
        amount.HasValue ? Format(amount.Value) : "—";

    /// <summary>Formats a shipping fee, showing "مجاني" when the fee is zero.</summary>
    public static string FormatShipping(decimal amount) =>
        amount <= 0 ? AppText.Free : Format(amount);

    /// <summary>Calculates the shipping fee for a given subtotal.</summary>
    public static decimal CalculateShipping(decimal subtotal) =>
        subtotal >= FreeShippingThreshold ? 0m : StandardShippingCost;
}

/// <summary>Convenience helpers so views can write @price.ToYer().</summary>
public static class CurrencyExtensions
{
    public static string ToYer(this decimal amount) => CurrencySettings.Format(amount);

    public static string ToYer(this decimal? amount) => CurrencySettings.Format(amount);
}

/// <summary>
/// Builds the Arabic (Yemen) culture used as the application default.
/// The numeric format is intentionally kept with ASCII digits, a dot decimal
/// separator and a comma group separator so that existing model binding,
/// validation and price calculations keep working unchanged.
/// </summary>
public static class ArabicCulture
{
    public const string CultureName = "ar-YE";

    public static CultureInfo Create()
    {
        CultureInfo culture;
        try
        {
            culture = (CultureInfo)CultureInfo.GetCultureInfo(CultureName).Clone();
        }
        catch (CultureNotFoundException)
        {
            culture = (CultureInfo)CultureInfo.InvariantCulture.Clone();
        }

        var numberFormat = (NumberFormatInfo)CultureInfo.InvariantCulture.NumberFormat.Clone();
        numberFormat.CurrencySymbol = CurrencySettings.CurrencySymbol;
        numberFormat.NumberGroupSeparator = ",";
        numberFormat.NumberDecimalSeparator = ".";
        culture.NumberFormat = numberFormat;

        return culture;
    }
}