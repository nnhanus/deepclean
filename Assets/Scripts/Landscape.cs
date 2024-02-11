using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Landscape handles the creation of coral at the beginning of each dive.
// As the game progresses, less coral is present and the colour dies off
public class Landscape : MonoBehaviour
{
    public GameManager manager;
    //all corals in scene
     GameObject[] corals;
     //corals to spawn on each dive
     public GameObject[] extraCorals;
     //positions to spawn each coral at
     public Vector3[] positions;
     public Color[] coralColor;
    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<GameManager>();
        //create coral based on dive number
        for( int i= (manager.GetDiveNum()-1)*10; i<extraCorals.Length ;i++)
        {
            Instantiate(extraCorals[i], positions[i], new Quaternion(0,Random.Range(-Mathf.PI, Mathf.PI),0,1));
        }
        //find all corals in the scene (some are permanent and not spawned by this script)
        if (corals == null){
            corals = GameObject.FindGameObjectsWithTag("Coral");
        }
        //change the color of all corals based on dive number
         foreach (GameObject coral in corals)
        {
            coral.GetComponent<Renderer>().material.color = coralColor[manager.GetDiveNum()-1];
        }
    }

    // Update is called once per frame
    void Update()
    {
         // could use something like this if we want the colour to gradually change inside each dive
        // https://forum.unity.com/threads/how-to-gradualy-change-the-colour-of-a-material-via-script.458293/
        // foreach (GameObject coral in corals)
        // {
        //     //change colour
        // }
    }
   
}
