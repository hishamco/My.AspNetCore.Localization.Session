using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;

namespace My.AspNetCore.Localization.Session
{
    /// <summary>
    /// Determines the culture information for a request via values in the session-state data.
    /// </summary>
    public class SessionStateRequestCultureProvider : RequestCultureProvider
    {
        /// <summary>
        /// The key that contains the culture name.
        /// Defaults to "culture".
        /// </summary>
        public string SessionStateKey { get; set; } = "culture";

        /// <summary>
        /// The key that contains the UI culture name. If not specified or no value is found,
        /// <see cref="SessionStateKey"/> will be used.
        /// Defaults to "ui-culture".
        /// </summary>
        public string UISessionStateKey { get; set; } = "ui-culture";

        /// <inheritdoc />
        public override Task<ProviderCultureResult> DetermineProviderCultureResult(HttpContext httpContext)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException(nameof(httpContext));
            }

            string culture = null;
            string uiCulture = null;

            if (!string.IsNullOrEmpty(SessionStateKey))
            {
                culture = httpContext.Session.GetString(SessionStateKey);
            }

            if (!string.IsNullOrEmpty(UISessionStateKey))
            {
                uiCulture = httpContext.Session.GetString(UISessionStateKey);
            }

            if (culture == null && uiCulture == null)
            {
                return Task.FromResult((ProviderCultureResult)null);
            }

            if (culture != null && uiCulture == null)
            {
                uiCulture = culture;
            }

            if (culture == null && uiCulture != null)
            {
                culture = uiCulture;
            }

            var providerResultCulture = new ProviderCultureResult(culture, uiCulture);

            return Task.FromResult(providerResultCulture);
        }
    }
}
