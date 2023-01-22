using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Net.Http;
using UnityEngine.Networking;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Text.RegularExpressions;
using TMPro;


public class signup : MonoBehaviour
{
    
    InputField outputArea;
    public TMP_InputField name;
    public TMP_InputField email;
    public TMP_InputField password;
    public TMP_InputField C_password;
    public string url;
    public TMP_Text txt;
    public GameObject Logintosigninss;
    public GameObject signintoliginss;
    // Validate if a string is a email
    Regex validateEmailRegex = new Regex("^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$");
    Regex pass = new Regex("^(?=.*?[a-z])(?=.*?[0-9]).{8,}$");

    void Start()
    {
   
        outputArea = GameObject.Find("outs").GetComponent<InputField>();
        gameObject.GetComponent<Button>().onClick.AddListener(PostSignup);
    }

    public void PostSignup()
    {
        if (!validateEmailRegex.IsMatch(email.text))
        {
            txt.text = "invalid email";
      
            return;
        }
        else if (!pass.IsMatch(password.text))
        {
            txt.text = "Password must contain Alphabet and digit";
           
            return;
        }
        else if (password.text != C_password.text)
        {
            txt.text = "Confirm passward did not match";
            return;
        }
        else {
            txt.text = "";
        }

      
        StartCoroutine(Upload());
    }
    IEnumerator Upload()
    {
        
        Usersignup user = new Usersignup();
        user.email = email.text;
        user.password = password.text;
        user.password_confirmation = C_password.text;
        Rootsignup root = new Rootsignup();
        root.user = user;
        string json = JsonUtility.ToJson(root,true);

        UnityWebRequest req = new UnityWebRequest(url, "POST");
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
        req.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
        req.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        req.SetRequestHeader("Content-Type", "application/json");
        Debug.Log(json);
        yield return req.SendWebRequest();

        RootsignupRes rootRes = new RootsignupRes();
        rootRes= JsonUtility.FromJson<RootsignupRes>(req.downloadHandler.text);
        if (rootRes.status != 200)
        {
            Debug.Log("error: " + req.error);
        }
        else
        {

            Logintosigninss.SetActive(false);
            signintoliginss.SetActive(true);
            Debug.Log("Form upload complete!");
        }
    }


}
