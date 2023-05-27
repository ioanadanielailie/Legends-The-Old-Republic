using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Trigger : MonoBehaviour
{
    public Collider Col;
    public float DamageAmt = 0.1f;

    public bool EmitFXP2 = false;
    public ParticleSystem ParticlesP2;
    public float PauseSpeedP2 = 0.6f;

    // Update is called once per frame
    void Update()
    {
        if(Player2Actions.HitsPlayer2==false)
        {
            Col.enabled = true;
        }
        else
        {
            Col.enabled = false;
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player1"))
        {
            if (EmitFXP2 == true)
            {
                ParticlesP2.Play();
                Time.timeScale = PauseSpeedP2;
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
