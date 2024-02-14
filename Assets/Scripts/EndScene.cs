using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(AudioSource))]
public class EndScene : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip[] audioClip;
    public GameObject[] trashPrefabs;
    
    private List<Vector3> spawnPoints;
     private Vector3 spawnBoundsSize;
     private Collider collider;
    public GameObject player;
    bool move = false;
   // string next = "wait";
    public GameManager manager;
    public TextMeshPro scoreDisplay;

    //private void OnEnable()
   // {
    //    StartCoroutine(Spawn());
   // }

    //could change to OnEnable
    private void Start()
    {
        manager = FindObjectOfType<GameManager>();
        // get which dive the player is on (max three)
        collider = GetComponent<Collider>();
        spawnBoundsSize = 0.5f*collider.bounds.size;
        audioSource = GetComponent<AudioSource>();
        //set spawnpoints relative to spawning box
        spawnPoints=new List<Vector3>();
        spawnPoints.Add(new Vector3 (-2, 1, spawnBoundsSize.z-2));
        spawnPoints.Add(new Vector3 (0, 5, 0));
        // could spawn trash that was in water in last scene from game manager
        //reset count of trash from each spawn point to zer0
        scoreDisplay.SetText("You collected "+manager.GetScore()+" pieces of garbage. That is 0000000000"+ (manager.GetScore()/52.5f)+"% of the garbage currently in the ocean.");
        Debug.Log("You collected "+manager.GetScore()+" pieces of garbage. That is 0000000000"+ (3/52.5f)+"% of the garbage currently in the ocean.");
               //test
        StartCoroutine(SpawnTrash(5));
    }
    private void Update(){

        //beginning of scene
        // if(/*certain amount of time has passed?*/)StartCoroutine(SpawnTrash(manager.GetScore()));
        // if(/*longer amount of time?*/){
        //     StartCoroutine(SpawnTrash(5000));
        //     move=true;
        // }
        if(move&&player.transform.position.z<10){
            player.transform.position+= new Vector3(0,0,0.25f);
        }
        //move player backwards slowly to get in view, change audio pan as you do
    }
    

    private void OnDisable()
    {
        StopAllCoroutines();
        //audioSource.Stop();
    }

//         StartCoroutine(SpawnTrash());
    private IEnumerator SpawnTrash(int count)
    {
        int index=0;
        if(count>24) {
            index=1;
            audioSource.loop=true;
        }
        Vector3 fallPoint = new Vector3 (spawnPoints[index].x,0,spawnPoints[index].z);
        AudioSource.PlayClipAtPoint(audioClip[index],fallPoint, 0.8f+(0.2f*index));

        //play trash sound once for little pile, multiple times simultaneously with offset for big pile
        for(int i = 0; i<count; i++){
            GameObject prefab = trashPrefabs[Random.Range(0, trashPrefabs.Length)];
            prefab.GetComponent<Rigidbody>().useGravity=true;
            prefab.GetComponent<Collider>().isTrigger = false;
            prefab.GetComponent<Floater>().enabled=false;
            GameObject trash = Instantiate(prefab, spawnPoints[index], Quaternion.identity);
            Debug.Log(trash.transform.position);

        }
            //wait a random amount of time in the determined range between each spawn
            yield return new WaitForSeconds(0.2f);
    }

}