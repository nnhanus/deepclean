using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DumpsterArea : MonoBehaviour
{
    public GameObject player;
    public AudioSource doBetter;
    public AudioSource goodJob;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other){
        if (other.gameObject == player){
            // Debug.Log("Trigger area");
            float random = Random.Range(0f,1f);
            animator.SetTrigger("Open");
            Debug.Log("random: " + random);
            if (random < 0.33f){
                doBetter.Play();   
            } else if (random < 0.66f){
                goodJob.Play();
            }
        }
        //we should change it to quantity of trash in bag determines between the 2 sounds and then 50% chance of saying something
    }

    public void OnTriggerExit(Collider other){
        if (other.gameObject == player){
            // Debug.Log("Trigger exit");
            // animator.SetTrigger("Close");
        }
    }
}
