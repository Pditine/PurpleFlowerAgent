using System;

namespace JsonData
{
    [Serializable]
    public class DialogueResponse
    {
        public string id;
        public int created;
        public string result;
        public bool is_truncated;
        public bool need_clear_history;
        public string finish_reason;
        public DialogueResponse usage;
    }

    [Serializable]
    public class DialogueUsage
    {
        public int prompt_tokens;
        public int completion_tokens;
        public int total_tokens;
    }
}