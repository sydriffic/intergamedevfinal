using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DocumentDrag : MonoBehaviour
{
    Vector3 mousePos;
    SpriteRenderer mySR;
    public float edge = 4f;

    private void Start()
    {
        mySR = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        if (transform.position.x < -edge)
        {
            //change depth
            mySR.sortingOrder = 15;
            //change size
            transform.localScale = new Vector3(1, 1, 1);
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
}
