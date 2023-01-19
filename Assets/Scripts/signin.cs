using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Net.Http;
using UnityEngine.Networking;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Net;


namespace asdf
{
    public class signin : MonoBehaviour
    {
        InputField outputArea;
        public InputField email;
        public InputField password;
        public GameObject loginScreen;
        public GameObject newScreen;
        private string auth;
        public string getAuth()
        {
            Debug.Log(auth);
            return auth;
        }

        public string url;
        void Start()
        {
            outputArea = GameObject.Find("outs").GetComponent<InputField>();
            gameObject.GetComponent<Button>().onClick.AddListener(PostSignin);
        }
        void PostSignin()
        {
            StartCoroutine(Uploads());
        }

        IEnumerator Uploads()
        {
            Usersignin user = new Usersignin();
            user.email = email.text;
            user.password = password.text;
            Rootsignin root = new Rootsignin();
            root.user = user;
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(root);

            var req = new UnityWebRequest(url, "POST");
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
            req.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            req.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            req.SetRequestHeader("Content-Type", "application/json");
            yield return req.SendWebRequest();
            //Debug.LogError(req.result.);
            //Debug.Log(req);
            //Debug.Log(req.downloadHandler);
        //    string res = req.downloadHandler.text;
          //  RootsignupRes rootRes = new RootsignupRes();
          //  rootRes = JsonUtility.FromJson<RootsignupRes>(req.downloadHandler.text);



            if (req.result != UnityWebRequest.Result.Success)
            {
                Debug.Log("error: " + req.error);
            }
            else
            {
                if (req.GetResponseHeaders().Values.Count > 0)
                {
                    foreach (KeyValuePair<string, string> entry in req.GetResponseHeaders())
                    {
                        if (entry.Key == "Authorization")
                        {
                            auth = entry.Value;
                        }
                    }
                }
                Debug.Log(auth);
                loginScreen.SetActive(false);
                newScreen.SetActive(true);
                Debug.Log("Form upload complete!");
            }
        }


    }
}