using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopApplicant : MonoBehaviour
{
    Animator anim;
    GameObject applicant;
    GameManager gm;
    // Start is called before the first frame update
    void Start()
    {
        gm= GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void moveApplicant()
    {
        applicant = gm.applicant;
        applicant.GetComponent<Applicant>().startAnim();
        Destroy(gameObject);
    }
}
