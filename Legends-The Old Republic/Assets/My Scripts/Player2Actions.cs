using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Actions : MonoBehaviour
{
    public float CharacterJumpSpeed = 1.0f;
    public GameObject Player1;
    private Animator Animator;
    private AnimatorStateInfo Player1Layer0;
    public float PunchMove = 2.0f;
    private bool HeavyMoving=false;
    private AudioSource MyPlayerAudioSource;
    public AudioClip Punch;
    public AudioClip Kick;
    // Start is called before the first frame update
    void Start()
    {
        Animator = GetComponent<Animator>();
        MyPlayerAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        //Heavy Punch Slide Smoothly
        if (Player2Move.FacingRightPlayer2 == true)
        {
            Player1.transform.Translate(PunchMove * Time.deltaTime, 0, 0);
        }
        if (Player2Move.FacingLeftPlayer2 == true)
        {
            Player1.transform.Translate(-PunchMove * Time.deltaTime, 0, 0);
        }


        //Listen to the animator
        Player1Layer0 = Animator.GetCurrentAnimatorStateInfo(0);

        //Standing attacks
        if (Player1Layer0.IsTag("Motion"))
        { 
            if (Input.GetButtonDown("Fire1"))
        {
            Animator.SetTrigger("LightPunch");
        }
        if (Input.GetButtonDown("Fire2"))
        {
            Animator.SetTrigger("HeavyPunch");
        }
        if (Input.GetButtonDown("Fire3"))
        {
            Animator.SetTrigger("LightKick");
        }
        if (Input.GetButtonDown("Jump"))
        {
            Animator.SetTrigger("HeavyKick");
        }
        if(Input.GetButtonDown("Block"))
            {
                Animator.SetTrigger("BlockOn");
            }
        }

        if (Player1Layer0.IsTag("Block"))
        {
            if (Input.GetButtonUp("Block"))
            {
                Animator.SetTrigger("BlockOff");
            }
        }

        if (Player1Layer0.IsTag("Crouching"))
        {
            if(Input.GetButtonDown("Fire3"))
            {
                Animator.SetTrigger("LightKick");
            }
        }

        //Aerial moves
        if (Player1Layer0.IsTag("Jumping"))
        {
            if (Input.GetButtonDown("Jump"))
            {
                Animator.SetTrigger("HeavyKick");
            }
        }



    }
     public void JumpUp()
    {
        Player1.transform.Translate( 0, CharacterJumpSpeed, 0);
    }

    public void FlipUp()
    {
        Player1.transform.Translate(0, CharacterJumpSpeed, 0);
        Player1.transform.Translate(0.1f, 0, 0);
    }
    public void FlipBack()
    {
        Player1.transform.Translate(0, CharacterJumpSpeed, 0);
        Player1.transform.Translate(-0.1f, 0, 0);
    }

    public void MoveForHeavyPunch()
    {
        StartCoroutine(PunchSlideSmoothly());
    }
    public void PunchSound()
    {
        MyPlayerAudioSource.clip = Punch;
        MyPlayerAudioSource.Play();
    }
    public void KickSound()
    {
        MyPlayerAudioSource.clip = Kick;
        MyPlayerAudioSource.Play();
    }


    IEnumerator PunchSlideSmoothly() 
    {
        HeavyMoving= true;
        yield return new WaitForSeconds(0.1f);
        HeavyMoving = false;

    }
}
