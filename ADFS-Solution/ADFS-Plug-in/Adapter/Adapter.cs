using ADFS_Plug_in.Extensions;
using ADFS_Plug_in.Pipeline;
using Microsoft.IdentityServer.Web.Authentication.External;
using System.Net;
using System.Security.Claims;

namespace ADFS_Plug_in
{
    public class Adapter : IAuthenticationAdapter
    {
        private static AuthPipeline<string, bool> pipeline = new AuthPipeline<string, bool>((inputFirst, builder) =>
                inputFirst.Step2(builder, input => ActiveDirectoryCheck())
                    .Step2(builder, input => DummyServiceCheck()));
        public IAuthenticationAdapterMetadata Metadata
        {
            get { return new Metadata(); }
        }

        public IAdapterPresentation BeginAuthentication(Claim identityClaim, HttpListenerRequest request, IAuthenticationContext authContext)
        {
            return new PresentationForm();
            //return new instance of IAdapterPresentationForm derived class
        }

        public bool IsAvailableForUser(Claim identityClaim, IAuthenticationContext authContext)
        {
            return true; //its all available for now
        }

        public void OnAuthenticationPipelineLoad(IAuthenticationMethodConfigData configData)
        {
            //TODO: Here could be DI container and Logger initialization
            //this is where AD FS passes us the config data, if such data was supplied at registration of the adapter
        }

        public void OnAuthenticationPipelineUnload()
        {

        }

        public IAdapterPresentation OnError(HttpListenerRequest request, ExternalAuthenticationException ex)
        {
            return new PresentationForm();
            //return new instance of IAdapterPresentationForm derived class
        }

        public IAdapterPresentation TryEndAuthentication(IAuthenticationContext authContext, IProofData proofData, HttpListenerRequest request, out Claim[] outgoingClaims)
        {
            outgoingClaims = new Claim[0];
            if (ValidateProofData(proofData, authContext))
            {
                //authn complete - return authn method
                outgoingClaims = new[]
                {
                    // Return the required authentication method claim, indicating the particulate authentication method used.
                    new Claim( "http://schemas.microsoft.com/ws/2008/06/identity/claims/authenticationmethod", "http://example.com/myauthenticationmethod1" )
                };
                return null;
            }
            else
            {
                //authentication not complete - return new instance of IAdapterPresentationForm derived class
                return new PresentationForm();
            }
        }
        static bool ValidateProofData(IProofData proofData, IAuthenticationContext authContext)
        {
            if (proofData == null || proofData.Properties == null || !proofData.Properties.ContainsKey("UserNameAnswer"))
            {
                throw new ExternalAuthenticationException("Error - no answer found", authContext);
            }

            var answer = (string)proofData.Properties["UserNameAnswer"];

            var result = pipeline.Execute(answer);

            return result;
        }
    }
}