using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Jump : MonoBehaviour
{
    public GameObject Player2;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player1SpaceDetector"))
        {
            if(Player2Move.FacingLeftPlayer2==true)
            {
                Player2.transform.Translate(0.8f, 0, 0);
            }
            if (Player2Move.FacingRightPlayer2 == true)
            {
                Player2.transform.Translate(-0.8f, 0, 0);
            }

        }
    }
}
