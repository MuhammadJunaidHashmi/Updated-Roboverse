using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class spawanPlayer : MonoBehaviour
{
    public GameObject PlayerObj;
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.Instantiate(PlayerObj.name, Vector3.zero, Quaternion.identity);

    }

    // Update is called once per frame
   
}
