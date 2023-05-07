using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1MoveRestrict : MonoBehaviour
{
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Player2Right"))
    //    {
    //        Player1Move.WalkLeftPlayer1 = false;
    //    }
    //    if (other.gameObject.CompareTag("Player2Left"))
    //    {
    //        Player1Move.WalkRightPlayer1 = false;
    //    }
    //}
    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Player2Right"))
    //    {
    //        Player1Move.WalkRightPlayer1 = true;
    //    }
    //    if (other.gameObject.CompareTag("Player2Left"))
    //    {
    //        Player1Move.WalkLeftPlayer1 = true;
    //    }
    //}
    private void OnTriggerEnter(Collider other)
    {
        if (Player1Move.FacingRightPlayer1 == true)
        {
            if (other.gameObject.CompareTag("Player2Left"))
            {
                Player2Move.WalkLeftPlayer2 = false;
            }
            if (other.gameObject.CompareTag("Player2Right"))
            {
                Player2Move.WalkRightPlayer2 = false;
            }
        }
        else if (Player1Move.FacingLeftPlayer1 == true)
        {
            if (other.gameObject.CompareTag("Player2Left"))
            {
                Player1Move.WalkRightPlayer1 = false;
            }
            if (other.gameObject.CompareTag("Player2Right"))
            {
                Player1Move.WalkLeftPlayer1 = false;
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (Player1Move.FacingRightPlayer1 == true)
        {
            if (other.gameObject.CompareTag("Player2Left"))
            {
                Player1Move.WalkLeftPlayer1 = true;
            }
            if (other.gameObject.CompareTag("Player2Right"))
            {
                Player1Move.WalkRightPlayer1 = true;
            }
        }
        else if (Player1Move.FacingLeftPlayer1 == true)
        {
            if (other.gameObject.CompareTag("Player2Left"))
            {
                Player1Move.WalkRightPlayer1 = true;
            }
            if (other.gameObject.CompareTag("Player2Right"))
            {
                Player1Move.WalkLeftPlayer1 = true;
            }
        }
    }
}
