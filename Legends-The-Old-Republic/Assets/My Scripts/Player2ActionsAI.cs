using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2ActionsAI : MonoBehaviour
{
    public float CharacterJumpSpeed = 4.0f;
    public float FSpeed = 0.4f;
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
    public static bool HitsAI=false;
    public static bool FlyingJumpAI = false;
    public static bool Dazed=false;

    private int AttackNumber = 1;
    private bool Attacking = true;
    public float AttackRate = 1.0f;
    public float DazedTime = 3.0f;


    // Start is called before the first frame update
    void Start()
    {
        Animator = GetComponent<Animator>();
        MyPlayerAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (SaveScript.TimeOut == false)
        {
            //Heavy Punch Slide Smoothly
            if (HeavyMoving == true)
            {
                if (Player2MoveAI.FacingRightAI == true)
                {
                    Player1.transform.Translate(PunchMove * Time.deltaTime, 0, 0);
                }
                if (Player2MoveAI.FacingLeftAI == true)
                {
                    Player1.transform.Translate(-PunchMove * Time.deltaTime, 0, 0);
                }
            }

            //Heavy React Slide 
            if (HeavyReact == true)
            {
                if (Player2MoveAI.FacingRightAI == true)
                {
                    Player1.transform.Translate(-HeavyReactAmt * Time.deltaTime, 0, 0);
                }
                if (Player2MoveAI.FacingLeftAI == true)
                {
                    Player1.transform.Translate(HeavyReactAmt * Time.deltaTime, 0, 0);
                }
            }


            //Listen to the animator
            Player1Layer0 = Animator.GetCurrentAnimatorStateInfo(0);

            //Standing attacks
            if (Player1Layer0.IsTag("Motion"))
            {
                if (Attacking == true)
                {
                    Attacking = false;
                    if (AttackNumber == 1)
                    {
                        Animator.SetTrigger("LightPunch");
                        HitsAI = false;
                        StartCoroutine(SetAttacking());
                    }
                    if (AttackNumber == 2)
                    {
                        Animator.SetTrigger("HeavyPunch");
                        HitsAI = false;
                        StartCoroutine(SetAttacking());
                    }
                    if (AttackNumber == 3)
                    {
                        Animator.SetTrigger("LightKick");
                        HitsAI = false;
                        StartCoroutine(SetAttacking());
                    }
                    if (AttackNumber == 4)
                    {
                        Animator.SetTrigger("HeavyKick");
                        HitsAI = false;
                        StartCoroutine(SetAttacking());
                    }
                    if (Input.GetButtonDown("BlockPlayer2"))
                    {
                        Animator.SetTrigger("BlockOn");
                    }
                }
            }

            if (Player1Layer0.IsTag("Block"))
            {
                if (Input.GetButtonUp("BlockPlayer2"))
                {
                    Animator.SetTrigger("BlockOff");
                }
            }

            //Crouching attack
            if (Player1Layer0.IsTag("Crouching"))
            {
                Animator.SetTrigger("LightKick");
                HitsAI = false;
                Animator.SetBool("Crouch", false);
            }

            //Aerial moves
            if (Player1Layer0.IsTag("Jumping"))
            {
                if (Input.GetButtonDown("Jump"))
                {
                    Animator.SetTrigger("HeavyKick");
                    HitsAI = false;
                }
            }
        }
    }
     public void JumpUp()
    {
        Player1.transform.Translate( 0, CharacterJumpSpeed, 0);
    }

    public void FlipUp()
    {
        Player1.transform.Translate(0, FSpeed, 0);
        FlyingJumpAI = true;
    }
    public void FlipBack()
    {
        Player1.transform.Translate(0, FSpeed, 0);
        FlyingJumpAI = true;
    }

    public void IdleSpeed()
    {
        FlyingJumpAI = false;
    }

    public void MoveForHeavyPunch()
    {
        StartCoroutine(PunchSlideSmoothly());
    }
    public void HeavyReaction()
    {
        StartCoroutine(HeavySlide());
        AttackNumber = 3;
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

    public void RandomAttack()
    {
        AttackNumber = Random.Range(1, 5);
        StartCoroutine(SetAttacking());    }


    IEnumerator PunchSlideSmoothly() 
    {
        HeavyMoving= true;
        yield return new WaitForSeconds(0.1f);
        HeavyMoving = false;

    }
    IEnumerator HeavySlide()
    {
        HeavyReact = true;
        Dazed = true;
        yield return new WaitForSeconds(0.3f);
        HeavyReact = false;
        yield return new WaitForSeconds(DazedTime);
        Dazed = false;

    }
    IEnumerator SetAttacking()
    {
        yield return new WaitForSeconds(AttackRate);
        Attacking = true;

    }
}
