using UnityEngine;
using System.Collections;

// Makes objects float up & down while gently spinning.
public class Fish : MonoBehaviour
{

    public static Fish fish;
    // User Inputs
    public float amplWiggle = 0.1f;
    public float ampSway = 1f;
    public float movementSpeed = 1.0f;
    public float frequency = 0.8f;

    // Position Storage Variables
    //Vector3 size = new Vector3();
   // Vector3 startPos = new Vector3();
    float rot_y;
    float rot_x;
    Quaternion startRot;
    

    // Use this for initialization
    void Start()
    {
        if (fish == null){
            fish =this;
        }
        // Store the starting position & rotation of the object
        transform.Rotate(new Vector3(0,Random.Range(-180,180),0), Space.World);
        startRot = transform.rotation;
        rot_y=transform.rotation.y;
        //size=GetComponent<Renderer>().bounds.size;
    }

    // Update is called once per frame
    void Update()
    {
        if(Mathf.Abs(transform.position.x)>22||Mathf.Abs(transform.position.z)>22){
            rot_y+=180;
            //could destroy or change position
            Debug.Log("Out of Bounds " + transform.position);
        }
        if(transform.position.y>-0.2||transform.position.y<-15.2){Destroy(this);}
        // up and down wiggle
        rot_x=startRot.x+Mathf.Sin(Time.fixedTime * Mathf.PI * frequency)*ampSway;
        transform.Rotate(new Vector3(rot_x, 0, 0), Space.Self);
        //side to side wiggle
        rot_y=startRot.y+Mathf.Sin(Time.fixedTime * Mathf.PI * 0.01f)*amplWiggle;
        transform.Rotate(new Vector3(0,rot_y,0), Space.World);
  

       transform.position += transform.forward* Time.deltaTime * movementSpeed;
            // Debug.Log("Going up");
    }
        
    
    void OnDestroy(){
        Debug.Log(gameObject.name);
        //Fade doesn't work because material isnt transparent, need to figure out work around
        iTween.FadeTo(gameObject, iTween.Hash("alpha", 0f, "time", 1.0f));
        FindObjectOfType<GameManager>().ChangeNumFishInWater(-1);
    }
}