using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;


public class createAndJoinRoom : MonoBehaviourPunCallbacks
{
    public InputField createInput;
    public InputField joinInput;
    public GameObject panl;
    public GameObject loading;
    bool check = true;
    
    public void createRoom()
    {
        PhotonNetwork.CreateRoom(createInput.text);
        Debug.Log("create");
        Debug.Log("online: " + PhotonNetwork.CountOfPlayers);
    }

    public void JoinRoom()
    {

        PhotonNetwork.JoinRoom(joinInput.text);
    }
    public override void OnJoinedRoom()
    {
        //panl.SetActive(false);
        // loading.SetActive(false);
        while(check)
        {
            StartCoroutine(startGame());
        }
     
        

    }
    IEnumerator startGame()
    {
        Debug.Log("counts:  "+PhotonNetwork.CountOfPlayers);
            yield return new WaitForSeconds(1);
            if (PhotonNetwork.CountOfPlayers >= 2)
            {
            check = false;
                 PhotonNetwork.LoadLevel("GamePlay");
            }  
    }
  
}
