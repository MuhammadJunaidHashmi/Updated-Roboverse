using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class spawanPlayer : MonoBehaviour
{
    public GameObject PlayerObj;
    public GameObject[] spawnpoints;
    // Start is called before the first frame update
    void Start()
    {



        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.Instantiate(PlayerObj.name, spawnpoints[0].transform.position, spawnpoints[0].transform.rotation);

        }
        else
        {
            PhotonNetwork.Instantiate(PlayerObj.name, spawnpoints[1].transform.position, spawnpoints[1].transform.rotation);
        }


        

    }
    
}
