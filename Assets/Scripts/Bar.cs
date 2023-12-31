using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bar : MonoBehaviour
{
    Animator anim;
    public bool barOut=false;
    [SerializeField] AudioSource baroutSoundEffect;
    [SerializeField] AudioSource barinSoundEffect;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseOver()
    {
       Debug.Log("MouseOver");
        if (Input.GetMouseButtonDown(0))
        {
            
        }
 
    }

    public void MoveOut()
    {
        baroutSoundEffect.Play();
        barOut = true;
        Debug.Log("Pressed");
        anim.SetBool("ShowBar", true);
    }

    public void MoveIn()
    {
        barinSoundEffect.Play();
        barOut = false;
         Debug.Log("Pressed");
        anim.SetBool("ShowBar", false);
    }


}
