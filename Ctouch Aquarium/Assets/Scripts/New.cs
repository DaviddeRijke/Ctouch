using System;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace DefaultNamespace
{
    public class New : MonoBehaviour
    {
        private string api_url = "";

        private readonly string Exec = "execute";
        private readonly string Get = "value_of";
        private readonly string Set = "set";
        private readonly string Replace = "action";
        
        void Start()
        {
            //bodyJsonString();
        }


        public void ExecuteSetting(string setting)
        {
            var json = bodyJsonString(
                new SetCommand{type = Exec, action = setting});
        }

        public void SetSetting(string setting, string value)
        {
            var json = bodyJsonString(
                new SetCommand {type = Set, action = value});


        }

        public void GetSetting(string setting)
        {
            
        }

        string bodyJsonString(Command c)
        {
            apirequestbody r = new apirequestbody
            {api_request = 
                new apirequest()
                {
                    hash = "a",
                    timestamp = "time",
                    command = c
                }
            };
            var b = JsonUtility.ToJson(r);
            Debug.Log(b);
            return "";
        }

        IEnumerator Post(string url, string bodyJsonString)
        {
            var request = new UnityWebRequest(url, "POST");
            byte[] bodyRaw = Encoding.UTF8.GetBytes(bodyJsonString);
            request.uploadHandler = (UploadHandler) new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            yield return request.SendWebRequest();
            Debug.Log("Status Code: " + request.responseCode);
        }
    }

    [Serializable]
    class apirequestbody
    {
        public apirequest api_request;
    }

    [Serializable]
    class apirequest
    {
        public string hash;
        public string timestamp;
        public Command command;

    }
    
    [Serializable]
    abstract class Command
    {
        public string type;
    }
    [Serializable]
    class GetCommand : Command
    {
        public string value_of;
    }
    [Serializable]
    class SetCommand : Command
    {
        public string action;
    }
}