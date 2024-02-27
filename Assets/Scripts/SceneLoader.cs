using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        // this.transform.GetComponent<MeshRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other){
        Debug.Log("Scene Change trigger" + other);
        if (other.tag == "Player"){
            // SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Single);
            GameManager.manager.ChangeScene();
        }
    }
}


