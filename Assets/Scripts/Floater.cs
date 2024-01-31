using UnityEngine;
using System.Collections;

// Makes objects float up & down while gently spinning.
public class Floater : MonoBehaviour
{
    // User Inputs
    public float degreesPerSecond = 15.0f;
    public float amplitude = 0.1f;
    public float frequency = 1f;

    // Position Storage Variables
    Vector3 posOffset = new Vector3();
    Vector3 size = new Vector3();
    Vector3 tempPos = new Vector3();

    // Use this for initialization
    void Start()
    {
        // Store the starting position & rotation of the object
        posOffset = transform.position;
        size=GetComponent<Renderer>().bounds.size;
        //Debug.Log(size.y);
    }

    // Update is called once per frame
    void Update()
    {

        // Spin object around Y-Axis
        transform.Rotate(new Vector3(Time.deltaTime * degreesPerSecond, Time.deltaTime * degreesPerSecond, Time.deltaTime * degreesPerSecond), Space.World);
        // Float up/down with a Sin()
        tempPos = posOffset;

        tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * (amplitude*size.y);

        transform.position = tempPos;
    }
}