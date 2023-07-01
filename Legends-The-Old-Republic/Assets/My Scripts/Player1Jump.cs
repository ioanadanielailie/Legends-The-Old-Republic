using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Jump : MonoBehaviour
{
    public GameObject Player1;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player2SpaceDetector"))
        {
            if (Player1Move.FacingRightPlayer1 == true)
            {
                Player1.transform.Translate(-1.0f, 0, 0);
            }
            if (Player1Move.FacingLeftPlayer1 == true)
            {
                Player1.transform.Translate(1.0f, 0, 0);
            }
        }

    }
}
