using Microsoft.IdentityServer.Web.Authentication.External;
using System.Globalization;

namespace ADFS_Plug_in
{
    internal class Metadata : IAuthenticationAdapterMetadata
    {
        public string[] AuthenticationMethods => new[] { "https://dummyservice.com/check" };

        public string[] IdentityClaims => new[] { "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/upn" };

        public string AdminName => "MFA adapter";

        public int[] AvailableLcids => new[] { new CultureInfo("en-us").LCID, new CultureInfo("fr").LCID };

        public Dictionary<int, string> Descriptions => new() { { new CultureInfo("en-us").LCID, "Friendly name of My Example MFA Adapter for end users (en)" }, { new CultureInfo("fr").LCID, "Friendly name translated to fr locale" } };

        public Dictionary<int, string> FriendlyNames => new() { { new CultureInfo("en-us").LCID, "Description of My Example MFA Adapter for end users (en)" }, { new CultureInfo("fr").LCID, "Description translated to fr locale" } };

        public bool RequiresIdentity => true;
    }
}
