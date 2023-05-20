using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Actions : MonoBehaviour
{
    public float CharacterJumpSpeed = 4.0f;
    public float FSpeed = 0.7f;
    public GameObject Player1;
    private Animator Animator;
    private AnimatorStateInfo Player1Layer0;
    public float PunchMove = 2.0f;
    public float HeavyReactAmt = 4f;
    private bool HeavyMoving=false;
    public bool HeavyReact=false;
    private AudioSource MyPlayerAudioSource;
    public AudioClip Punch;
    public AudioClip Kick;
    public static bool HitsPlayer2=false;
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
        if (HeavyMoving == true)
        {
            if (Player2Move.FacingRightPlayer2 == true)
            {
                Player1.transform.Translate(PunchMove * Time.deltaTime, 0, 0);
            }
            if (Player2Move.FacingLeftPlayer2 == true)
            {
                Player1.transform.Translate(-PunchMove * Time.deltaTime, 0, 0);
            }
        }

        //Heavy React Slide 
        if (HeavyReact == true)
        {
            if (Player2Move.FacingRightPlayer2 == true)
            {
                Player1.transform.Translate(-HeavyReactAmt * Time.deltaTime, 0, 0);
            }
            if (Player2Move.FacingLeftPlayer2 == true)
            {
                Player1.transform.Translate(HeavyReactAmt * Time.deltaTime, 0, 0);
            }
        }


        //Listen to the animator
        Player1Layer0 = Animator.GetCurrentAnimatorStateInfo(0);

        //Standing attacks
        if (Player1Layer0.IsTag("Motion"))
        { 
            if (Input.GetButtonDown("Fire1Player2"))
        {
            Animator.SetTrigger("LightPunch");
                HitsPlayer2 = false;
            }
        if (Input.GetButtonDown("Fire2Player2"))
        {
            Animator.SetTrigger("HeavyPunch");
                HitsPlayer2 = false;
         }
            if (Input.GetButtonDown("Fire3Player2"))
        {
            Animator.SetTrigger("LightKick");
                HitsPlayer2 = false;
            }
        if (Input.GetButtonDown("JumpPlayer2"))
        {
            Animator.SetTrigger("HeavyKick");
                HitsPlayer2 = false;
            }
        if(Input.GetButtonDown("BlockPlayer2"))
            {
                Animator.SetTrigger("BlockOn");
            }
        }

        if (Player1Layer0.IsTag("Block"))
        {
            if (Input.GetButtonUp("BlockPlayer2"))
            {
                Animator.SetTrigger("BlockOff");
            }
        }

        if (Player1Layer0.IsTag("Crouching"))
        {
            if(Input.GetButtonDown("Fire3Player2"))
            {
                Animator.SetTrigger("LightKick");
                HitsPlayer2 = false;
            }
        }

        //Aerial moves
        if (Player1Layer0.IsTag("Jumping"))
        {
            if (Input.GetButtonDown("Jump"))
            {
                Animator.SetTrigger("HeavyKick");
                HitsPlayer2 = false;
            }
        }



    }
     public void JumpUp()
    {
        Player1.transform.Translate( 0, CharacterJumpSpeed* Time.deltaTime, 0);
    }

    public void FlipUp()
    {
        Player1.transform.Translate(0, FSpeed, 0);
        Player1.transform.Translate(0.1f, 0, 0);
    }
    public void FlipBack()
    {
        Player1.transform.Translate(0, FSpeed, 0);
        Player1.transform.Translate(-0.1f, 0, 0);
    }

    public void MoveForHeavyPunch()
    {
        StartCoroutine(PunchSlideSmoothly());
    }
    public void HeavyReaction()
    {
        StartCoroutine(HeavySlide());
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
    IEnumerator HeavySlide()
    {
        HeavyReact = true;
        yield return new WaitForSeconds(0.3f);
        HeavyReact = false;

    }
}
