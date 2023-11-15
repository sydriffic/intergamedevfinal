using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Applicant : MonoBehaviour
{
    public bool shouldBeAccepted = true;
    public bool accepted = false;
    public GameObject newApplicant;
    Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Decision()
    {
        Instantiate(newApplicant, startPos, transform.rotation);
        Destroy(gameObject);
    }


}
