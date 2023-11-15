using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float yOffset = 1f;
    public float stampOffset = 0.5f;
    Vector3 ogPos;
    bool pressed = false;
    public GameObject paper;
    public GameObject[] papers;
    public GameObject stamp;
    public bool accepts = false;
    GameObject applicant;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0)&&!pressed)
        {
            Pressed();
        }
    }

    private void Pressed()
    {
        pressed = true;
        ogPos = transform.position;
        Vector3 newPos = new Vector3(ogPos.x, ogPos.y - yOffset, ogPos.z);
        transform.position = Vector3.Lerp(transform.position, newPos, moveSpeed);
        papers = GameObject.FindGameObjectsWithTag("Paper");
        if (papers.Length != 0)
        {
            paper = papers[0];
            Debug.Log(paper.gameObject.name);

            if (GetComponent<Collider2D>().bounds.Intersects(paper.GetComponent<Collider2D>().bounds))
            {
                GameObject stamped = Instantiate(stamp, new Vector3(transform.position.x, transform.position.y + stampOffset, transform.position.z), transform.rotation);
                stamped.transform.SetParent(paper.transform);
                if (paper.GetComponent<Paper>().stamped == false)
                {
                    paper.GetComponent<Paper>().stamped = true;
                    if (accepts)
                    {
                        paper.GetComponent<Paper>().accepted = true;
                    }
                    else
                    {
                        paper.GetComponent<Paper>().accepted = false;
                    }

                    applicant = GameObject.FindWithTag("Applicant");
                    applicant.GetComponent<Applicant>().accepted = paper.GetComponent<Paper>().accepted;
                    Debug.Log(applicant);
                }

            }
            print(paper.GetComponent<Paper>().accepted);
        }
        Invoke("ResetButton", 1f);
    }

    private void ResetButton()
    {
        transform.position = Vector3.Lerp(transform.position, ogPos, moveSpeed);
        pressed = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("overlap");
    }
}
