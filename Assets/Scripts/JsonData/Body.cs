using System;
using System.Collections.Generic;

namespace JsonData
{
    [Serializable]
    public class Body
    {
        public List<Message> messages;
        public float temperature;
        public float top_p;
        public float penalty_score;
        public bool disable_search;
        public bool enable_citation;
        public string response_format;
        public string system;
    }

    [Serializable]
    public class Message
    {
        public string role;
        public string content;
    }
}