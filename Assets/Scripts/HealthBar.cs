using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Random = UnityEngine.Random;


public class HealthBar : MonoBehaviourPun, IPunObservable
{
    public Text speed;
    public Text mass;
    public Text velocity;
    public Text damage;
    public Slider slider;
    public float maxHealth = 10;
    public float currentHealth = 0;
    private GameObject[] players;
    private int index = 0;
    bool check = false;
    Rigidbody rd;
    PhotonView view;
    public float ran = 0.0f;


    PrometeoCarController controller;

    private string recievedData = "ok!";
    private float test = 0;
    void Start()
    {
        

        currentHealth = maxHealth;
        rd = GetComponent<Rigidbody>();
        controller = GetComponent<PrometeoCarController>();
        InvokeRepeating("CarSpeedUI", 0f, 0.1f);
        view = GetComponent<PhotonView>();


        StartCoroutine(assign());
       

    }
    IEnumerator assign()
    {
        yield return new WaitForSeconds(10);
        players = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i].GetComponent<PhotonView>().IsMine == false)
            {
                index = i;
                check = true;
               
            }

        }
    }
    public void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }

    public void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }

   

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        { 
            stream.SendNext(currentHealth);
        }
        else if (stream.IsReading)
        {
            test = (float)stream.ReceiveNext();
        }
    }

    // Start is called before the first frame update
   
    public void DamageHealth(float damage, HealthBar hb)
    {
        // Adjust health
        hb.currentHealth -= damage;

        if (hb.currentHealth < 0) hb.currentHealth = 0;
        // Adjust healthbar.
        SetHealth(hb.currentHealth);
  
        // If health is 0 or less, run Die animation.
        if (hb.currentHealth <= 0)
        {
            Die();
        }
    }

    /// <summary>
    /// Handle Orc is dead.
    /// </summary>
    private void Die()
    {
        // GameManager.Instance.CheckDie();

    }
    public void SetMaxHealth(float health)
    {
        slider.maxValue = health;
        slider.value = health;
    }
    public void SetHealth(float health)
    {
        slider.value = health;
    }

    public void CarSpeedUI()
    {

            try
            {
                float absoluteCarSpeed = Mathf.Abs(controller.carSpeed);
            float absoluteCartorque = Mathf.Abs(controller.rearLeftCollider.motorTorque);

            speed.text = Mathf.RoundToInt(absoluteCarSpeed).ToString();

            mass.text = rd.mass.ToString();
            
            velocity.text = "( "+ Math.Round(rd.velocity.x, 2)  + "," + Math.Round(rd.velocity.y, 2) + "," + Math.Round(rd.velocity.z, 2) + " )";

            }
            catch (System.Exception ex)
            {
            
                Debug.LogWarning("name " +this.name+ ex);
            }
    }
    // Update is called once per frame
     void Update()
    {
        
  
            

            if (currentHealth==0 && this.tag.Equals("Player"))
        {
            GameManager.Instance.GameLoose(1);
        }
        if (currentHealth == 0 && this.tag.Equals("AI"))
        {
            GameManager.Instance.GameLoose(2);
        }
      
        if(check)
        {
            players[index].GetComponent<HealthBar>().slider.value = test;
        }
    }
   

}
