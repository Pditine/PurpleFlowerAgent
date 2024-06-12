using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class MessageItem : MonoBehaviour
    {
        [SerializeField] private RectTransform rectTransform;
        [SerializeField] private Text text;

        public void Init(string role, string content)
        {
            text.text = $"[{role}]：{content}";
            rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, text.preferredHeight);
        }
    }
}