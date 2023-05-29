using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Actions : MonoBehaviour
{
    public float CharacterJumpSpeed = 0.8f;
    public GameObject Player1;
    private Animator Animator;
    private AnimatorStateInfo Player1Layer0;
    public float PunchMove = 0.01f;
    private bool HeavyMoving=false;
    public float HeavyReactAmt = 4f;
    private AudioSource MyPlayerAudioSource;
    public AudioClip Punch;
    public AudioClip Kick;
    public static bool Hits=false;
    public bool HeavyReact = false;
    public static bool FlyingJumpP1 = false;


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
        if(HeavyMoving==true)
        {
            if (Player1Move.FacingRightPlayer1 == true)
            {
                Player1.transform.Translate(PunchMove * Time.deltaTime , 0, 0);
            }
            if (Player1Move.FacingLeftPlayer1 == true)
            {
                Player1.transform.Translate(-PunchMove * Time.deltaTime , 0, 0);
            }

        }
        //Heavy React Slide 
        if (HeavyReact == true)
        {
            if (Player1Move.FacingRightPlayer1 == true)
            {
                Player1.transform.Translate(-HeavyReactAmt * Time.deltaTime, 0, 0);
            }
            if (Player1Move.FacingLeftPlayer1 == true)
            {
                Player1.transform.Translate(HeavyReactAmt * Time.deltaTime, 0, 0);
            }
        }


        //Listen to the animator
        Player1Layer0 = Animator.GetCurrentAnimatorStateInfo(0);

        //Standing attacks
        if (Player1Layer0.IsTag("Motion"))
        { 
            if (Input.GetButtonDown("Fire1"))
        {
            Animator.SetTrigger("LightPunch");
                Hits = false;
        }
        if (Input.GetButtonDown("Fire2"))
        {
            Animator.SetTrigger("HeavyPunch");
                Hits = false;
            }
        if (Input.GetButtonDown("Fire3"))
        {
            Animator.SetTrigger("LightKick");
                Hits = false;
            }
        if (Input.GetButtonDown("Jump"))
        {
            Animator.SetTrigger("HeavyKick");
                Hits = false;
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
                Hits = false;
            }
        }

        //Aerial moves
        if (Player1Layer0.IsTag("Jumping"))
        {
            if (Input.GetButtonDown("Jump"))
            {
                Animator.SetTrigger("HeavyKick");
                Hits = false;
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
        FlyingJumpP1=true;
 
    }
    public void FlipBack()
    {
        Player1.transform.Translate(0, CharacterJumpSpeed, 0);
        FlyingJumpP1 = true;
    }

    public void IdleSpeed()
    {
        FlyingJumpP1 = false;
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
    public void RandomAttack()
    {

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
