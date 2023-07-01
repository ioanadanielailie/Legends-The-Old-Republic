using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2TriggerAI : MonoBehaviour
{
    public Collider Col;
    public float DamageAmt = 0.1f;

    public bool EmitFXP2 = false;
    public ParticleSystem ParticlesP2;
    public float PauseSpeedP2 = 0.6f;
    public string ParticleType = "P11";

    public bool Player2 = true;

    private GameObject ChosenParticles;

    private void Start()
    {
        ChosenParticles = GameObject.Find(ParticleType);
        ParticlesP2 = ChosenParticles.gameObject.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Player2 == true)
        {
            if (Player2ActionsAI.HitsAI == false)
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
            if(Player1Actions.Hits==false) 
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
        if (SaveScript.P1Reacting == false)
        {
            if (Player2 == true)
            {
                if (other.gameObject.CompareTag("Player1"))
                {
                    if (EmitFXP2 == true)
                    {
                        ParticlesP2.Play();
                        Time.timeScale = PauseSpeedP2;
                    }
                    Player2ActionsAI.HitsAI = true;
                    SaveScript.Player1Health -= DamageAmt;
                    if (SaveScript.Player1Timer < 2.0f)
                    {
                        SaveScript.Player1Timer += 2.0f;
                    }
                }
            }
        }
        if (SaveScript.P2Reacting == false)
        {
            if (Player2 == false)
            {
                if (other.gameObject.CompareTag("Player2"))
                {
                    if (EmitFXP2 == true)
                    {
                        ParticlesP2.Play();
                        Time.timeScale = PauseSpeedP2;
                    }
                    Player1Actions.Hits = true;
                    SaveScript.Player2Health -= DamageAmt;
                    if (SaveScript.Player2Timer < 2.0f)
                    {
                        SaveScript.Player2Timer += 2.0f;
                    }
                }
            }
        }
    }
}
