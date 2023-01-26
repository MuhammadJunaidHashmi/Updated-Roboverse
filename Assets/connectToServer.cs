using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class connectToServer : MonoBehaviourPunCallbacks
{
    public GameObject panl;
    public GameObject loadingAnimation;
    public Button joinBtn;
    public Button createBtn;


    // Start is called before the first frame update
    void Start()
    {
        // PhotonNetwork.ConnectUsingSettings();
        //.ConnectUsingSettings() call on menu script when click on muiltiplayer
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }
    public override void OnJoinedLobby()
    {
        //SceneManager.LoadScene(1);
        joinBtn.interactable = true;

        createBtn.interactable = true;
        loadingAnimation.SetActive(false);
        panl.SetActive(true);
     
    }
}
