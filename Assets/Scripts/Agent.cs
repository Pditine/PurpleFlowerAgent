using System.Collections;
using System.Collections.Generic;
using Response;
using UnityEngine;
using RestSharp;//依赖版本106.15.0 https://www.nuget.org/packages/RestSharp/106.15.0

public class Agent : MonoBehaviour
{
    const string API_KEY = "7G7Mk6gUxLU90gl9ys8quGtL";
    const string SECRET_KEY = "mD5VZnsGAy1XwldNLVkerUxkUJoy7h3T";
    void Start()
    {
        var client = new RestClient($"https://aip.baidubce.com/rpc/2.0/ai_custom/v1/wenxinworkshop/chat/completions?access_token={GetAccessToken()}");
        client.Timeout = -1;
        var request = new RestRequest(Method.POST);
        request.AddHeader("Content-Type", "application/json");
        var body = @"{""messages"": [ {""role"": ""user"",""content"": ""你好,请做一个自我介绍""}],""temperature"":0.95,""top_p"":0.8,""penalty_score"":1,""disable_search"":false,""enable_citation"":false,""response_format"":""text""}";
        request.AddParameter("application/json", body, ParameterType.RequestBody);
        IRestResponse response = client.Execute(request);
        var responseResult = JsonUtility.FromJson<DialogueResponse>(response.Content);
        Debug.LogError(responseResult.result);
    }
    
    /**
* 使用 AK，SK 生成鉴权签名（Access Token）
* @return 鉴权签名信息（Access Token）
*/
    static string GetAccessToken()
    {
        var client = new RestClient($"https://aip.baidubce.com/oauth/2.0/token");
        client.Timeout = -1;
        var request = new RestRequest(Method.POST);
        request.AddParameter("grant_type", "client_credentials");
        request.AddParameter("client_id", API_KEY);
        request.AddParameter("client_secret", SECRET_KEY);
        IRestResponse response = client.Execute(request);
        Debug.Log(response.Content);
        AccessResponse result = JsonUtility.FromJson<AccessResponse>(response.Content);
        return result.access_token;
    }

}
