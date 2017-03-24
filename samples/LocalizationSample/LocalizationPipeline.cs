using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using My.AspNetCore.Localization.Session;
using System.Globalization;

namespace LocalizationSample
{
    public class LocalizationPipeline
    {
        public void Configure(IApplicationBuilder app)
        {
            var supportedCultures = new[]
            {
                new CultureInfo("en-US"),
                new CultureInfo("ar-YE")
            };

            var options = new RequestLocalizationOptions()
            {
                DefaultRequestCulture = new RequestCulture(culture: "en-US", uiCulture: "en-US"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            };

            options.RequestCultureProviders = new[] 
            {
                new SessionStateRequestCultureProvider() { Options = options }
            };

            app.UseRequestLocalization(options);
        }
    }
}
