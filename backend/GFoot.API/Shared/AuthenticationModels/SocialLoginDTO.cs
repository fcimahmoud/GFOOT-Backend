
namespace Shared.AuthenticationModels
{
    public class SocialLoginDTO
    {
        public string Provider { get; set; } // "Google", "Facebook", "Apple"
        public string IdToken { get; set; } // OAuth token
    }

    public class SocialUserPayload
    {
        public string email { get; set; }
        public string name { get; set; }
    }
}
