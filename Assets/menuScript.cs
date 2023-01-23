using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;


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

}
