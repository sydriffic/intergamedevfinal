using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paper : MonoBehaviour
{
    public bool stamped = false;
    public bool accepted = false;

    public Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (stamped)
        {
            Debug.Log("print");
            if (collision.gameObject.name == "BoothWall")
            {
                rb.gravityScale = 1f;
            }
        }
        
    }
}
