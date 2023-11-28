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
    public void LoadIntro()
    {
        SceneManager.LoadScene("IntroductionCutscene");
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void EndGame()
    {
        SceneManager.LoadScene("End");
    }

    public void CallApplicant()
    {
        applicant = Instantiate(applicants[applicantIndex]); 
        Instantiate(topApplicant);
        applicantIndex++;
    }

    public void NextApplicant()
    {
        applicant.GetComponent<Applicant>().leaveAnim();
       
    }

    public void resetBooth()
    {
        booth.GetComponent<calling>().boothAnim();
    }

    public void PenaltySpawn()
    {
        penaltySpawner.GetComponent<Penalty_Citation>().SpawnCitation(true);
    }
}
