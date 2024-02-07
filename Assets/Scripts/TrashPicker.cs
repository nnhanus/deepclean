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

    public GameObject grabbed;
    public bool hasTrash;
    private Quaternion initialRotation;
    private AudioSource audioSource;

    public GameObject trashBag;
    public List<GameObject> collectedTrash; //change to whatever the type for trash is

    // Start is called before the first frame update
    void Start()
    {
        collectedTrash = new List<GameObject>();
        hasTrash = false;
        audioSource = GetComponent<AudioSource>();
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
        if( hasTrash && m_GripReference.action.ReadValue<float>() < float.Epsilon)
        {
            emptyTrash();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.tag == "Trash")
        {
            //Debug.Log("hit trash");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Trash" && m_GripReference.action.ReadValue<float>() > float.Epsilon && !hasTrash)
        {
            hasTrash = true;
           // Debug.Log("pick trash");
            grabbed = other.gameObject;
            grabbed.GetComponent<Floater>().enabled = false;
           // Debug.Log(grabbed);
            
            initialRotation = grabbed.transform.rotation;
        }
        if (hasTrash)
        {
           // Debug.Log("has trash");
            grabbed.transform.SetParent(transform);

            //play movement sound
            audioSource.Play();
            //grabbed.transform.SetPositionAndRotation(transform.position, transform.rotation);
            //grabbed.transform.position = transform.position;
            //grabbed.transform.localScale = transform.localScale;
        }
        if (other.gameObject == trashBag
          &&  gripReference != null
          && gripReference.action != null
          && gripReference.action.ReadValue<float>() > float.Epsilon
          && collectedTrash.Count < 10
          && hasTrash) //check if the collisison is with the controller so it doesn't happen when random trash hits it or something
        {
            GameObject trash = grabbed;
            collectedTrash.Add(trash);
            trashToBag();
            FindObjectOfType<GameManager>().RemoveTrash(trash);
        }
    }

    public void trashToBag()
    {
        grabbed.transform.SetParent(null);
        grabbed.GetComponent<Floater>().enabled = true;
        grabbed = null;
        hasTrash = false;
    }
}
