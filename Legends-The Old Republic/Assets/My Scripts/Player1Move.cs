using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Move : MonoBehaviour
{
    private Animator Animator;
    public float CharacterWalkSpeed=0.001f;
    public float JumpSpeed = 0.05f;
    public float MoveSpeed;
    private bool IsJumping = false;
    private AnimatorStateInfo Player1Layer0;
    private bool CharacterCanWalkRight = true;
    private bool CharacterCanWalkLeft = true;
    public GameObject Player1;
    public GameObject Opponent;
    private Vector3 OpponentPosition;
    public static bool FacingLeft=false;
    public static bool FacingRight=true;
    public static bool WalkLeftPlayer1 = true;
    public static bool WalkRightPlayer1 = true;
    public AudioClip LPunch;
    public AudioClip HPunch;
    public AudioClip LKick;
    public AudioClip HKick;
    private AudioSource MyPlayer;
    // Start is called before the first frame update
    void Start()
    {
        FacingLeft= false;
        FacingRight = true;
        WalkLeftPlayer1= true;
        WalkRightPlayer1= true;
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
        if (ScreenBounds.x < 150)
        {
            CharacterCanWalkLeft = false;
        }
        else if(ScreenBounds.x > 150 && ScreenBounds.x < Screen.width-150) 
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
            if (FacingRight == true)
            {
                if (Input.GetAxis("Horizontal") > 0)
                {
                    if (CharacterCanWalkRight == true)
                    {
                        if (WalkRightPlayer1 == true)
                        {
                            Animator.SetBool("Forward", true);
                            transform.Translate(CharacterWalkSpeed, 0, 0);
                        }
                    }
                }
                if (Input.GetAxis("Horizontal") < 0)
                {
                    if (CharacterCanWalkLeft == true)
                    {
                        if (WalkLeftPlayer1 == true)
                        {
                            Animator.SetBool("Backward", true);
                            transform.Translate(-CharacterWalkSpeed, 0, 0);
                        }
                    }
                }
            }
            else if (FacingLeft == true)
            {
                if (Input.GetAxis("Horizontal") > 0)
                {
                    if (CharacterCanWalkRight == true)
                    {
                        Animator.SetBool("Forward", true);
                        transform.Translate(-CharacterWalkSpeed, 0, 0);
                    }
                }
                if (Input.GetAxis("Horizontal") < 0)
                {
                    if (CharacterCanWalkLeft == true)
                    {
                        Animator.SetBool("Backward", true);
                        transform.Translate(CharacterWalkSpeed, 0, 0);
                    }
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
            Animator.SetTrigger("HeadReact");
            MyPlayer.clip = HPunch;
            MyPlayer.Play();
        }
        if (other.gameObject.CompareTag("KickHeavy"))
        {
            Animator.SetTrigger("BigReact");
            MyPlayer.clip = HKick;
            MyPlayer.Play();
        }
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
        if (FacingLeft == true)
        {
            FacingLeft = false;
            FacingRight = true;
            yield return new WaitForSeconds(0.01f);
            Player1.transform.Rotate(0, -180, 0);
            Animator.SetLayerWeight(1, 0);
        }
    }
    IEnumerator FaceRight()
    {
        if (FacingRight == true)
        {
            FacingRight = false;
            FacingLeft = true;
            yield return new WaitForSeconds(0.01f);
            Player1.transform.Rotate(0, 180, 0);
            Animator.SetLayerWeight(1, 1);
        }
    }
}
