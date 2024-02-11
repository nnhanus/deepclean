using UnityEngine;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;
using UnityEngine.InputSystem;
using TMPro;

public class trashbag : MonoBehaviour
{
    // and that as your controller
    public GameObject xRController;
    public TextMeshPro bagDisplay;
    public ImgsFillDynamic gaugeBar;
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
        bagDisplay.SetText(collectedTrash.Count + "/8");
        gaugeBar.SetValue(collectedTrash.Count / 8 * 100);
    }

    public void emptyTrash(){
        FindObjectOfType<GameManager>().AddToBin(collectedTrash.Count);
        collectedTrash.Clear();
        bagDisplay.SetText(collectedTrash.Count + "/8");
        gaugeBar.SetValue(0);
    }
}
