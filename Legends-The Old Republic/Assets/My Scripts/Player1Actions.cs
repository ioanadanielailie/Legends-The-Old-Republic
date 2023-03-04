using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Actions : MonoBehaviour
{
    public float CharacterJumpSpeed = 1.0f;
    public GameObject Player1;
    private Animator Animator;
    // Start is called before the first frame update
    void Start()
    {
        Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Animator.SetTrigger("LightPunch");
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
}
