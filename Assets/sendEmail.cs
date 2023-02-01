using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine.Networking;
using UnityEngine;
using TMPro;


public class sendEmail : MonoBehaviour
{

    public TMP_InputField Email;
    public GameObject MessageBox;
    public TMP_Text Message;
    public GameObject SetPassword;
    public GameObject ForgetPassword;
  //  public GameObject Loader;
    public string url;

    public void Sendemail()
    {
        if (Regex.IsMatch(Email.text, @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$") != true && !Email.text.EndsWith("@gmail.com"))
        {
            StartCoroutine(DisplayMessage("Invalid email"));
        }
        else
        {
           // Loader.SetActive(true);
            StartCoroutine(Uploads());
        }
    }
    IEnumerator Uploads()
    {
        string email = Email.text;
        UnityWebRequest req = new UnityWebRequest(url + "/?email=" + email, "POST");
  /*      byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
        req.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);*/
        req.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        req.SetRequestHeader("Content-Type", "application/json");
        //Debug.Log(json);
        yield return req.SendWebRequest();
        if (req.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("error: " + req.error);
        } 
        else
        {
            string response = req.downloadHandler.text;
            Debug.Log("response" + response);

            EmailData rootRes = new EmailData();
            rootRes = JsonUtility.FromJson<EmailData>(response);
            if (rootRes.status == 200)
            {
               // Loader.SetActive(false);
                StartCoroutine(DisplayMessage("Verification Code sent to your Email"));
                //ForgetPassword.SetActive(false);
                SetPassword.SetActive(true);
            }
            else if (rootRes.status == 404)
            {
               // Loader.SetActive(false);
                StartCoroutine(DisplayMessage("Email not found"));
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
