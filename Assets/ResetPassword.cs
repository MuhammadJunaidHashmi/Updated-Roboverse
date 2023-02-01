using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine.Networking;
using UnityEngine;
using TMPro;

public class ResetPassword : MonoBehaviour
{
    public TMP_InputField Password;
    public TMP_InputField ConfirmPassword;
    public TMP_InputField Code;
    public GameObject MessageBox;
    public TMP_Text Message;
    public GameObject Signin;
    public GameObject resetPassword;
   // public GameObject Loader;
    public string url;

    public void Reset()
    {
        Regex validateNumberRegex = new Regex("^(?:-(?:[1-9](?:\\d{0,2}(?:,\\d{3})+|\\d*))|(?:0|(?:[1-9](?:\\d{0,2}(?:,\\d{3})+|\\d*))))(?:.\\d+|)$");
        if (!Regex.IsMatch(Password.text, @"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$"))
        {
            StartCoroutine(DisplayMessage("Password contains first capital letter,\n digits and special character and length is 8"));
        }
        else if (!Regex.IsMatch(ConfirmPassword.text, @"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$"))
        {
            StartCoroutine(DisplayMessage("Password contains first capital letter,\n digits and special character and length is 8"));
        }
        else if (!Regex.Match(Password.text, ConfirmPassword.text).Success)
        {
            StartCoroutine(DisplayMessage("Password must be same"));
        }
        else if (Code.text.Length == 0)
        {
            StartCoroutine(DisplayMessage("Enter the code"));
        }
        else
        {
            StartCoroutine(Uploads());
           // Loader.SetActive(true);
        }
    }
    IEnumerator Uploads()
    {
        ResetData reset = new ResetData();
        reset.password = Password.text;
        reset.token = Code.text;
        string json = JsonUtility.ToJson(reset, true);
        UnityWebRequest req = new UnityWebRequest(url, "POST");
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
        req.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
        req.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        req.SetRequestHeader("Content-Type", "application/json");
        yield return req.SendWebRequest();
      //  Debug.Log("Before if");
        if (req.result != UnityWebRequest.Result.Success)
        {
          //  Debug.Log("Result");
            Debug.Log("error: " + req.error);
        }
        else
        {
            string response = req.downloadHandler.text;
            // Debug.Log("response" + response);
            EmailData rootRes = new EmailData();
            rootRes = JsonUtility.FromJson<EmailData>(response);
            if (rootRes.status == 200)
            {
              //  Loader.SetActive(false);
                Signin.SetActive(true);
                resetPassword.SetActive(false);
            }
            else if (rootRes.status == 401)
            {
               // Loader.SetActive(false);
                StartCoroutine(DisplayMessage("Token expired or Wrong"));
            }
        }
    }
    IEnumerator DisplayMessage(string msg)
    {
        Message.text = msg;
        MessageBox.SetActive(true);
        yield return new WaitForSeconds(1);
        MessageBox.SetActive(false);
    }
}





