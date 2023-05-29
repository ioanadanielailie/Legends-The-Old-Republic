using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2MoveAI : MonoBehaviour
{
    private Animator Animator;
    public float CharacterWalkSpeed=0.001f;
    public float JumpSpeed = 0.05f;
    private float MoveSpeed;
    private bool IsJumping = false;
    private AnimatorStateInfo Player1Layer0;
    private bool CharacterCanWalkRight = true;
    private bool CharacterCanWalkLeft = true;
    public GameObject Player1;
    public GameObject Opponent;
    private Vector3 OpponentPosition;
    public static bool FacingLeftAI=false;
    public static bool FacingRightAI=true;
    public static bool WalkRightAI = true;
    public static bool WalkLeftAI = true;
    public AudioClip LPunch;
    public AudioClip HPunch;
    public AudioClip LKick;
    public AudioClip HKick;
    private AudioSource MyPlayer;
    public GameObject Restrict;
    public Rigidbody RB;
    public Collider BoxCollider;
    public Collider CapsuleCollider;

    private float OppDistance;
    public float AttackDistance = 1.5f;
    private bool MoveAI = true;
    public static bool AttackState = false;

    // Start is called before the first frame update
    void Start()
    {
        Opponent = GameObject.Find("Player1");
        FacingLeftAI = false;
        FacingRightAI = true;
        WalkLeftAI = true;
        WalkRightAI=true;
        Animator=GetComponentInChildren<Animator>();
        StartCoroutine(FaceRight());
        MyPlayer = GetComponentInChildren<AudioSource>();
        MoveSpeed = CharacterWalkSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        OppDistance=Vector3.Distance(Opponent.transform.position, Player1.transform.position);

        if (Player2Actions.FlyingJumpP2 == true)
        {
            CharacterWalkSpeed = JumpSpeed;
        }
        else
        {
            CharacterWalkSpeed = MoveSpeed;
        }


        //Check if we are knocked out
        if (SaveScript.Player2Health<=0)
        {
            Animator.SetTrigger("KnockOut");
            Player1.GetComponent<Player2Actions>().enabled = false;
             StartCoroutine(KnockedOut());
        }

        if (SaveScript.Player1Health <= 0)
        {
            Animator.SetTrigger("Victory");
            Player1.GetComponent<Player2Actions>().enabled = false;
            this.GetComponent<Player2Move>().enabled = false;
        }
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
        if (OpponentPosition.x > Player1.transform.position.x)
        {
            StartCoroutine(FaceLeft());
            if (Player1Layer0.IsTag("Motion"))
            {
                Time.timeScale = 1.0f;
                if (OppDistance > AttackDistance)
                {
                    if (MoveAI == true)
                    {
                        if (CharacterCanWalkRight == true)
                        {
                            if (WalkRightAI == true)
                            {
                                Animator.SetBool("Forward", true);
                                Animator.SetBool("Backward", false);
                                AttackState = false;
                                transform.Translate(CharacterWalkSpeed, 0, 0);
                            }
                        }
                    }
                }
                if(OppDistance< AttackDistance)
                {
                    if(CharacterCanWalkRight==true)
                    {
                        if(MoveAI==true)
                        {
                            MoveAI= false;
                            Animator.SetBool("Forward", false);
                            Animator.SetBool("Backward", false);
                            StartCoroutine(ForwardFalse());
                        }
                    }
                }
            }
        }
        if (OpponentPosition.x < Player1.transform.position.x)
        {
            StartCoroutine(FaceRight());
            if (Player1Layer0.IsTag("Motion"))
            {
                Time.timeScale = 1.0f;
                if (OppDistance > AttackDistance)
                {
                    if (MoveAI == true)
                    {
                        if (CharacterCanWalkLeft == true)
                        {
                            if (WalkLeftAI == true)
                            {
                                Animator.SetBool("Backward", true);
                                Animator.SetBool("Forward", false);
                                AttackState = false;
                                transform.Translate(-CharacterWalkSpeed, 0, 0);
                            }
                        }
                    }
                }
                if (OppDistance < AttackDistance)
                {
                    if (CharacterCanWalkLeft == true)
                    {
                        if (MoveAI == true)
                        {
                            MoveAI = false;
                            Animator.SetBool("Forward", false);
                            Animator.SetBool("Backward", false);
                            StartCoroutine(ForwardFalse());
                        }
                    }
                }
            }
        }



        /*

                //Walking left and right
                if (Player1Layer0.IsTag("Motion"))
                {
                    Time.timeScale = 1.0f;
                    if (Input.GetAxis("HorizontalPlayer2") > 0)
                    {
                        if (CharacterCanWalkRight == true)
                        {
                            if (WalkRightAI == true)
                            {
                                Animator.SetBool("Forward", true);
                                transform.Translate(CharacterWalkSpeed , 0, 0);
                            }
                        }
                    }
                    if (Input.GetAxis("HorizontalPlayer2") < 0)
                    {
                        if (CharacterCanWalkLeft == true)
                        {
                            if (WalkLeftAI == true)
                            {
                                Animator.SetBool("Backward", true);
                                transform.Translate(-CharacterWalkSpeed , 0, 0);
                            }
                        }
                    }
                }
                if (Input.GetAxis("HorizontalPlayer2") == 0)
                {
                    Animator.SetBool("Forward", false);
                    Animator.SetBool("Backward", false);
                }
        */


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
        //Resets the restrict
        if (Restrict.gameObject.activeInHierarchy == false)
        {
            WalkLeftAI = true;
            WalkRightAI = true;
        }


        if (Player1Layer0.IsTag("Block"))
        {
            RB.isKinematic = true;
            BoxCollider.enabled = false;
            CapsuleCollider.enabled = false;
        }
        else
        {
            RB.isKinematic = false;
            BoxCollider.enabled = true;
            CapsuleCollider.enabled = true;

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
        if (FacingLeftAI == true)
        {
            FacingLeftAI = false;
            FacingRightAI = true;
            yield return new WaitForSeconds(0.15f);
            //Player1.transform.Rotate(0, -180, 0);
            Animator.transform.Rotate(0, -180, 0);
            Animator.SetLayerWeight(1, 0);
        }
    }
    IEnumerator FaceRight()
    {
        if (FacingRightAI == true)
        {
            FacingRightAI = false;
            FacingLeftAI = true;
            yield return new WaitForSeconds(0.15f);
            //Player1.transform.Rotate(0, 180, 0);
            Animator.transform.Rotate(0, 180, 0);
            Animator.SetLayerWeight(1, 1);
        }
    }
    IEnumerator KnockedOut()
    {
        yield return new WaitForSeconds(0.1f);
        this.GetComponent<Player2Move>().enabled = false;
    }

    IEnumerator ForwardFalse()
    {
        yield return new WaitForSeconds(0.6f);
        MoveAI= true;
    }
}
