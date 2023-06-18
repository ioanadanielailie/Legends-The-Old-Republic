using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1MoveRestrict : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
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
    private void OnTriggerExit(Collider other)
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