using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P2MoveRestrict : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("P1Left"))
        {
            Player2Move.WalkRightPlayer2 = false;
        }
        if (other.gameObject.CompareTag("P1Right"))
        {
            Player2Move.WalkLeftPlayer2 = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("P1Left"))
        {
            Player2Move.WalkRightPlayer2 = true;
        }
        if (other.gameObject.CompareTag("P1Right"))
        {
            Player2Move.WalkLeftPlayer2 = true;
        }
    }

}
