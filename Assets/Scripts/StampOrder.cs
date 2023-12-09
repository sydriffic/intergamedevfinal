using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StampOrder : MonoBehaviour
{
    GameObject paper;
    SpriteRenderer mySR;
    // Start is called before the first frame update
    void Start()
    {
        paper = transform.parent.gameObject;
        mySR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x> -paper.GetComponent<DocumentDrag>().edge)
        {
            mySR.sortingOrder = paper.GetComponent<SpriteRenderer>().sortingOrder + 1;
        }
        else
        {
            mySR.sortingOrder = paper.GetComponent<SpriteRenderer>().sortingOrder - 1;
        }
        
    }
}
