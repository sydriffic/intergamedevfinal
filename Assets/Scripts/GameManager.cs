using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject[] applicants;
    public GameObject applicant;
    public GameObject topApplicant;
    public int applicantIndex = 0;
    public GameObject booth;
    public GameObject penaltySpawner;
    public int money = 100;
    public bool applicantPresent = false;
    public void LoadIntro()
    {
        SceneManager.LoadScene("IntroductionCutscene");
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("Game");
    }

    //endings
    public void EndGame()
    {
        SceneManager.LoadScene("End");
    }
    public void EndGameGood()
    {
        SceneManager.LoadScene("End");
    }
    public void EndGameBad()
    {
        SceneManager.LoadScene("End");
    }
    public void EndGameNeutral()
    {
        SceneManager.LoadScene("End");
    }


    public void CallApplicant()
    {
        if(applicantIndex < applicants.Length)
        {
            applicant = Instantiate(applicants[applicantIndex]);
            Instantiate(topApplicant);
            applicantIndex++;
        }
        
    }

    public void NextApplicant()
    {
        applicant.GetComponent<Applicant>().endDialogue();
       
    }

    public void resetBooth()
    {
        if (applicantIndex < applicants.Length)
        {
            booth.GetComponent<calling>().boothAnim();
        }
    }

    public void PenaltySpawn(Sprite a)
    {
        penaltySpawner.GetComponent<Penalty_Citation>().SpawnCitation(a);
    }
}
