using UnityEngine;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;
using UnityEngine.InputSystem;

public class TrashPicker : MonoBehaviour
{
    [SerializeField]
    private InputActionReference m_GripReference;
    public InputActionReference gripReference { get => m_GripReference; set => m_GripReference = value; }

    public Collider handSphere;

    private GameObject grabbed;
    public bool hasTrash;
    private Quaternion initialRotation;
    // Start is called before the first frame update
    void Start()
    {
        hasTrash = false;
    }

    // Update is called once per frame
    void Update()
    {
        //if (hasTrash)
        //{
           // Debug.Log("has trash");
          //  grabbed.transform.SetPositionAndRotation(transform.position, initialRotation * transform.rotation);
           // grabbed.transform.localScale = transform.localScale;
        //}
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.tag == "Trash")
        {
            Debug.Log("hit trash");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Trash" && m_GripReference.action.ReadValue<float>() > float.Epsilon && !hasTrash)
        {
            hasTrash = true;
            Debug.Log("pick trash");
            grabbed = other.gameObject;
            Debug.Log(grabbed);
            
            initialRotation = grabbed.transform.rotation;
        }
        if (hasTrash)
        {
            Debug.Log("has trash");
            //grabbed.transform.SetPositionAndRotation(transform.position, transform.rotation);
            grabbed.transform.position = transform.position;
            //grabbed.transform.localScale = transform.localScale;
        }
    }

    public void emptyTrash()
    {
        grabbed = null;
        hasTrash = false;
    }
}
