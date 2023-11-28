using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class calling : MonoBehaviour
{
    Animator myAnim;
    GameManager gm;
    public GameObject passportprefab;
    public bool boothEnabled=true;


    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();

        myAnim = GetComponent<Animator>();
        boothAnim();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

            if(hit.collider != null && hit.transform.name == "call" &&boothEnabled)
            {
                boothEnabled = false;
                gm.CallApplicant();
                Debug.Log("yess");
                myAnim.SetBool("peopleinroom", true);

             
            }

        }


    }

    public void boothAnim()
    {
        myAnim.SetBool("peopleinroom", false);
        boothEnabled = true;
    }
}
