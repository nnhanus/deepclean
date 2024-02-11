using UnityEngine;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;
using UnityEngine.InputSystem;


public class trashbag : MonoBehaviour
{

    [SerializeField]
    private InputActionReference m_GripReference;
    public InputActionReference gripReference { get => m_GripReference; set => m_GripReference = value; }

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

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addTrashToBag(GameObject trash){
        collectedTrash.Add(trash);
    }

    public void emptyTrash(){
        collectedTrash.Clear();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("OnTriggerEnter bag");
    }
}
