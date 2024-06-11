using System;
using System.Collections;
using System.Collections.Generic;
using JsonData;
using PurpleFlowerCore;
using UnityEngine;
using RestSharp;//依赖版本106.15.0 https://www.nuget.org/packages/RestSharp/106.15.0
using UnityEngine.UI; 

public class Agent : MonoBehaviour
{
    private bool _isReady = true;
    const string API_KEY = "7G7Mk6gUxLU90gl9ys8quGtL";
    const string SECRET_KEY = "mD5VZnsGAy1XwldNLVkerUxkUJoy7h3T";
    private RestClient _client;
    private RestRequest _request;
    [SerializeField]private Button virtualPerson;
    private void Start()
    {
        
        _client = new RestClient($"https://aip.baidubce.com/rpc/2.0/ai_custom/v1/wenxinworkshop/chat/completions?access_token={GetAccessToken()}")
            {
                Timeout = -1
            };
        
        _request = new RestRequest(Method.POST);
        _request.AddHeader("Content-Type", "application/json");
    }

    public void SendMessage()
    {
        virtualPerson.interactable = false;
        var body = SaveSystem.Load<Body>("1");
        
        _request.AddParameter("application/json", JsonUtility.ToJson(body), ParameterType.RequestBody);
        ReviveMessage();
    }

    private void ReviveMessage()
    {
        StartCoroutine(DoReviveMessage());
    }

    private IEnumerator DoReviveMessage()
    {
        var response = _client.ExecuteAsync(_request);

        yield return response;
        
        var responseResult = JsonUtility.FromJson<DialogueResponse>(response.Result.Content);
        Debug.Log(responseResult.result);
        Recover();
    }
    
    private void Recover()
    {
        virtualPerson.interactable = true;
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
        request.AddParameter("client_id", API_KEY);
        request.AddParameter("client_secret", SECRET_KEY);
        IRestResponse response = client.Execute(request);
        Debug.Log(response.Content);
        AccessResponse result = JsonUtility.FromJson<AccessResponse>(response.Content);
        return result.access_token;
    }

}
