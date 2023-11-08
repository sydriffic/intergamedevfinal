using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Penalty_Citation : MonoBehaviour
{
    public bool entry;
    public GameObject penaltyCitationPrefab;
    public Transform Printer;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.tag = "penaltySystem";

        Instantiate(penaltyCitationPrefab, Printer.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
/*
    private void OntriggerEnter(Collider other)
    {
        //check to see if the player is incorrect
        if ((other.tag == "Accept" && entry == false) || (other.tag == "Deny" && entry))
        {
            
    }
*/
}
