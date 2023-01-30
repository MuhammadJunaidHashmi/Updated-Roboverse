using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particleOff : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject particles;

   public void offParticle()
    {
        particles.SetActive(false);
    }
    public void onParticle()
    {
        particles.SetActive(true);
    }
}
