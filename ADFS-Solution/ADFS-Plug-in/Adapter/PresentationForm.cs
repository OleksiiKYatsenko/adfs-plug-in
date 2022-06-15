using Microsoft.IdentityServer.Web.Authentication.External;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADFS_Plug_in
{
    internal class PresentationForm : IAdapterPresentation
    {
        /// Returns the HTML Form fragment that contains the adapter user interface. This data will be included in the web page that is presented
        /// to the cient.
        public string GetFormHtml(int lcid)
        {
            string htmlTemplate = Resources.PresentationForm; //todo we will implement this
            return htmlTemplate;
        }

        /// Return any external resources, ie references to libraries etc., that should be included in
        /// the HEAD section of the presentation form html.
        public string GetFormPreRenderHtml(int lcid)
        {
            return null;
        }

        public string GetPageTitle(int lcid) => "MFA Adapter";
    }
}
