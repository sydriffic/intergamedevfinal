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
    public Sprite idSprite;
    GameObject canvas;
    public GameObject textPrefab;
    public float papersSubmitted = 0;
    GameObject[] SpawnedPaper;
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
        SpawnedPaper = new GameObject[papers.Length];
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
        
        gm.applicantPresent = true;
        StartCoroutine(DailogueSpawn());
        passport = Instantiate(papers[0], new Vector3(-5, -1, 0), Quaternion.identity);
        SpawnedPaper[0] = passport;
        
        SpawnedPaper[1]= Instantiate(papers[1], new Vector3(-5, -1.5f, 0), Quaternion.identity);
        SpawnedPaper[1].GetComponent<DocumentDrag>().frontSprite = idSprite;
        if (papers.Length == 3)
        {
            SpawnedPaper[2]=Instantiate(papers[2], new Vector3(-5, -2f, 0), Quaternion.identity);
        }
    }

    public void startAnim()
    {
        anim.SetBool("startMove", true);
    }

    public void ApplicantLeft()
    {
        gm.applicantPresent = false;
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
        for(int i = 0; i < SpawnedPaper.Length; i++)
        {
            Destroy(SpawnedPaper[i]);
        }
        Destroy(gameObject);
    }

    public void leaveAnim()
    {

        anim.SetBool("startMove", false);
    }

    private IEnumerator DailogueSpawn()
    { 
    
        GameObject[] textSpawn = new GameObject[dialogue.Length];
        int topIndex=0;

        for(int i=0; i < dialogue.Length; i++)
        {
            if (textOS > 2)
            {
                for (int j = topIndex; j < textOS+topIndex; j++)
                {
                    textSpawn[j].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, textSpawn[j].GetComponent<RectTransform>().anchoredPosition.y - textOffset);
                }
                topIndex++;
                textOS--;
            }

            textSpawn[i]=Instantiate(textPrefab, new Vector2(0, 0), canvas.transform.rotation, canvas.transform.GetChild(0).transform);
            
           

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
            yield return new WaitForSeconds(1f);
            textOS++;

        }

        yield return new WaitForSeconds(5f);
        for (int i =0; i < textSpawn.Length; i++)
        {
            Destroy(textSpawn[i]);
        }
    }   
    
}
