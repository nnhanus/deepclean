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
    private List<Vector3> spawnPoints;
    public float posVar = 1;

    private int[] trashCounts;
    private bool[] inArea={false,false,false,false};
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
        spawnPoints=new List<Vector3>();
        spawnPoints.Add(new Vector3 (spawnBoundsSize.x/2, -spawnBoundsSize.y/2, spawnBoundsSize.z/2));
        spawnPoints.Add(new Vector3 (-spawnBoundsSize.x/2, -spawnBoundsSize.y/2, spawnBoundsSize.z/2));
        spawnPoints.Add(new Vector3 (spawnBoundsSize.x/2, -spawnBoundsSize.y/2, -spawnBoundsSize.z/2));
        spawnPoints.Add(new Vector3 (-spawnBoundsSize.x/2, -spawnBoundsSize.y/2, -spawnBoundsSize.z/2));
        // trashCount =0;
         trashCounts= new int[] {0,0,0,0};
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
            for(int i=0; i<4; i++){
                var distance = Vector3.Distance(spawnPoints[i], player.transform.position);
                var line = (spawnPoints[i] - player.transform.position).normalized;
                //if under water and havent spawned too many trash
                    //if player is further than 5 away or not facing spawn point
                if (trashCounts[i]< spawnLimit&& manager.NumTrashInWater()<trashLimit && (distance>5.0f || Vector3.Dot(line, player.transform.forward)<0.5f)){
                    if (inArea[i]){
                        trashCounts[i]=0;
                        inArea[i]=false;
                    }
                    GameObject prefab = trashPrefabs[Random.Range(0, trashPrefabs.Length)];
                    Vector3 position = new Vector3();
                    // min and max for scene bounds
                    position.x = Random.Range(spawnPoints[i].x+posVar, spawnPoints[i].x-posVar);
                    position.y = Random.Range(spawnPoints[i].y+posVar, spawnPoints[i].y-posVar);
                    position.z = Random.Range(spawnPoints[i].z+posVar, spawnPoints[i].z-posVar);
                    Debug.Log(position.x+","+position.y+","+position.z);
                    //spawns in any point around the character that is more than 1 away and less than the variance+2 (ie a spawning box)

                    GameObject trash = Instantiate(prefab, position, Quaternion.identity);
                    trashCounts[i] +=1;
                    manager.AddTrashToWater(trash);
                //  Destroy(trash, maxLifetime);

                }
                else if(distance<1.5f){
                    inArea[i]=true;
                }
            }
            
            yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
        }
    }
}