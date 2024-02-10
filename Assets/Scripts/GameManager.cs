using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager manager;
    //sceneNum 0 = boat scene 1=underwater scene 2=load scene 3=end scene
    int sceneNum;
    int totalScore=0;
    public int numDives = 0;
    bool gameStart =true;
    
    //hoping we can use this list to load existing trash from before upon entering water
    List<GameObject> trashInWater;
    List<GameObject> trashInBag;
    int numFish;
    private AudioSource splash;
    // Start is called before the first frame update
    private void Awake(){
        if (manager == null){
            manager =this;
            DontDestroyOnLoad(this);
        }else if(manager !=this){
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        sceneNum=0;
        splash= GetComponent<AudioSource>();
        trashInWater= new List<GameObject>();
        trashInBag = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

//use FindObjectOfType<GameManager>().AddToBin(bagCount); in the script that handles dumpin garbage into the bin
    public void AddToBin(int bagCount){
        totalScore+=bagCount;
        trashInBag.Clear();
    }

    //use FindObjectOfType<GameManager>().AddTrashToWater(trash); in the script that handles spawning trash
    public void AddTrashToWater(GameObject trash){
        trashInWater.Add(trash);
    }
    public void RemoveTrashFromWater(GameObject trash){
        trashInWater.Remove(trash);
        Destroy(trash);
    }

    public int NumTrashInWater(){
        Debug.Log(trashInWater.Count);
        return trashInWater.Count;
    }
    public int NumFishInWater(){
        return numFish;
    }
    public void ChangeNumFishInWater(int diff){
        numFish+=diff;
    }
    public int GetDiveNum(){
           // Debug.Log(trashInWater.Count);
            return numDives;
        }
//use FindObjectOfType<GameManager>().ChangeScene(); in the script that handles the trigger area for moving from boat to water
    public void ChangeScene(){
        if(gameStart){
            gameStart=false;
            sceneNum = 0;
            }
        else if (numDives>=3){
            sceneNum=3;
        }
        else{
            sceneNum= sceneNum*(-1)+1;
        }
        if(sceneNum==1){
            splash.Play();
            numDives+=1;
        }else{
            trashInBag.AddRange(FindObjectOfType<TrashPicker>().collectedTrash);
        }
        SceneManager.LoadScene(sceneNum);
    }
}
