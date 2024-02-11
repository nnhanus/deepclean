using UnityEngine;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;
using UnityEngine.InputSystem;


public class trashbag : MonoBehaviour
{
    // and that as your controller
    public GameObject xRController;

    private TrashPicker trashPicker;

    public List<GameObject> collectedTrash; //change to whatever the type for trash is

    // Start is called before the first frame update
    void Start()
    {
        collectedTrash = new List<GameObject>();
        trashPicker = xRController.GetComponent<TrashPicker>();
    }

    public void addTrashToBag(GameObject trash){
        collectedTrash.Add(trash);
    }

    public void emptyTrash(){
        FindObjectOfType<GameManager>().AddToBin(collectedTrash.Count);
        collectedTrash.Clear();
    }
}
