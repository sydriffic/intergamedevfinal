using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickDrag : MonoBehaviour
{

    Vector3 mousePos;

    void Update()
    {
        if(transform.position.x < 0)
        {
            //change depth
            transform.position = new Vector3(transform.position.x, transform.position.y, -7);
            //change size
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -1);
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
