using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Move : MonoBehaviour
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
    public static bool FacingLeftPlayer2=false;
    public static bool FacingRightPlayer2=true;
    public AudioClip LPunch;
    public AudioClip HPunch;
    public AudioClip LKick;
    public AudioClip HKick;
    private AudioSource MyPlayer;
    // Start is called before the first frame update
    void Start()
    {
        FacingLeftPlayer2 = false;
        FacingRightPlayer2 = true;
       Animator=GetComponentInChildren<Animator>();
       StartCoroutine(FaceRight());
        MyPlayer = GetComponentInChildren<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //Listen to the Animator
        Player1Layer0 = Animator.GetCurrentAnimatorStateInfo(0);

        //Cannot exit screen
        Vector3 ScreenBounds=Camera.main.WorldToScreenPoint(this.transform.position);
        if(ScreenBounds.x > Screen.width-150)
        {
            CharacterCanWalkRight= false;
        }
        if (ScreenBounds.x < 200)
        {
            CharacterCanWalkLeft = false;
        }
        else if(ScreenBounds.x > 200 && ScreenBounds.x < Screen.width-150) 
        {
            CharacterCanWalkRight = true;
            CharacterCanWalkLeft = true;
        }

        //Get the opponent's position
        OpponentPosition=Opponent.transform.position;

        //Facing left or right of the Opponent
        if(OpponentPosition.x > Player1.transform.position.x)
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
            if (FacingRightPlayer2 == true)
            {
                if (Input.GetAxis("HorizontalPlayer2") > 0)
                {
                    if (CharacterCanWalkRight == true)
                    {
                        Animator.SetBool("Forward", true);
                        transform.Translate(CharacterWalkSpeed, 0, 0);
                    }
                }
                if (Input.GetAxis("HorizontalPlayer2") < 0)
                {
                    if (CharacterCanWalkLeft == true)
                    {
                        Animator.SetBool("Backward", true);
                        transform.Translate(-CharacterWalkSpeed, 0, 0);
                    }
                }
            }
            else if (FacingLeftPlayer2 == true)
            {
                if (Input.GetAxis("HorizontalPlayer2") > 0)
                {
                    if (CharacterCanWalkRight == true)
                    {
                        Animator.SetBool("Forward", true);
                        transform.Translate(-CharacterWalkSpeed, 0, 0);
                    }
                }
                if (Input.GetAxis("HorizontalPlayer2") < 0)
                {
                    if (CharacterCanWalkLeft == true)
                    {
                        Animator.SetBool("Backward", true);
                        transform.Translate(CharacterWalkSpeed, 0, 0);
                    }
                }
            }
        }
        if (Input.GetAxis("HorizontalPlayer2") == 0)
        {
            Animator.SetBool("Forward", false);
            Animator.SetBool("Backward", false);
        }


        //Jumping and crouching
        if (Input.GetAxis("VerticalPlayer2") > 0)
        {
            if (IsJumping == false)
            {
               IsJumping = true;
               Animator.SetTrigger("Jump");
                StartCoroutine(JumpPause());
            }
        }
        if (Input.GetAxis("VerticalPlayer2") < 0)
        {
            Animator.SetBool("Crouch",true);
        }
        if (Input.GetAxis("VerticalPlayer2") == 0)
        {
            Animator.SetBool("Crouch", false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("FistLight"))
        {
            Animator.SetTrigger("HeadReact");
            MyPlayer.clip = LPunch;
            MyPlayer.Play();
        }
        if (other.gameObject.CompareTag("FistHeavy"))
        {
            Animator.SetTrigger("BigReact");
            MyPlayer.clip = HPunch;
            MyPlayer.Play();
        }
        //if (other.gameObject.CompareTag("KickHeavy"))
        //{
        //    Animator.SetTrigger("BigReact");
        //    MyPlayer.clip = HKick;
        //    MyPlayer.Play();
        //}
        if (other.gameObject.CompareTag("KickLight"))
        {
            Animator.SetTrigger("HeadReact");
            MyPlayer.clip = LKick;
            MyPlayer.Play();
        }
    }

    IEnumerator JumpPause()
    {
        yield return new WaitForSeconds(1.0f);
        IsJumping = false;
    }
    IEnumerator FaceLeft() 
    {
        if (FacingLeftPlayer2 == true)
        {
            FacingLeftPlayer2 = false;
            FacingRightPlayer2 = true;
            yield return new WaitForSeconds(0.01f);
            Player1.transform.Rotate(0, -180, 0);
            Animator.SetLayerWeight(1, 0);
        }
    }
    IEnumerator FaceRight()
    {
        if (FacingRightPlayer2 == true)
        {
            FacingRightPlayer2 = false;
            FacingLeftPlayer2 = true;
            yield return new WaitForSeconds(0.01f);
            Player1.transform.Rotate(0, 180, 0);
            Animator.SetLayerWeight(1, 1);
        }
    }
}
