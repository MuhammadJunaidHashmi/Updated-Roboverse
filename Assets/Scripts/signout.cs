using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Net.Http;
using UnityEngine.Networking;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Net;
using UnityEngine.SceneManagement;
namespace asdf
{
    public class signout : MonoBehaviour
    {

      //  public GameObject loginScreen;
      //  public GameObject newScreen;
        //public GameObject ReferencedScript;

        //public string url;
        void Start()
        {
            //ReferencedScript.GetComponent<signin>();
            //gameObject.GetComponent<Button>().onClick.AddListener(logout);
        }
        public void logout()
        {
           
            StartCoroutine(signouts());
        }
        IEnumerator signouts()
        {
            PlayerPrefs.DeleteAll();
            yield return new WaitForSeconds(1);
            SceneManager.LoadScene("SplashScreen");
        }
            IEnumerator Uploads()
        {
         

            var req = new UnityWebRequest("as", "Delete");
            req.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            req.SetRequestHeader("Content-Type", "application/json");
           
         //  req.SetRequestHeader("Authorization", ReferencedScript.GetComponent<signin>().getAuth());

            yield return req.SendWebRequest();
            //Debug.LogError(req.result.);
            //Debug.Log(req);
            //Debug.Log(req.downloadHandler);
            RootsignupRes rootRes = new RootsignupRes();
            rootRes = JsonUtility.FromJson<RootsignupRes>(req.downloadHandler.text);
            if (rootRes.status != 200)
            {
                Debug.Log("error: " + req.error);
            }
            else
            {
              //  loginScreen.SetActive(false);
               // newScreen.SetActive(true);
                Debug.Log("Form upload complete!");
            }
        }


    }
}