using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line_Behaviour : MonoBehaviour
{
    //animation
    Animator anim;
    
    // Start is called before the first frame update
    IEnumerator Start()
    {
        var anim = GetComponent<Animator>();
        var rb = GetComponent<Rigidbody2D>();

        while (true)
        {
            yield return new WaitForSeconds(Random.Range(5,30));
            
            anim.SetInteger("ShuffleIndex", Random.Range(0, 3));
            anim.SetTrigger("Alive");
            rb.velocity = RandomVector(-0.007f, 0.007f);
        }       
    }

    void Update()
    {
        //if ()
    }

    private Vector3 RandomVector(float min, float max)
    {
        var x = Random.Range(min, max);
        var y = Random.Range(min, max);
        var z = Random.Range(min, max);
        return new Vector3(x, y, z);
    }
}
