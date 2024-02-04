using UnityEngine;
using System.Collections;

// Makes objects float up & down while gently spinning.
public class Floater : MonoBehaviour
{
    // User Inputs
    public float degreesPerSecond = 15.0f;
    public float amplitude = 0.2f;
    //public float frequency = 1f;

    // Position Storage Variables
    Vector3 posOffset = new Vector3();
    Vector3 size = new Vector3();
    Vector3 startPos = new Vector3();

    // Use this for initialization
    void Start()
    {
        // Store the starting position & rotation of the object
        startPos = transform.position;
        size=GetComponent<Renderer>().bounds.size;
        amplitude=amplitude*(size.y);
        GetComponent<Rigidbody>().AddForce(Vector3.down*0.05f, ForceMode.VelocityChange) ;
    }

    // Update is called once per frame
    void Update()
    {

        // Spin object around Y-Axis
        transform.Rotate(new Vector3(Time.deltaTime * degreesPerSecond, Time.deltaTime * degreesPerSecond, Time.deltaTime * degreesPerSecond), Space.World);
        // Float up/down with a Sin()
        //tempPos = posOffset;
        //tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * (amplitude*size.y);
        //transform.position = tempPos;

       
        if (startPos.y -transform.position.y <Mathf.Min((-Mathf.Sqrt(size.y)),-0.2f) ||transform.position.y >=-1 ) {
            if(transform.position.y>0) {
                transform.position=new Vector3(transform.position.x,0,transform.position.z);
                GetComponent<Rigidbody>().AddForce(Vector3.down*0.05f, ForceMode.VelocityChange); 
                }
             GetComponent<Rigidbody>().AddForce(Vector3.down*amplitude, ForceMode.Force); 
        }
        else if(startPos.y-transform.position.y > Mathf.Max((Mathf.Sqrt(size.y)),0.2f) || transform.position.y <= -15) {
            if(transform.position.y<-16) {
                transform.position=new Vector3(transform.position.x,-16,transform.position.z);
                GetComponent<Rigidbody>().AddForce(Vector3.up*0.05f, ForceMode.VelocityChange); 
                }            
             GetComponent<Rigidbody>().AddForce(Vector3.up*amplitude, ForceMode.Force); 
            // Debug.Log("Going up");
             }
        
    }
}