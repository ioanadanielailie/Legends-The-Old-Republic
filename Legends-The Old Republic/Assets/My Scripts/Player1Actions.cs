using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Actions : MonoBehaviour
{
    public float CharacterJumpSpeed = 1.0f;
    public GameObject Player1;
    private Animator Animator;
    private AnimatorStateInfo Player1Layer0;
    public float PunchMove = 2.0f;
    private bool HeavyMoving=false;
    // Start is called before the first frame update
    void Start()
    {
        Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        //Heavy Punch Slide Smoothly
        if(HeavyMoving==true)
        {
            Player1.transform.Translate(PunchMove* Time.deltaTime, 0, 0);
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
        }

        if(Player1Layer0.IsTag("Crouching"))
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

    IEnumerator PunchSlideSmoothly() 
    {
        HeavyMoving= true;
        yield return new WaitForSeconds(0.1f);
        HeavyMoving = false;

    }
}
