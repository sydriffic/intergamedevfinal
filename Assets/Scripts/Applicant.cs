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

    public string[] dialogueAccepted;
    public int[] acceptedAllign;

    public string[] dialogueDeclined;
    public int[] declinedAllign;

    float textOffset = -100f;
    float boxOffset = 3f;
    public Sprite idSprite;
    public Sprite accessSprite;
    public Sprite citationSprite;
    GameObject canvas;
    public GameObject textPrefab;
    public float papersSubmitted = 0;
    GameObject[] SpawnedPaper;
    int textOS;
    float textX = 20f;
    SpriteRenderer mySR;

   [SerializeField] AudioSource papersoundEffect;
   [SerializeField] AudioSource walkinsoundEffect;
   [SerializeField] AudioSource dialogueboxsoundEffect;
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
        papersoundEffect.Play();
        gm.applicantPresent = true;
        StartCoroutine(DailogueSpawn(dialogue,dialogueAllign));
        passport = Instantiate(papers[0], new Vector3(-5, -1, 0), Quaternion.identity);
        SpawnedPaper[0] = passport;
        if (papers.Length >= 2 )
        {
            SpawnedPaper[1] = Instantiate(papers[1], new Vector3(-5.5f, -1.5f, 0), Quaternion.identity);
            SpawnedPaper[1].GetComponent<DocumentDrag>().frontSprite = idSprite;
        }
        
        if (papers.Length == 3)
        {
            SpawnedPaper[2]=Instantiate(papers[2], new Vector3(-6f, -2f, 0), Quaternion.identity);
            SpawnedPaper[2].GetComponent<DocumentDrag>().frontSprite = accessSprite;
        }
    }

    public void startAnim()
    {
        walkinsoundEffect.Play();
        anim.SetBool("startMove", true);
    }

    public void ApplicantLeft()
    {
        gm.applicantPresent = false;
        gm.resetBooth();
        gm.checkEnding();
        if (shouldAccepted != accepted)
        {
            gm.money -= 20;
            gm.PenaltySpawn(citationSprite);
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
        walkinsoundEffect.Play();
        if (accepted)
        {
            anim.SetBool("startMove", false);
        }
        else
        {
            anim.SetBool("startDenied", true);
        }

        
    }

    public void endDialogue()
    {
        if (accepted)
        {
            StartCoroutine(DailogueSpawn(dialogueAccepted, acceptedAllign));
        }
        else
        {
            StartCoroutine(DailogueSpawn(dialogueDeclined, declinedAllign));
        }
    }

    private IEnumerator DailogueSpawn(string[] dialogueText, int[] allignNo)
    { 
    
        GameObject[] textSpawn = new GameObject[dialogueText.Length];
        int topIndex=0;
        textOS = 0;
        float allignVal = 1;

        for(int i=0; i < dialogueText.Length; i++)
        {
            if (textOS > 2)
            {
                for (int j = topIndex; j < textOS+topIndex; j++)
                {
                    textSpawn[j].GetComponent<RectTransform>().anchoredPosition = new Vector2(textSpawn[j].GetComponent<RectTransform>().anchoredPosition.x, textSpawn[j].GetComponent<RectTransform>().anchoredPosition.y - textOffset);
                }
                topIndex++;
                textOS--;
            }

            textSpawn[i]=Instantiate(textPrefab, new Vector2(0, 0), canvas.transform.rotation, canvas.transform.GetChild(1).transform);
            dialogueboxsoundEffect.Play();
           

            Debug.Log(textOffset * i);
            if (allignNo[i] == 1)
            {
                textSpawn[i].GetComponent<RectTransform>().anchorMax = new Vector2(0f, 1f);
                textSpawn[i].GetComponent<RectTransform>().anchorMin = new Vector2(0f, 1f);
                textSpawn[i].GetComponent<RectTransform>().pivot = new Vector2(0f, 1f);
                allignVal = textX * 1f;
                
            }
            else
            {
                textSpawn[i].GetComponent<RectTransform>().anchorMax = new Vector2(1f, 1f);
                textSpawn[i].GetComponent<RectTransform>().anchorMin = new Vector2(1f, 1f);
                textSpawn[i].GetComponent<RectTransform>().pivot = new Vector2(1f, 1f);
                allignVal = textX * -1f;
            }
            
            textSpawn[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(allignVal,textOffset*textOS);
            textSpawn[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = dialogueText[i];
            textSpawn[i].GetComponent<RectTransform>().sizeDelta = new Vector2(dialogueText[i].Length * boxOffset, 100);
            yield return new WaitForSeconds(1f);
            textOS++;

        }

        yield return new WaitForSeconds(2f);
        for (int i =0; i < textSpawn.Length; i++)
        {
            Destroy(textSpawn[i]);
        }

        if (dialogueText == dialogueAccepted || dialogueText == dialogueDeclined)
        {
            yield return new WaitForSeconds(0.5f);
            leaveAnim();
        }
    }   
    
}
