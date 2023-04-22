using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2MoveRestrict : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(Player2Move.FacingRightPlayer2==true)
        {
            if (other.gameObject.CompareTag("Player1Left"))
            {
                Player2Move.WalkLeft = false;
            }
            if (other.gameObject.CompareTag("Player1Right"))
            {
                Player2Move.WalkRight = false;
            }
        }
        else if (Player2Move.FacingLeftPlayer2 == true)
        {
            if (other.gameObject.CompareTag("Player1Left"))
            {
                Player2Move.WalkRight = false;
            }
            if (other.gameObject.CompareTag("Player1Right"))
            {
                Player2Move.WalkLeft = false;
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (Player2Move.FacingRightPlayer2 == true)
        {
            if (other.gameObject.CompareTag("Player1Left"))
            {
                Player2Move.WalkLeft = true;
            }
            if (other.gameObject.CompareTag("Player1Right"))
            {
                Player2Move.WalkRight = true;
            }
        }
        else if (Player2Move.FacingLeftPlayer2 == true)
        {
            if (other.gameObject.CompareTag("Player1Left"))
            {
                Player2Move.WalkRight = true;
            }
            if (other.gameObject.CompareTag("Player1Right"))
            {
                Player2Move.WalkLeft = true;
            }
        }
    }
}
