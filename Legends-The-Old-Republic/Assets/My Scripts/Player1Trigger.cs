using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Trigger : MonoBehaviour
{
    public Collider Col;
    public float DamageAmt = 0.1f;

    public bool EmitFXP1 = false;
    private ParticleSystem ParticlesP1;
    public float PauseSpeedP1 = 0.6f;

    public bool Player1 = true;

    public string ParticleType = "P21";

    private GameObject ChosenParticles;

    private void Start()
    {
        ChosenParticles = GameObject.Find(ParticleType);
        ParticlesP1 = ChosenParticles.gameObject.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Player1 == true)
        {
            if (Player1Actions.Hits == false)
            {
                Col.enabled = true;
            }
            else
            {
                Col.enabled = false;
            }
        }
        else
        {
            if(Player2Actions.HitsPlayer2== false) 
            {
                Col.enabled = true;
            }
            else
            {
                Col.enabled = false;

            }
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        Console.WriteLine("A intrat!");
        if (Player1 == true)
        {
            if (other.gameObject.CompareTag("Player2"))
            {
                if (EmitFXP1 == true)
                {
                    ParticlesP1.Play();
                    Time.timeScale = PauseSpeedP1;
                }
                Player1Actions.Hits = true;
                SaveScript.Player2Health -= DamageAmt;
                if (SaveScript.Player2Timer < 2.0f)
                {
                    SaveScript.Player2Timer += 2.0f;
                }
            }
        }
        else if(Player1 == false) 
        {
            if (other.gameObject.CompareTag("Player1"))
            {
                if (EmitFXP1 == true)
                {
                    ParticlesP1.Play();
                    Time.timeScale = PauseSpeedP1;
                }
                Player2Actions.HitsPlayer2 = true;
                SaveScript.Player1Health -= DamageAmt;
                if (SaveScript.Player1Timer < 2.0f)
                {
                    SaveScript.Player1Timer += 2.0f;
                }
            }

        }
    }
}
