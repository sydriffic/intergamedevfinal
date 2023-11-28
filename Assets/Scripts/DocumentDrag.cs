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

    Rigidbody2D rb;

    Paper paperScript;
   
    bool submitted = false;
    

    GameManager gm;

    GameObject applicant;
    private void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        applicant = gm.applicant;
        mySR = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        if (isPassport)
        {
            paperScript = GetComponent<Paper>();
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
            mySR.sortingOrder = 15;
            //change size
            transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }
        else
        {
            mySR.sortingOrder = -8;
            transform.localScale = new Vector3(2, 2, 1);
            //}

        }

        if (isSubmittable)
        {
            if (paperScript.stamped || !isPassport)
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

            gm.NextApplicant();
            Destroy(gameObject);
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
            rb.gravityScale = 1f;
        }
    }
}
