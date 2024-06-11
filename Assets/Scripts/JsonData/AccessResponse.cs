using System;

namespace JsonData
{
    [Serializable]
    public class AccessResponse
    {
        public string refresh_token;
        public int expires_in;
        public string session_key;
        public string access_token;
        public string scope;
        public string session_secret;
    }
}

