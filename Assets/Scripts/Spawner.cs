using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    
    public bool isVisible = false;
    private AudioSource audioSource;
    public GameObject[] trashPrefabs;
    
    public float minSpawnDelay = 1f;
    public float maxSpawnDelay = 2f;
    public int trashSpawnLimit = 4;
    public int trashLimit = 10;
    private List<Vector3> spawnPoints;
    private bool[] inArea={false,false,false,false};
     private Vector3 spawnBoundsSize;
     private Collider collider;
    public float posVar = 1;
    private int[] trashCounts;

    public GameObject [] fishPrefabs; 

    public GameObject player;
    public GameManager manager;
    int numDive;

    //private void OnEnable()
   // {
    //    StartCoroutine(Spawn());
   // }

    //could change to OnEnable
    private void Start()
    {
        manager = FindObjectOfType<GameManager>();
        // get which dive the player is on (max three)
        numDive = manager.GetDiveNum();

        collider = GetComponent<Collider>();
        spawnBoundsSize = 0.5f*collider.bounds.size;
        //  audioSource = GetComponent<AudioSource>();
        //  audioSource.Play();

        //set spawnpoints relative to spawning box
        spawnPoints=new List<Vector3>();
        spawnPoints.Add(new Vector3 (spawnBoundsSize.x/2, -spawnBoundsSize.y/2, spawnBoundsSize.z/2));
        spawnPoints.Add(new Vector3 (-spawnBoundsSize.x/2, -spawnBoundsSize.y/2, spawnBoundsSize.z/2));
        spawnPoints.Add(new Vector3 (spawnBoundsSize.x/2, -spawnBoundsSize.y/2, -spawnBoundsSize.z/2));
        spawnPoints.Add(new Vector3 (-spawnBoundsSize.x/2, -spawnBoundsSize.y/2, -spawnBoundsSize.z/2));
        // could spawn trash that was in water in last scene from game manager
        //reset count of trash from each spawn point to zero
         trashCounts= new int[] {0,0,0,0};
         StartCoroutine(SpawnTrash());
         StartCoroutine(SpawnFish());
         
    }
    

    private void OnDisable()
    {
        StopAllCoroutines();
        //audioSource.Stop();
    }

// SpawnTrash() handles the creation of trash objects in the water
    private IEnumerator SpawnTrash()
    {
        //number of trash objects in water depends on dive number
        trashLimit=trashLimit*numDive;   
        while (true)
        {
            //for each spawn point
            for(int i=0; i<4; i++){
                var distance = Vector3.Distance(spawnPoints[i], player.transform.position);
                var line = (spawnPoints[i] - player.transform.position).normalized;
                //if under water and havent spawned too many trash
                    //if player is further than 5 away or not facing spawn point
                if (trashCounts[i]< trashSpawnLimit&& manager.NumTrashInWater()<trashLimit && (distance>5.0f || Vector3.Dot(line, player.transform.forward)<0.5f)){
                    //if the player was close to the spawn point and has now left, can spawn more trash at this location
                    if (inArea[i]){
                        trashCounts[i]=0;
                        inArea[i]=false;
                    }
                    //select a trash object
                    GameObject prefab = trashPrefabs[Random.Range(0, trashPrefabs.Length)];
                    Vector3 position = new Vector3();
                    // find a position within a distance of 1 from the spawn point in any direction
                    position.x = Random.Range(spawnPoints[i].x+posVar, spawnPoints[i].x-posVar);
                    position.y = Random.Range(spawnPoints[i].y+posVar, spawnPoints[i].y-posVar);
                    position.z = Random.Range(spawnPoints[i].z+posVar, spawnPoints[i].z-posVar);
                    //Debug.Log(position.x+","+position.y+","+position.z);
                    GameObject trash = Instantiate(prefab, position, Quaternion.identity);
                    trashCounts[i] +=1;
                    //manager tracks which object is in the water
                    manager.AddTrashToWater(trash);

                }
                //if the player is in the spawning box, notify that they have been in the area for resetting of trashCount
                else if(distance<1.5f){
                    inArea[i]=true;
                }
            }
            //wait a random amount of time in the determined range between each spawn
            yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
        }
    }

    //SpawnFish() handles the creation and destruction of fish objects
    // heavily dependent on the dive number -  further in the game, less fish spawn and more exotic fish don't spawn
     private IEnumerator SpawnFish()
    {   
       // if(numDive==0){numDive=1;}
       //less fish spawn as the player progresses
        float fishSpawnLimit=Mathf.Ceil(30/numDive);
        //fish alive anywhere from 8s to (25,22,19)s (dive dependent)
        float minLifeTime= 8;
        float maxLifeTime = (28-3*numDive);

        //fish spawn more frequently earlier in the game
        float minFishDelay= numDive/3;
        float maxFishDelay=numDive*2;
        while (true)
        {
                //if under water and havent spawned too many fish
                if (manager.NumFishInWater()< fishSpawnLimit){
                    //select which fish to spawn based on dive number
                    int fishIncl=fishPrefabs.Length-(numDive-1)*2;
                    GameObject prefab = fishPrefabs[Random.Range(0, fishIncl)];
                    //spawn anywhere in play area that is more than 3 away from player and less than 15 away
                    Vector3 offset = Random.onUnitSphere * Random.Range(3, 15);
                    Vector3 position = new Vector3();
                    // min and max for scene bounds
                    position.x = player.transform.position.x+offset.x;
                    if(Mathf.Abs(position.x)>spawnBoundsSize.x){position.x=player.transform.position.x-offset.x;}
                    position.y = Random.Range(-12.0f,-3.0f);
                    position.z = player.transform.position.z+offset.z;
                    if(Mathf.Abs(position.z)>spawnBoundsSize.z){position.x=player.transform.position.z-offset.z;}
                    //Debug.Log(position.x+","+position.y+","+position.z);

                    GameObject fish = Instantiate(prefab, position, Quaternion.identity);
                    manager.ChangeNumFishInWater(1);
                    Destroy(fish, Random.Range(minLifeTime, maxLifeTime));

                }
            //wait a random amount of time in the determined range between each spawn
            yield return new WaitForSeconds(Random.Range(minFishDelay, maxFishDelay));
            }
        }
    
}