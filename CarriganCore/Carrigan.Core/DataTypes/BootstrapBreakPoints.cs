using System.Text;

namespace Carrigan.Core.DataTypes;

public class BootstrapBreakPoints
{
    public int? Col { get; set; }
    public int? Sm { get; set; }
    public int? Md { get; set; }
    public int? Lg { get; set; }
    public int? Xl { get; set; }
    public int? Xxl { get; set; }


    /// <summary>
    /// Builds Bootstrap grid classes for the configured breakpoints.
    /// - If all values are null, returns an empty string.
    /// - Otherwise returns a string that begins and ends with a space, containing:
    ///   - Col  -> "col-{n}"
    ///   - Sm   -> "col-sm-{n}"
    ///   - Md   -> "col-md-{n}"
    ///   - Lg   -> "col-lg-{n}"
    ///   - Xl   -> "col-xl-{n}"
    ///   - Xxl  -> "col-xxl-{n}"
    /// - Any null breakpoint is omitted.
    /// </summary>
    public string ToHtml()
    {
        if (Col is null  && Sm is null && Md is null && Lg is null && Xl is null && Xxl is null)
        {
            return string.Empty;
        }
        else
        {
            StringBuilder stringBuilder = new();
            stringBuilder.Append(' ');

            bool appendedAny = false;

            void AppendClass(string cssClass)
            {
                if (appendedAny)
                {
                    stringBuilder.Append(' ');
                }

                stringBuilder.Append(cssClass);
                appendedAny = true;
            }

            // Col corresponds to the default "col-{n}" form
            if (Col is not null)
            {
                AppendClass($"col-{Col.Value}");
            }

            if (Sm is not null)
            {
                AppendClass($"col-sm-{Sm.Value}");
            }

            if (Md is not null)
            {
                AppendClass($"col-md-{Md.Value}");
            }

            if (Lg is not null)
            {
                AppendClass($"col-lg-{Lg.Value}");
            }

            if (Xl is not null)
            {
                AppendClass($"col-xl-{Xl.Value}");
            }

            if (Xxl is not null)
            {
                AppendClass($"col-xxl-{Xxl.Value}");
            }

            stringBuilder.Append(' ');
            return stringBuilder.ToString();
        }
    }
}
