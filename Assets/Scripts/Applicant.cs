using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Applicant : MonoBehaviour
{
    public RectTransform myTransform;
    public bool shouldAccepted = true;
    public bool accepted = false;
    public Animator anim;
    public GameObject[] papers;
    GameManager gm;
    public GameObject passport;

    public string[] dialogue;
    public int[] dialogueAllign;

    float textOffset = -100f;
    float boxOffset = 3f;

    GameObject canvas;
    public GameObject textPrefab;

    int textOS;
    
    SpriteRenderer mySR;
    // Start is called before the first frame update
    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        canvas = GameObject.Find("Canvas");
        mySR = GetComponent<SpriteRenderer>();
        mySR.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
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
        StartCoroutine(DailogueSpawn());
        passport = Instantiate(papers[0], new Vector3(-5, -3, 0), Quaternion.identity);
        Instantiate(papers[1], new Vector3(-5, -3.5f, 0), Quaternion.identity);
        if (papers.Length == 3)
        {
            Instantiate(papers[2], new Vector3(-5, -4f, 0), Quaternion.identity);
        }
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

    private IEnumerator DailogueSpawn() { 
    
        GameObject[] textSpawn = new GameObject[dialogue.Length];

        for(int i=0; i < dialogue.Length; i++)
        {
            if (textOS > 3)
            {
                for (int j = 0; j < textOS; j++)
                {
                    textSpawn[j].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, textSpawn[j].GetComponent<RectTransform>().anchoredPosition.y - textOffset);
                }
                textOS--;
            }

            textSpawn[i]=Instantiate(textPrefab, new Vector2(0, 0), canvas.transform.rotation, canvas.transform);
            
           

            Debug.Log(textOffset * i);
            if (dialogueAllign[i] == 1)
            {
                textSpawn[i].GetComponent<RectTransform>().anchorMax = new Vector2(0f, 1f);
                textSpawn[i].GetComponent<RectTransform>().anchorMin = new Vector2(0f, 1f);
                textSpawn[i].GetComponent<RectTransform>().pivot = new Vector2(0f, 1f);
            }
            else
            {
                textSpawn[i].GetComponent<RectTransform>().anchorMax = new Vector2(1f, 1f);
                textSpawn[i].GetComponent<RectTransform>().anchorMin = new Vector2(1f, 1f);
                textSpawn[i].GetComponent<RectTransform>().pivot = new Vector2(1f, 1f);
            }
            
            textSpawn[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(0,textOffset*textOS);
            textSpawn[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = dialogue[i];
            textSpawn[i].GetComponent<RectTransform>().sizeDelta = new Vector2(dialogue[i].Length * boxOffset, 100);
            yield return new WaitForSeconds(0.5f);
            textOS++;

        }
        
    }
}
