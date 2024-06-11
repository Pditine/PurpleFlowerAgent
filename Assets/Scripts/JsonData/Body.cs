using System;
using UnityEngine;

namespace JsonData
{
    [Serializable]
    public class Body
    {
        public Message[] messages;
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