using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DocumentDrag : MonoBehaviour
{
    Vector3 mousePos;
    SpriteRenderer mySR;
    public float edge = 4f;

    Rigidbody2D rb;

    Paper paperScript;
    bool dropAllowed = false;
    bool submitted = false;

    GameObject applicant;
    private void Start()
    {
        applicant = GameObject.FindGameObjectWithTag("Applicant");
        mySR = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        paperScript = GetComponent<Paper>();
    }
    void Update()
    {
        if (transform.position.x < -edge)
        {
            //change depth
            mySR.sortingOrder = 15;
            //change size
            transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            Invoke("SetDropAllowanceTimed", 2.0f);
        }
        else
        {
            mySR.sortingOrder = -8;
            transform.localScale = new Vector3(2, 2, 1);
        }

    }

    private Vector3 GetMousePos()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    void OnMouseDown()
    {
        mousePos = gameObject.transform.position - GetMousePos();
    }

    void OnMouseDrag()
    {
        transform.position = GetMousePos() + mousePos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (dropAllowed)
        {
            dropAllowed = false;
            if (paperScript.stamped)
            {
                Debug.Log("print");
                if (collision.gameObject.name == "BoothWall")
                {
                    submitted = true;
                    rb.gravityScale = 1f;
                }
            }
        }


        if (collision.gameObject.name == "Desk" && submitted)
        {
            print(applicant);
            applicant.GetComponent<Applicant>().Decision();
            Destroy(gameObject);
        }




    }

    private void SetDropAllowanceTimed()
    {
        if (paperScript.stamped)
        {
            dropAllowed = true;
        }
        
    }
}
