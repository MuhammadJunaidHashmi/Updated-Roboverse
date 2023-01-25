using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Net.Http;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Net;
using TMPro;


namespace asdf
{
    public class signin : MonoBehaviour
    {
        InputField outputArea;
        public TMP_InputField email;
        public TMP_InputField password;
        public GameObject loginScreen;
        public GameObject loader;
        public string url;
        string ExistingEmail;
        string ExistingPassword;
        public void Start()
        {
            ExistingEmail = PlayerPrefs.GetString("Email");
            ExistingPassword = PlayerPrefs.GetString("Password");
            Debug.Log(ExistingEmail.Length);
            Debug.Log(ExistingPassword.Length);
            if (ExistingEmail.Length != 0)
            {
                loader.SetActive(true);
                // Signin.enabled = false;
                email.text = ExistingEmail;
                password.text = ExistingPassword;
                StartCoroutine(Uploads());
            }
        }
        public void PostSignin()
        {
           
            StartCoroutine(Uploads());
        }

        IEnumerator Uploads()
        {
            Usersignin user = new Usersignin();
            if (ExistingEmail.Length == 0)
            {
                user.email = email.text;
                user.password = password.text;
            }
            else if (ExistingEmail.Length != 0)
            {
                user.email = ExistingEmail;
                user.password = ExistingPassword;
            }
            Rootsignin root = new Rootsignin();
            root.user = user;
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(root);

            var req = new UnityWebRequest(url, "POST");
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
            req.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            req.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            req.SetRequestHeader("Content-Type", "application/json");
            yield return req.SendWebRequest();
            if (req.result != UnityWebRequest.Result.Success)
            {
                Debug.Log("error: " + req.error);
            }

            else
            {
                string response = req.downloadHandler.text;
                Debug.Log(response);

                SignInResponse rootRes = new SignInResponse();
                rootRes = JsonUtility.FromJson<SignInResponse>(response);
                if (rootRes.data.status == 200)
                {
                  
                    //Signin.enabled = true;
                    int id = rootRes.data.user.id;
                    string playername = rootRes.data.user.name;
                    PlayerPrefs.SetInt("playerID", id);//player ID
                    //loader.SetActive(false);
                  //  StartCoroutine(check(deserialized.data.user.id));
                    loader.SetActive(false);
                    PlayerPrefs.SetString("Email", email.text);
                    PlayerPrefs.SetString("Password", password.text);
                    PlayerPrefs.SetString("Name", playername);
                    PlayerPrefs.Save();
                    loginScreen.SetActive(false);
                    SceneManager.LoadScene("MainMenu");
                    Debug.Log("Form upload complete!");
                }
                if (rootRes.data.status == 404)
                {
                   // Loader.SetActive(false);
                    //StartCoroutine(DisplayMessage("No User Exist with this Email!"));
                }
                else if (rootRes.data.status == 401)
                {
                    //Loader.SetActive(false);
                    //StartCoroutine(DisplayMessage("Invalid Password!"));
                }

               
                
            }
        }

       


    }
}