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

    FadeInOut fade;

    void Start()
    {
        fade = FindObjectOfType<FadeInOut>();
    }

    public void Transition()
    {
        StartCoroutine(LoadGame());
    }

    public void LoadIntro()
    {
        SceneManager.LoadScene("IntroductionCutscene");
    }

    public IEnumerator LoadGame()
    {
        fade.FadeIn();
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Game");
    }

    //endings
    public void EndGame()
    {
        SceneManager.LoadScene("End");
    }
    public void EndGameGood()
    {
        SceneManager.LoadScene("EndRich");
    }
    public void EndGameBad()
    {
        SceneManager.LoadScene("EndBroke");
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

    public void checkEnding()
    {
        if(applicants.Length== applicantIndex)
        {
            if (money >= 160)
            {
                EndGameGood();
            }
            else if (money <160 && money>=100)
            {
                EndGame();
            }
            else
            {
                EndGameBad();
            }
        }
        
    }
}
