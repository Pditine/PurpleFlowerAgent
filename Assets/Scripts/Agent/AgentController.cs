using System.Collections;
using JsonData;
using PurpleFlowerCore;
using RestSharp;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
//依赖版本106.15.0 https://www.nuget.org/packages/RestSharp/106.15.0

namespace Agent
{
    public class AgentController : MonoBehaviour
    {
        const string APIKey = "7G7Mk6gUxLU90gl9ys8quGtL";
        const string SecretKey = "mD5VZnsGAy1XwldNLVkerUxkUJoy7h3T";
        private RestClient _client;
        private RestRequest _request;
        [SerializeField] private Body body;
        [SerializeField] private AgentUIManager uiManager;
        private void Start()
        {
            _client = new RestClient($"https://aip.baidubce.com/rpc/2.0/ai_custom/v1/wenxinworkshop/chat/completions?access_token={GetAccessToken()}")
            {
                Timeout = -1
            };
        
            body = SaveSystem.Load<Body>("1");
        }

        public void SendMessageToAI(string content)
        {
            var theMessage = new Message { role = "user", content = content };
            
            uiManager.SetReady(false);
            uiManager.ShowMessage(theMessage);
            
            body.messages.Add(theMessage);
            
            _request = new RestRequest(Method.POST);
            _request.AddHeader("Content-Type", "application/json");
            _request.AddParameter("application/json", JsonUtility.ToJson(body), ParameterType.RequestBody);
            ReviveMessageFromAI();
        }

        private void ReviveMessageFromAI()
        {
            StartCoroutine(DoReviveMessage());
        }

        private IEnumerator DoReviveMessage()
        {
            var response = _client.ExecuteAsync(_request);
            yield return response;
            var responseResult = JsonUtility.FromJson<DialogueResponse>(response.Result.Content);
            var theMessage = new Message { role = "assistant", content = responseResult.result };
            body.messages.Add(theMessage);
            uiManager.ShowMessage(theMessage);
            uiManager.SetReady(true);
        }
        
    
        /// <summary>
        /// 使用 AK，SK 生成鉴权签名（Access Token）
        /// </summary>
        /// <returns>鉴权签名信息（Access Token）</returns>
        static string GetAccessToken()
        {
            var client = new RestClient($"https://aip.baidubce.com/oauth/2.0/token");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddParameter("grant_type", "client_credentials");
            request.AddParameter("client_id", APIKey);
            request.AddParameter("client_secret", SecretKey);
            IRestResponse response = client.Execute(request);
            AccessResponse result = JsonUtility.FromJson<AccessResponse>(response.Content);
            return result.access_token;
        }

    }
}
