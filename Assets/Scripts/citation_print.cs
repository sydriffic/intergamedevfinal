using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class citation_print : MonoBehaviour
{
    public Transform Printer;

    private Vector3 endPosition = new Vector3(5, 4, 0);
    private Vector3 startPosition;
    private float duration = 3f;
    private float elapsedTime;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = Printer.position;
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        float percentComplete = elapsedTime / duration;

        transform.position = Vector3.Lerp(startPosition, endPosition, percentComplete);
    }
}
