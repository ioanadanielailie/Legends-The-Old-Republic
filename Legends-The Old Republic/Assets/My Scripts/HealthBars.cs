using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBars : MonoBehaviour
{
    public UnityEngine.UI.Image Player1Green;
    public UnityEngine.UI.Image Player2Green;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Player1Green.fillAmount = SaveScript.Player1Health;
        Player2Green.fillAmount=SaveScript.Player2Health;
    }
}
