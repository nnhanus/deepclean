using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DumpsterArea : MonoBehaviour
{
    public GameObject grip;
    public TrashPicker trashPicker;
    public AudioSource doBetter;
    public AudioSource goodJob;
    public AudioSource trashSounds;
    public AudioSource openTrash;
    // public GameObject trashBag;
    private trashbag trashBag_Script;

    // Start is called before the first frame update
    void Start()
    {
        // trashBag_Script = trashBag.GetComponent<trashbag>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other){
        Debug.Log("Trigger Enter");
        Debug.Log(other.gameObject);
        if (other.gameObject == grip){
            Debug.Log("Opened");
            openTrash.Play();
            this.transform.GetChild(0).Rotate(0f,-90f,0f);
            this.transform.GetChild(1).Rotate(0f,-90f,0f);
        }
        
    }
    public void OnTriggerStay(Collider other){
        if (other.gameObject == grip && other.transform.up.y > 0f && trashPicker.hasBag){
            trashSounds.Play();
            // trashBag_Script.emptyTrash();
        }
    }

    public void OnTriggerExit(Collider other){
        if (other.gameObject == grip){
            
            //  float random = Random.Range(0f,1f);
            // Debug.Log("random: " + random);
            // if (random < 0.33f){
            //     doBetter.Play();   
            // } else if (random < 0.66f){
            //     goodJob.Play();
            // }
            // //we should change it to quantity of trash in bag determines between the 2 sounds and then 50% chance of saying something
            this.transform.GetChild(0).Rotate(0f,90f,0f);
            this.transform.GetChild(1).Rotate(0f,90f,0f);
        }
    }
}
