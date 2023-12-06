using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour
{
    //access applicant variables
    public Applicant applicantScript;
    public GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gm.money >= 100)
        {
            gm.EndGameGood();
        }
        else if (gm.money < 100 && gm.money > 50)
        {
            gm.EndGameBad();
        }
        else if (gm.money <= 50)
        {
            gm.EndGameNeutral();
        }
        else
        {
            gm.EndGame();
        }
    }
}
