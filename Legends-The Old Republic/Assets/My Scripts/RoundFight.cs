using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundFight : MonoBehaviour
{
    public GameObject RoundText;
    public GameObject FightText;
    public AudioSource MyPlayer;
    public AudioClip FightAudio;
    public float PauseTime = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        RoundText.gameObject.SetActive(false);
        FightText.gameObject.SetActive(false);
        StartCoroutine(RoundSet());
        
    }
    IEnumerator RoundSet()
    {
        yield return new WaitForSeconds(0.4f);
        RoundText.gameObject.SetActive(true);
        MyPlayer.Play();
        yield return new WaitForSeconds(PauseTime);
        RoundText.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        FightText.gameObject.SetActive(true);
        MyPlayer.clip= FightAudio;
        MyPlayer.Play();
        yield return new WaitForSeconds(PauseTime);
        FightText.gameObject.SetActive(false);
        SaveScript.TimeOut = false;
        this.gameObject.SetActive(false);


    }
}


