using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Applicant : MonoBehaviour
{
    public bool shouldAccepted = true;
    public bool accepted = false;
    public Animator anim;
    public GameObject[] papers;
    GameManager gm;
    public GameObject passport;

    // Start is called before the first frame update
    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            gm.NextApplicant();
        }
    }

    public void SpawnPaper()
    {
        passport= Instantiate(papers[0], new Vector3(-5, -3, 0), Quaternion.identity);
    }

    public void startAnim()
    {
        anim.SetBool("startMove", true);
    }

    public void ApplicantLeft()
    {
        gm.resetBooth();
        if (shouldAccepted != accepted)
        {
            gm.money -= 20;
            gm.PenaltySpawn();
        }
        else
        {
            gm.money += 20;
        }
        Debug.Log(gm.money);
        Destroy(gameObject);
    }

    public void leaveAnim()
    {
        
        anim.SetBool("startMove", false);
    }
}
