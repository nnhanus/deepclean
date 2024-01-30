using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] trashPrefabs;

// public GameObject [] fishPrefabs; 

    public bool isVisible = false;

    [Range(0f, 1f)]

    public float minSpawnDelay = 0.25f;
    public float maxSpawnDelay = 1f;

    public float minAngle = -20;
    public float maxAngle = 20;

    public float minForce = 0;
    public float maxForce = 1;

    // public float maxLifetime = 5f;

    public Vector3 spawnBoundsSize;
    private Collider collider;

    //private void OnEnable()
   // {
    //    StartCoroutine(Spawn());
   // }

    
    private void Start()
    {
        collider = GetComponent<Collider>();
        spawnBoundsSize = collider.bounds.size;
         StartCoroutine(Spawn());
    }
    

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator Spawn()
    {
        
        while (enabled)
        {
            Bounds spawnBounds = new Bounds(transform.position, spawnBoundsSize);
            GameObject prefab = trashPrefabs[Random.Range(0, trashPrefabs.Length)];
            Debug.Log(spawnBoundsSize);
            Vector3 position = new Vector3();
            Debug.Log(spawnBounds);
            Vector3 min = spawnBounds.min;
            Vector3 max = spawnBounds.max;
            position.x = Random.Range(min.x, max.x);
            position.y = Random.Range(min.y, max.y);
            position.z = Random.Range(min.z, max.z);

            // random rotation but always towards 'up' of the parent
            //Quaternion rotation = transform.parent.rotation * Quaternion.Euler(Random.Range(minAngle, maxAngle), 0f, Random.Range(minAngle, maxAngle));
            Quaternion rotation = transform.parent.rotation * Quaternion.Euler(0f, 0f,0f);

            GameObject trash = Instantiate(prefab, position, rotation);
            trash.transform.localScale = new Vector3(0.03f, 0.03f, 0.03f);

          //  Destroy(trash, maxLifetime);

            //float force = Random.Range(minForce, maxForce);
            //trash.GetComponent<Rigidbody>().AddForce(trash.transform.up * force, ForceMode.Impulse);

            yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
        }
    }
}