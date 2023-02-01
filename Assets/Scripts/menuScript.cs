using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;
using System;

public class menuScript : MonoBehaviourPunCallbacks
{
    public GameObject muiltyplayerLoby;
    public GameObject loadingAnimation;
    public GameObject joinRoom;
    public GameObject createRoom;
    public GameObject setting;
    public GameObject quit;
    public Button joinBtn;
    public Button createBtn;
    public GameObject renameScreen;
    public TMP_Text playerName;
    public TMP_InputField playerRename;
    public GameObject loader;
    public AudioSource menuAudio;
    
    public string url;

    public void Start()
    {
        playerName.text= PlayerPrefs.GetString("Name");
        playerRename.text = PlayerPrefs.GetString("Name");


    }
    public void clickName()
    {
        renameScreen.SetActive(true);
    }
    public void updateName()
    {
        loader.SetActive(true);
        StartCoroutine(rename());
    }

    IEnumerator rename()
    {
        Rename user = new Rename();
        //  var ids= PlayerPrefs.GetString("playerID");
         user.id = PlayerPrefs.GetInt("playerID");
     
        user.name = playerRename.text;
       // Rootsignup root = new Rootsignup();
       // root.user = user;
        string json = JsonUtility.ToJson(user, true);

        UnityWebRequest req = new UnityWebRequest(url, "POST");
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
        req.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
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
            Debug.Log(response);

            RootsignupRes rootRes = new RootsignupRes();
            rootRes = JsonUtility.FromJson<RootsignupRes>(response);
            Debug.Log("code:" + rootRes.status);
            if (rootRes.status == 500)
            {
               // txt.text = "Invalid Code";
                // Loader.SetActive(false);
                // StartCoroutine(DisplayMessage("User already exists"));
            }
            else if (rootRes.status == 200)
            {
                loader.SetActive(false);
                PlayerPrefs.SetString("Name",playerRename.text);
                playerName.text = PlayerPrefs.GetString("Name");
                renameScreen.SetActive(false);
                //Debug.Log("email: " + rootRes.user.email);
                // Logintosigninss.SetActive(false);
                // signintoliginss.SetActive(true);
                //  Debug.Log("Form upload complete!");
            }


        
    }
}

    public void clickMuiltiplyer()
    {
        muiltyplayerLoby.SetActive(true);
        loadingAnimation.SetActive(true);
        PhotonNetwork.ConnectUsingSettings();

    }
    public void close()
    {
        if (PhotonNetwork.IsConnected == true)
        {
            PhotonNetwork.Disconnect();
        }
        renameScreen.SetActive(false);
        setting.SetActive(false);
        joinBtn.interactable = false;
        createBtn.interactable = false;
        muiltyplayerLoby.SetActive(false);
        joinRoom.SetActive(false);
        createRoom.SetActive(false);
        quit.SetActive(false);
    }
    public void closeJoinPopup()
    {

        joinRoom.SetActive(false);
    }
    public void closeCreatePopup()
    {
        createRoom.SetActive(false);
    }
    public void clickJoinPopup()
    {
        joinRoom.SetActive(true);
    }
    public void clickCreatePopup()
    {
        createRoom.SetActive(true);
    }
    public void clickQuitPopup()
    {
        quit.SetActive(true);
    }
    public void clickSetting()
    {
        setting.SetActive(true);
    }
 
    public void Quit()
    {
        #if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
        #endif
            Application.Quit();
    }
    public void buttonAudio()
    {
        menuAudio.Play();

    }

}
