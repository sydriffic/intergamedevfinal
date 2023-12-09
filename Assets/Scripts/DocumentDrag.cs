using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DocumentDrag : MonoBehaviour
{
    public bool isSubmittable=true;
    public bool isPassport = false;
    Vector3 newMouse;
    SpriteRenderer mySR;
    public float edge = 3f;
    public float gravityLevel = 2f;

    public Sprite backSprite;
    public Sprite frontSprite;

    GameObject booth;


    Rigidbody2D rb;

    Paper paperScript;
   
    bool submitted = false;
    GameObject desk;

    GameObject submission;

    GameManager gm;

    bool clicked = false;

    GameObject applicant;
    private void Start()
    {
        
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        applicant = gm.applicant;
        desk = GameObject.Find("Desk");
        mySR = GetComponent<SpriteRenderer>();
        mySR.sprite = backSprite;
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = gravityLevel;
        if (isPassport)
        {
            paperScript = GetComponent<Paper>();
        }
        else
        {
            paperScript = applicant.GetComponent<Applicant>().passport.GetComponent<Paper>();
        }

        GameObject[] docs = GameObject.FindGameObjectsWithTag("Paper");
        for(int i = 0; i < docs.Length; i++)
        {
            Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), docs[i].GetComponent<Collider2D>());
        }

        booth = GameObject.Find("BoothWall");
        submission = GameObject.Find("Submission");
        
    }

    void Update()
    {

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

        if (Input.GetMouseButtonDown(0))
        {
            newMouse = gameObject.transform.position - mousePos;
        }

        if (Input.GetMouseButtonUp(0) && clicked)
        {
            if(mousePos2D.x < -edge && transform.position.x < -edge)
            {
                rb.gravityScale = gravityLevel;
                
            }
            else
            {
                rb.gravityScale = 0f;
                rb.velocity = new Vector3(0, 0, 0);
            }
            clicked = false;
        }

        if (Input.GetMouseButton(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

            Debug.Log(hit);
            

            if (hit.collider != null && hit.transform == gameObject.transform)
            {

                clicked = true;
                rb.gravityScale = 0f;
                if (mousePos2D.x < -edge && transform.position.x < -edge)
                {
                    //change depth
                    mySR.sprite = backSprite;
                    if (!submitted)
                    {
                        mySR.sortingOrder = 15;
                    }

                    mySR.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
                    //change size
                    transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                    if (GetComponent<Collider2D>().bounds.Intersects(desk.GetComponent<Collider2D>().bounds))
                    {
                        Debug.Log(desk);
                        rb.gravityScale = 0f;
                        rb.velocity = new Vector3(0, 0, 0);

                    }
                    
                }
                else
                {
                    mySR.sprite = frontSprite;
                    mySR.maskInteraction = SpriteMaskInteraction.None;
                    mySR.sortingOrder = -8;
                    transform.localScale = new Vector3(2, 2, 1);
                    
                    //}

                }

                Debug.Log("a");
                transform.position = mousePos + newMouse;

            }

        }

        

        if (isSubmittable)
        {
            if (paperScript.stamped)
            {
                if (transform.position.x < -edge)
                {
                    StartCoroutine(CheckDrag());
                }


            }
        }
        


    }

   
    private void OnTriggerEnter2D(Collider2D collision)
    {
       

        if (collision.gameObject== submission && submitted && rb.velocity.y<0)
        {
            applicant.GetComponent<Applicant>().papersSubmitted++;
            if (applicant.GetComponent<Applicant>().papers.Length == applicant.GetComponent<Applicant>().papersSubmitted)
            {
                gm.NextApplicant();
            }
            gameObject.SetActive(false);
        }

    }


    private IEnumerator CheckDrag()
    { 
        Vector3 startPos = transform.position;
        yield return new WaitForSeconds(0.5f);
        Vector3 finalPos = transform.position;

        if (finalPos.y-startPos.y > 0.5f && GetComponent<Collider2D>().bounds.Intersects(booth.GetComponent<Collider2D>().bounds))
        {
            Debug.Log("Check");
            submitted = true;
            mySR.sortingOrder = 9;
            rb.gravityScale = gravityLevel;
        }
    }
}
