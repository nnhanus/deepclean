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

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("OnTriggerEnter bag");
        // if (gripReference != null
        //   && gripReference.action != null
        //   && gripReference.action.ReadValue<float>() > float.Epsilon
        //   && collectedTrash.Count < 10
        //   && collision.gameObject == xRController
        //   && trashPicker.hasTrash) //check if the collisison is with the controller so it doesn't happen when random trash hits it or something
        // {
        //     Debug.Log("we're in");
        //     GameObject trash = trashPicker.grabbed; //get the trash from the controller
        //     collectedTrash.Add(trash);
        //     trashPicker.trashToBag();


        // }
    }
}
