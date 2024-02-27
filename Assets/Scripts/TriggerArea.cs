using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerArea : MonoBehaviour
{
    public GameObject player;
    public GameManager manager;
    int phraseIndex=0;
    // Start is called before the first frame update
    void Start()
    {
        manager = GameManager.manager;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other){
        if (other.gameObject == player)
        {
            Debug.Log("Trigger area" + phraseIndex);
            if(phraseIndex==1)
                manager.triggerAudio(3);
            manager.triggerAudio(4+phraseIndex);
            phraseIndex=phraseIndex*(-1)+1;

        }
    }

    public void OnTriggerExit(Collider other){
        if (other.gameObject == player){
            Debug.Log("Trigger exit");
        }
    }
}
