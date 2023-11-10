using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class calling : MonoBehaviour
{
    Animator myAnim;
    public GameObject targetObject;
    public float moveDistance = 3f;
    public float moveDuration = 2f;
    private Vector3 initialPosition;
    private Vector3 targetPosition;
    private bool isMoving = false;
    private float startTime;

    public GameObject targetObject2;
    public float moveDistance2 = 5f;
    public float moveDuration2 = 2f;
    private Vector3 initialPosition2;
    private Vector3 targetPosition2;
    private bool nextcharacter = false;
    private float startTime2;

    private bool showthepaper;
    private float passportspawntimer = 0f;
    private float passportspawnduration = 100f;


    public GameObject passportprefab;


    // Start is called before the first frame update
    void Start()
    {
        initialPosition = targetObject.transform.position;
        targetPosition = initialPosition + Vector3.right * moveDistance;

        initialPosition2 = targetObject2.transform.position;
        targetPosition2 = initialPosition2 + Vector3.right * moveDistance2;

        myAnim = GetComponent<Animator>();
        myAnim.SetBool("peopleinroom", false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

            if(hit.collider != null && hit.transform.name == "call")
            {
                Debug.Log("yess");
                myAnim.SetBool("peopleinroom", true);

                startTime = Time.time;
                isMoving = true;
            }

            if (nextcharacter)
            {
                startTime2 = Time.time;
                nextcharacter = true;
            }

        }

        //small character moves in
        if (isMoving)
        {
            float fullLength = Vector3.Distance(initialPosition, targetPosition);
            float distanceCovered = (Time.time - startTime);
            float fractionOfFinal = distanceCovered / moveDuration;
            targetObject.transform.position = Vector3.Lerp(initialPosition, targetPosition, fractionOfFinal);

            if (fractionOfFinal >= 1f)
            {
                isMoving = false;
                nextcharacter = true;
            }
        }

        //for bigger character moves in 
        if (nextcharacter)
        {
            float fullLength2 = Vector3.Distance(initialPosition2, targetPosition2);
            float distanceCovered2 = (Time.time - startTime);
            float fractionOfFinal2 = distanceCovered2 / moveDuration2;
            targetObject2.transform.position = Vector3.Lerp(initialPosition2, targetPosition2, fractionOfFinal2);

            if (fractionOfFinal2 >= 1f)
            {
                nextcharacter = false;
                showthepaper = true;
            }
        }

        if (showthepaper)
        {
            passportspawntimer++;
            if (passportspawntimer > passportspawnduration)
            {
                GameObject newObject = Instantiate(passportprefab, new Vector3(-5, -3, 0), Quaternion.identity);
                passportspawntimer = 0;
                showthepaper = false;
            }
        }





    }
}
