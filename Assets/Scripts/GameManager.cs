using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{

    public static GameManager manager;
    int sceneNum;
    public string sceneName = "Intro_Boat_Scene";
    public int totalScore=0;
    public int numDives = 0;
    bool gameStart = true;
    bool gameStarted = false;

    //we can use this list to load existing trash from before upon entering water
    List<GameObject> trashInWater;
    public List<GameObject> trashInBag;
    int numFish=0;
    private AudioSource audio;
    private AudioClip clip;
    public AudioClip[] audioClips;
    //public GameObject TMP_GO;
    public GameObject dialogueCanvas;
    List<string[]> dialogueSources = new List<string[]>();
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
        //game manager is active once out of loading screen
        //sceneNum=0;
        audio= GetComponent<AudioSource>();
        trashInWater= new List<GameObject>();
        trashInBag = new List<GameObject>();

        Debug.Log(SceneManager.GetActiveScene().name);
        sceneName = SceneManager.GetActiveScene().name;
        gameStarted = false;
        Debug.Log(sceneName);

        SceneManager.sceneLoaded += OnSceneLoaded;
        OnSceneLoaded(SceneManager.GetActiveScene(), LoadSceneMode.Single);
        //Debug.Log(dialogueCanvas);
            
        //dialogueCanvas.SetActive(false);
        dialogueSources.Add(new string[]{""});
        dialogueSources.Add(new string[]{"Plastics are a big catalyst for climate change.","They are made from fossil fuels, take a long time to decompose, and emit greenhouse gasses as they do.","They also interfere with the oceans capacity to absorb carbon dioxide from the air.","More CO2 in the atmosphere means more CO2 in the waters and a higher water acidity.", "These conditions are rapidly killing our coral reefs and threatening the aquatic biodiversity.","By 2050 scientists believe that the oceans will be too hostile for coral to survive. It happens very quickly.", "I started this ocean clean up mission in 2019 and this is what it looked like then."});
        dialogueSources.Add(new string[]{"You'll see how much it's changed when you jump in. As more trash stays in the ocean, the more the coral reefs and its aquatic inhabitants die off."});
        dialogueSources.Add(new string[]{"Hey there!"});
        dialogueSources.Add(new string[]{"Thanks again for helping out with our ocean cleanup. There's 5.25 trillion pieces of garbage ","out here, so what we are doing today will barely make a dent- but every piece counts!"});
        dialogueSources.Add(new string[]{"Your bag can only hold 8 pieces of trash, so when it's full come back up here and dump it in the bin."});
        dialogueSources.Add(new string[]{"Your bag is looking full!"});
        dialogueSources.Add(new string[]{"Woah, nice job!"});
        dialogueSources.Add(new string[]{"I appreciate your effort, but you can do better."});
        dialogueSources.Add(new string[]{"We've only got time for this last dive, so make it count!"});
        dialogueSources.Add(new string[]{"Thanks again for helping out with the clean up! We'd love to have you back out here again sometime."});

        if (sceneName == "Intro_Boat_Scene")
            triggerAudio(3);
        //triggerAudio(1);
        //triggerAudio(2);
    }

    public void OnSceneLoaded(Scene s, LoadSceneMode l)
    {
        Debug.Log(gameStarted);
        if (gameStarted)
        {
            Debug.Log("rotated player");
            GameObject player = GameObject.FindWithTag("Player");
            player.transform.RotateAround(player.transform.position, player.transform.up, -90);
            Debug.Log(GameObject.FindWithTag("Player").transform.rotation.eulerAngles);
            gameStarted = false;
        }
        dialogueCanvas = GameObject.FindGameObjectsWithTag("Dialogue")[0];
        dialogueCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        
    }

//use FindObjectOfType<GameManager>().AddToBin(bagCount); in the script that handles dumpin garbage into the bin
    public void AddToBin(){
        //nice job and do better audios
        if(trashInBag.Count>6) triggerAudio(7);
        else triggerAudio(8);
        totalScore+= trashInBag.Count;
        trashInBag.Clear();
        if (numDives == 3)
            triggerAudio(10);
    }

    //use FindObjectOfType<GameManager>().AddTrashToWater(trash); in the script that handles spawning trash
    public void AddTrashToWater(GameObject trash){
        trashInWater.Add(trash);
    }
    public void RemoveTrashFromWater(GameObject trash){
        trashInWater.Remove(trash);
        trash.GetComponent<Renderer>().enabled = false;
        // Destroy(trash);
    }
    public int GetScore(){
        return totalScore;
    }
     public string GetSceneName(){
        return sceneName;
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
    public void ChangeScene() {
        //if coming out of loading scene, go to boat scene
        if (gameStart) {
            gameStart = false;
            sceneName = "Boat_Scene";
            gameStarted = true;
        }
        //go to end scene if dives are over
        else if (numDives >= 3 && SceneManager.GetActiveScene().name.Equals("Boat_Scene")) {
            sceneName = "End_Scene";
        }
        //otherwise switch between boat and underwater scene
        else if (SceneManager.GetActiveScene().name.Equals("Boat_Scene"))
        {
            sceneName = "Underwater_scene";
            triggerAudio(11);
            numDives+=1;
            if(numDives==3){
                //trigger last dive audio
                triggerAudio(9);
            }
        } 
        else
        {
            sceneName = "Boat_Scene";
            trashInBag.AddRange(FindObjectOfType<TrashPicker>().collectedTrash);
        }
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
    

    //use FindObjectOfType<GameManager>().triggerAudio(clip #);
    public void triggerAudio(int clipIndex){
        StartCoroutine(waitForSound(clipIndex));
    }
    private IEnumerator waitForSound(int clipIndex)
    {
        //Wait Until Sound has finished playing
        while (audio.isPlaying)
        {
            yield return null;
        }

        StartCoroutine(audioPlayer(clipIndex));
        //if(clipIndex<11)
           // StartCoroutine(triggerDialogue(clipIndex, audioClips[clipIndex].length)); 
    }

    private IEnumerator audioPlayer(int clipIndex){
        //0 index is radio sound 1=Intro 2= Intro cont'd 3= Hey there 4=context 5=bag instruct 6= bag full 7=nice job 8= do better 9=last dive 10=outro 11=splash
        clip = audioClips[clipIndex];
        //while(audioSource.isPlaying) {WaitForSeconds(1);}
        if(clipIndex==11){
            audio.clip=clip;
        }
        else{
            if(sceneName.Equals("Underwater_scene")){
                //play primarily in the left ear/
                audio.panStereo=-0.75f;
                //below water play beep
                audio.clip = audioClips[0];
                audio.Play();
            }
            audio.clip=clip;         
        }
        float audioTime = clip.length;
        audio.Play();
       //reset to both ears
        audio.panStereo=0;
       //yield return new WaitForSeconds(audio.clip.length);
    //    yield return null;
    // }
    // private IEnumerator triggerDialogue(int clipIndex, float audioTime){
        if(clipIndex<11 && !sceneName.Equals("Underwater_scene")){
            dialogueCanvas.SetActive(true);
            Debug.Log(dialogueCanvas.activeSelf);
            TMP_Text textMeshPro = FindObjectOfType<TMP_Text>();
            //Debug.Log(" text : " + textMeshPro.text);

            //TextMeshPro textMeshPro = TMP_GO.GetComponent<TextMeshPro>();
            string[] dialogue = dialogueSources[clipIndex]; 
            foreach(string phrase in dialogue){
                dialogueCanvas.SetActive(true);
                textMeshPro.text = phrase;
                Debug.Log(phrase +" "+audioTime / dialogue.Length);
                yield return new WaitForSeconds(audioTime/dialogue.Length);
                dialogueCanvas.SetActive(false);
            }
            if (clipIndex == 3 && gameStart)
                triggerAudio(1);
            else if (clipIndex == 1)
                triggerAudio(2);
            else if (clipIndex == 2)
                ChangeScene();
            //else if (clipIndex == 4)
                //triggerAudio(5);
        }
        yield return null;
    }
}
