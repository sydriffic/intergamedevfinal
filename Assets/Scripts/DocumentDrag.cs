using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DocumentDrag : MonoBehaviour
{
    public bool isSubmittable=true;
    public bool isPassport = false;
    Vector3 newMouse;
    SpriteRenderer mySR;
    public float edge = 3.5f;
    public float gravityLevel = 2f;

    public Sprite backSprite;
    public Sprite frontSprite;


    Rigidbody2D rb;

    Paper paperScript;
   
    bool submitted = false;
    GameObject desk;
    

    GameManager gm;

    GameObject applicant;
    private void Start()
    {
        
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        applicant = gm.applicant;
        desk = GameObject.Find("Desk");
        mySR = GetComponent<SpriteRenderer>();
        mySR.sprite = backSprite;
        rb = GetComponent<Rigidbody2D>();
        if (isPassport)
        {
            paperScript = GetComponent<Paper>();
        }
        else
        {
            paperScript = applicant.GetComponent<Applicant>().passport.GetComponent<Paper>();
        }
        
    }

    void Update()
    {

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            newMouse = gameObject.transform.position - mousePos;
        }
        if (Input.GetMouseButton(0))
        {
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

            if (hit.collider != null && hit.transform.name == gameObject.name)
            {
                Debug.Log("a");
                transform.position = mousePos + newMouse;

            }

        }

        if (transform.position.x < -edge)
        {
            //change depth
            mySR.sprite = backSprite;
            mySR.sortingOrder = 15;
            mySR.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
            //change size
            transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            if (GetComponent<Collider2D>().bounds.Intersects(desk.GetComponent<Collider2D>().bounds))
            {
                Debug.Log(desk);
                rb.gravityScale = 0f;
                rb.velocity= new Vector3(0,0,0);

            }
            else
            {
               
                rb.gravityScale = gravityLevel;
            }
        }
        else
        {
            mySR.sprite = frontSprite;
            mySR.maskInteraction = SpriteMaskInteraction.None;
            mySR.sortingOrder = -8;
            transform.localScale = new Vector3(2, 2, 1);
            rb.gravityScale = 0f;
            rb.velocity = new Vector3(0, 0, 0);
            //}

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
       

        if (collision.gameObject.name == "Desk" && submitted && rb.velocity.y<0)
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

        if (finalPos.y-startPos.y > 0.5f)
        {
            Debug.Log("Check");
            submitted = true;
            rb.gravityScale = gravityLevel;
        }
    }
}
