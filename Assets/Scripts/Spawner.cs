using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

// should use game manager to track number of garbage in the ocean and limit it
    public GameObject[] trashPrefabs;

    public GameObject [] fishPrefabs; 

    public bool isVisible = false;

    public float minSpawnDelay = 1f;
    public float maxSpawnDelay = 2f;

    public GameObject player;
    public int spawnLimit = 4;
    public int trashLimit = 20;
    private Vector3 spawnPoint;
    //private float lastRotate=0f;
    public float posVar = 1;
    //private int[] randDirection;
    private int[] direction = {-1,1};
    private int trashCount =0;
    // public float maxLifetime = 5f;
    private AudioSource audioSource;

     private Vector3 spawnBoundsSize;
     private Collider collider;
     public GameManager manager;

    //private void OnEnable()
   // {
    //    StartCoroutine(Spawn());
   // }

    //could change to OnEnable
    private void Start()
    {
         collider = GetComponent<Collider>();
         spawnBoundsSize = 0.5f*collider.bounds.size;
        //  audioSource = GetComponent<AudioSource>();
        //  audioSource.Play();
         trashCount =0;
         StartCoroutine(SpawnTrash());
         //manager = FindObjectOfType<GameManager>();
    }
    

    private void OnDisable()
    {
        StopAllCoroutines();
        //audioSource.Stop();
    }

    private IEnumerator SpawnTrash()
    {
        
        while (true)
        {
            //if player moves more than .25 . can also add when looks arounds more than 5 degrees, spawn more trash
            if (Vector3.Distance (spawnPoint, player.transform.position)>0.25f /*|| Mathf.Abs(player.transform.rotation.eulerAngles.z-lastRotate)>5*/){
                trashCount=0;
            }
            //get player rig location and set it to spawnPoint
            //lastRotate=player.transform.rotation.eulerAngles.z;
            spawnPoint = player.transform.position;
            //if under water and havent spawned too many trash
            if(spawnPoint.y<0 && trashCount< spawnLimit&& manager.NumTrashInWater()<trashLimit ){
                GameObject prefab = trashPrefabs[Random.Range(0, trashPrefabs.Length)];
                var dir=direction[Random.Range(0,2)];
                Vector3 position = new Vector3();
                // min and max for scene bounds
                position.x = Mathf.Max(Mathf.Min(Random.Range(spawnPoint.x+posVar, spawnPoint.x-posVar), spawnBoundsSize.x),-spawnBoundsSize.x);
                position.y = Mathf.Max(Mathf.Min(Random.Range(spawnPoint.y+posVar, spawnPoint.y-posVar),-0.5f),-2*spawnBoundsSize.y+0.5f);
                position.z = Mathf.Max(Mathf.Min(Random.Range(spawnPoint.z+(dir*posVar), spawnPoint.z+(dir*0.1f)),spawnBoundsSize.z),-spawnBoundsSize.z);
                Debug.Log(position.x+","+position.y+","+position.z);
                //spawns in any point around the character that is more than 1 away and less than the variance+2 (ie a spawning box)

                GameObject trash = Instantiate(prefab, position, Quaternion.identity);
                trashCount +=1;
                manager.AddTrash(trash);
                // trash.transform.localScale = new Vector3(0.03f, 0.03f, 0.03f);

            //  Destroy(trash, maxLifetime);

                //float force = Random.Range(minForce, maxForce);
                //trash.GetComponent<Rigidbody>().AddForce(trash.transform.up * force, ForceMode.Impulse);

            }
            
            yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
        }
    }
}