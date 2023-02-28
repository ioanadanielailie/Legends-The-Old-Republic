using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Move : MonoBehaviour
{
    private Animator Animator;
    public float CharacterWalkSpeed=0.001f;
    private bool IsJumping = false;
    private AnimatorStateInfo Player1Layer0;
    // Start is called before the first frame update
    void Start()
    {
       Animator=GetComponentInChildren<Animator>(); 
    }

    // Update is called once per frame
    void Update()
    {
        Player1Layer0 = Animator.GetCurrentAnimatorStateInfo(0);
        //Walking left and right
        if (Player1Layer0.IsTag("Motion"))
        {
            if (Input.GetAxis("Horizontal") > 0)
            {
                Animator.SetBool("Forward", true);
                transform.Translate(CharacterWalkSpeed, 0, 0);
            }
            if (Input.GetAxis("Horizontal") < 0)
            {
                Animator.SetBool("Backward", true);
                transform.Translate(-CharacterWalkSpeed, 0, 0);
            }
        }
        if (Input.GetAxis("Horizontal") == 0)
        {
            Animator.SetBool("Forward", false);
            Animator.SetBool("Backward", false);
        }
        //Jumping and crouching
        if (Input.GetAxis("Vertical") > 0)
        {
            if (IsJumping == false)
            {
               IsJumping = true;
               Animator.SetTrigger("Jump");
                StartCoroutine(JumpPause());
            }
        }
        if (Input.GetAxis("Vertical") < 0)
        {
            Animator.SetBool("Crouch",true);
        }
        if (Input.GetAxis("Vertical") == 0)
        {
            Animator.SetBool("Crouch", false);
        }
    }

    IEnumerator JumpPause()
    {
        yield return new WaitForSeconds(1.0f);
        IsJumping = false;
    }
}
