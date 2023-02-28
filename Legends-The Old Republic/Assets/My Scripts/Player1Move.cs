using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Move : MonoBehaviour
{
    private Animator Animator;
    // Start is called before the first frame update
    void Start()
    {
       Animator=GetComponent<Animator>(); 
    }

    // Update is called once per frame
    void Update()
    {
        //Walking left and right
        if(Input.GetAxis("Horizontal")>0)
        {
            Animator.SetBool("Forward", true);
        }
        if (Input.GetAxis("Horizontal") < 0)
        {
            Animator.SetBool("Backward", true);
        }
        if (Input.GetAxis("Horizontal") == 0)
        {
            Animator.SetBool("Forward", false);
            Animator.SetBool("Backward", false);
        }
        //Jumping and crouching
        if (Input.GetAxis("Vertical") > 0)
        {
            Animator.SetTrigger("Jump");
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
}
