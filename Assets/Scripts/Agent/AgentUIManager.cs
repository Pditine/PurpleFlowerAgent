using JsonData;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Agent
{
    public class AgentUIManager : MonoBehaviour
    {
        [SerializeField] private MessageItem messagePrototype;
        [SerializeField] private Transform messageField;
        [SerializeField] private Button sendMessageButton;
        [SerializeField] private TMP_InputField inputField;
        [SerializeField] private AgentController controller;
        public void SetReady(bool isReady)
        {
            sendMessageButton.interactable = isReady;
        }
        
        public void ShowMessage(Message message)
        {
            var messageItem = Instantiate(messagePrototype).GetComponent<MessageItem>();
            
            string role = message.role;
            if (role == "user") role = "你";
            if (role == "assistant") role = "李健豪";
            
            messageItem.Init(role,message.content);
            messageItem.transform.parent = messageField;
        }

        public void SentMessage()
        {
            controller.SendMessageToAI(inputField.text);
        }
    }
}