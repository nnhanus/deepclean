using UnityEngine;
using System.Collections;

// Makes objects float up & down while gently spinning.
public class Fish : MonoBehaviour
{

    public static Fish fish;
    // User Inputs
    //amplitude of the side to side movement
    public float amplWiggle = 0.1f;
    //amplitude of the up and down movement
    public float ampSway = 1f;
    public float movementSpeed = 1.0f;
    //frequency of the up and down movement
    public float frequency = 0.8f;

    //temp variable used to store amount of rotation in the y and x directions to be applied
    float rot_y;
    float rot_x;
    float minSize = 0.1f;
    float growthRate  = 1.0f;
    float scale =1.0f;

    //gameObject starting rotation
    Quaternion startRot;
    

    void Start()
    {
        if (fish == null){
            fish =this;
        }
        // Store the starting rotation of the object
        transform.Rotate(new Vector3(0,Random.Range(-180,180),0), Space.World);
        startRot = transform.rotation;
        rot_y=transform.rotation.y;
        //size=GetComponent<Renderer>().bounds.size;
    }

    // Update is called once per frame
    void Update()
    {
        //if fish is getting close to the edge boundaries, turn it around
        if(Mathf.Abs(transform.position.x)>26||Mathf.Abs(transform.position.z)>26||transform.position.y>-0.2||transform.position.y<-15.2){
            //rot_y+=180;
            //could destroy or change position
            Debug.Log("Out of Bounds " + transform.position);
            transform.localScale = Vector3.one * scale;
            scale -= growthRate * Time.deltaTime;
            if (scale < minSize) Destroy (gameObject);
        }
        //if the fish is at the surface or floor of the ocean, destroy it

        // up and down wiggle
        rot_x=startRot.x+Mathf.Sin(Time.fixedTime * Mathf.PI * frequency)*ampSway;
        transform.Rotate(new Vector3(rot_x, 0, 0), Space.Self);

        //side to side wiggle
        rot_y=startRot.y+Mathf.Sin(Time.fixedTime * Mathf.PI * 0.01f)*amplWiggle;
        transform.Rotate(new Vector3(0,rot_y,0), Space.World);
  
        //forward movement
        transform.position += transform.forward* Time.deltaTime * movementSpeed;
    }
        
    
    void OnDestroy(){
        Debug.Log(gameObject.name);
        //Fade doesn't work because material isnt transparent, need to figure out work around
       // iTween.FadeTo(gameObject, iTween.Hash("alpha", 0f, "time", 1.0f));
        FindObjectOfType<GameManager>().ChangeNumFishInWater(-1);
    }
}