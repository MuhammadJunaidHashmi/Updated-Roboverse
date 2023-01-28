using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using TMPro;

public class createAndJoinRoom : MonoBehaviourPunCallbacks
{
    public TMP_InputField createInput;
    public TMP_InputField joinInput;
    public GameObject error;
    bool joined = false;
    
    public void createRoom()
    {
        PhotonNetwork.CreateRoom(createInput.text);
        Debug.LogWarning("create");

    }

   public override void OnJoinRoomFailed(short returnCode, string message)
    {
        error.GetComponent<TMP_Text>().text = "Invalid Room";
    }
    public void JoinRoom()
    {
        if(joinInput.text=="")
        {
            error.GetComponent<TMP_Text>().text = "Enter Room Name";
            error.SetActive(true);
            return;
        }
        if (!PhotonNetwork.JoinRoom(joinInput.text))
        {
            error.GetComponent<TMP_Text>().text = "Invalid Room";
            error.SetActive(true);
        }       
    }
    public override void OnJoinedRoom()
    {
        error.SetActive(false);
       // var roomName = PhotonNetwork.CurrentRoom.Name;
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
