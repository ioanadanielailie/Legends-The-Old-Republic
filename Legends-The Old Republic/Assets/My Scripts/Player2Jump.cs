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
            Player2.transform.Translate(-0.4f, 0, 0);
        }
    }
}
