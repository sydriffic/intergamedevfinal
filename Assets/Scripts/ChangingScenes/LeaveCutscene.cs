using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LeaveCutscene : MonoBehaviour
{
    void OnEnable()
    {
        SceneManager.LoadScene("Game", LoadSceneMode.Additive);
    }
}
