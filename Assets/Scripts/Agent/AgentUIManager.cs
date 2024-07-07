using JsonData;
using PurpleFlowerCore;
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
        [SerializeField] private AgentExpression agentExpression;
        public void SetReady(bool isReady)
        {
            sendMessageButton.interactable = isReady;
        }
        
        public void ShowMessage(Message message)
        {
            inputField.text = "";
            var messageItem = Instantiate(messagePrototype).GetComponent<MessageItem>();
            
            string role = message.role;
            string content = message.content;
            if (role == "user") role = "你";
            if (role == "assistant")
            {
                string expression = message.content.Substring(1, 2);
                agentExpression.ChangeExpression(expression);

                content = message.content.Substring(4, message.content.Length-4);
                role = "李健豪";
            }
            messageItem.Init(role,content);
            messageItem.transform.parent = messageField;
            
        }

        public void SentMessage()
        {
            controller.SendMessageToAI(inputField.text);
        }

        public void Restart()
        {
            SceneSystem.LoadScene(0);
        }
    }
}