using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Move : MonoBehaviour
{
    private Animator Animator;
    public float CharacterWalkSpeed=0.001f;
    private bool IsJumping = false;
    private AnimatorStateInfo Player1Layer0;
    private bool CharacterCanWalkRight = true;
    private bool CharacterCanWalkLeft = true;
    public GameObject Player1;
    public GameObject Opponent;
    private Vector3 OpponentPosition;
    private bool FacingLeft=false;
    private bool FacingRight=true;
    // Start is called before the first frame update
    void Start()
    {
        FacingLeft= false;
        FacingRight = true;
       Animator=GetComponentInChildren<Animator>(); 
    }

    // Update is called once per frame
    void Update()
    {
        //Listen to the Animator
        Player1Layer0 = Animator.GetCurrentAnimatorStateInfo(0);
        //Cannot exit screen
        Vector3 ScreenBounds=Camera.main.WorldToScreenPoint(this.transform.position);
        if(ScreenBounds.x>Screen.width-200)
        {
            CharacterCanWalkRight= false;
        }
        if (ScreenBounds.x < 200)
        {
            CharacterCanWalkLeft = false;
        }
        else if(ScreenBounds.x< Screen.width-200 && ScreenBounds.x>200) 
        {
            CharacterCanWalkRight = true;
            CharacterCanWalkLeft = true;
        }

        //Get the opponent's position
        OpponentPosition=Opponent.transform.position;

        //Facing left or right of the Opponent
        if(OpponentPosition.x>Player1.transform.position.x)
        {
            StartCoroutine(FaceLeft());
        }
        if (OpponentPosition.x < Player1.transform.position.x)
        {
            StartCoroutine(FaceRight());
        }





        //Walking left and right
        if (Player1Layer0.IsTag("Motion"))
        {
            if (Input.GetAxis("Horizontal") > 0)
            {
                if (CharacterCanWalkRight == true)
                {
                    Animator.SetBool("Forward", true);
                    transform.Translate(CharacterWalkSpeed, 0, 0);
                }
            }
            if (Input.GetAxis("Horizontal") < 0)
            {
                if (CharacterCanWalkLeft == true)
                {
                    Animator.SetBool("Backward", true);
                    transform.Translate(-CharacterWalkSpeed, 0, 0);
                }
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
    IEnumerator FaceLeft() 
    {
        if (FacingLeft == true)
        {
            FacingLeft = false;
            FacingRight = true;
            yield return new WaitForSeconds(0.15f);
            Player1.transform.Rotate(0, -180, 0);
        }
    }
    IEnumerator FaceRight()
    {
        if (FacingRight == true)
        {
            FacingRight = false;
            FacingLeft = true;
            yield return new WaitForSeconds(0.15f);
            Player1.transform.Rotate(0, 180, 0);
        }
    }
}
