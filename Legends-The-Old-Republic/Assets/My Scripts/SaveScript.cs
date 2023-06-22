using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveScript : MonoBehaviour
{
    public static float Player1Health = 1.0f;
    public static float Player2Health = 1.0f;
    public static float Player1Timer = 2.0f;
    public static float Player2Timer = 2.0f;
    public static bool TimeOut=false;
    public static bool Player1Mode = true;
    public static int Player1Wins = 0;
    public static int Player2Wins = 0;
    public static int Round = 0;
    public static string P1Select;
    public static string P2Select;
    public static GameObject Player1Load;
    public static GameObject Player2Load;
    public static int LevelNumber = 1;
    public static float DifficultyAmt = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        Player1Health = 1.0f;
        Player2Health = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
