using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroScene : MonoBehaviour
{
    
    public bool isVisible = false;
    public AudioSource audioSource;
     private Vector3 spawnBoundsSize;
     private Collider collider;

    public GameObject [] fishPrefabs; 

    public GameObject player;
    public GameManager manager;
    public int fishCount = 0;

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
        //audioSource = GetComponent<AudioSource>();
        audioSource.Play();

        StartCoroutine(SpawnFish());
         
    }
    

    private void OnDisable()
    {
        StopAllCoroutines();
        //audioSource.Stop();
    }

    //SpawnFish() handles the creation and destruction of fish objects
    // heavily dependent on the dive number -  further in the game, less fish spawn and more exotic fish don't spawn
     private IEnumerator SpawnFish()
    {   
        //fish spawn more frequently earlier in the game
        float minFishDelay= 1;
        float maxFishDelay=3;
        while (fishCount<30)
        {
                    //select which fish to spawn based on dive number
                    int fishIncl=fishPrefabs.Length;
                    GameObject prefab = fishPrefabs[Random.Range(0, fishIncl)];
                    //spawn anywhere in play area that is more than 3 away from player and less than 15 away
                    Vector3 offset = Random.onUnitSphere * Random.Range(3, 15);
                    Vector3 position = new Vector3();
                    // min and max for scene bounds
                    position.x = player.transform.position.x+offset.x;
                    if(Mathf.Abs(position.x)>spawnBoundsSize.x){position.x=player.transform.position.x-offset.x;}
                    position.y = Random.Range(-12.0f,-3.0f);
                    position.z = player.transform.position.z+offset.z;
                    if(Mathf.Abs(position.z)>spawnBoundsSize.z){position.z=player.transform.position.z-offset.z;}
                    //Debug.Log(position.x+","+position.y+","+position.z);

                    GameObject fish = Instantiate(prefab, position, Quaternion.identity);
                    //Destroy(fish, Random.Range(minLifeTime, maxLifeTime));

            //wait a random amount of time in the determined range between each spawn
            yield return new WaitForSeconds(Random.Range(minFishDelay, maxFishDelay));
            }
        }
    
}