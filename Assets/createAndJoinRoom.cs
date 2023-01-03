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
    bool joined = false;
    
    public void createRoom()
    {
        PhotonNetwork.CreateRoom(createInput.text);
        Debug.Log("create");

    }

    public void JoinRoom()
    {

        PhotonNetwork.JoinRoom(joinInput.text);
    }
    public override void OnJoinedRoom()
    {
        joined = true;
    }
    private void Update()
    {
        if (PhotonNetwork.PlayerList.Length==2&& PhotonNetwork.PlayerList.Length<=2)
        {
            if(joined)
            {
                joined = false;
                PhotonNetwork.LoadLevel("GamePlay");
            }
         
        }
    }

}
