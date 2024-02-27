using UnityEngine;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;
using UnityEngine.InputSystem;


public class move : MonoBehaviour
{
    [SerializeField]
    private InputActionReference m_TriggerReference;
    public InputActionReference triggerReference { get => m_TriggerReference; set => m_TriggerReference = value; }

    [SerializeField]
    private InputActionReference m_AxisReference;
    public InputActionReference axisReference { get => m_AxisReference; set => m_AxisReference = value; }
    public float[] xRange;
    public float[] zRange;
    // Define these two as the XRRig in Unity
    public GameObject RigContainer;
    // and that as your controller
    public GameObject xRController;

    //These are values used just by the script
    public float minHeight;
    public float maxHeight;
    bool triggerValue, up, down;
    Vector2 fingerPress;
    //Modify this value for the move speed control
    public float moveSpeed = 0.03f;
    Vector3 plusX;
    Vector3 minusX;
    Vector3 plusZ;
    Vector3 minusZ;
    //Modify this value for the rotation speed control
    private const float joystickRotation = 0.5f;

    private void Start()
    {
        Vector3 plusX = new Vector3(moveSpeed, 0f, 0f);
        Vector3 minusX = new Vector3(-moveSpeed, 0f, 0f);
        Vector3 plusZ = new Vector3(0f, 0f, moveSpeed);
        Vector3 minusZ = new Vector3(0f, 0f, -moveSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 translateVect = new Vector3(0, 0, 0);
        if (triggerReference != null
            && triggerReference.action != null
            && triggerReference.action.ReadValue<float>() > 0.5
            && axisReference != null 
            && axisReference.action != null)
        {
            Vector2 value = axisReference.action.ReadValue<Vector2>();
            //Vector3 translateVect = new Vector3(0, 0, 0);
            Quaternion rotation = Quaternion.Euler(xRController.gameObject.transform.rotation.eulerAngles);
            Matrix4x4 m = Matrix4x4.Rotate(rotation);
            
           // Debug.Log("x = " + value.x + ", y = " + value.y);

            if (value.x < -0.25)
            {
                translateVect += m.MultiplyPoint3x4(minusX);
                //translateVect.y = 0;
            }
            if (value.y > 0.25)
            {
                translateVect += m.MultiplyPoint3x4(plusZ);
            }
            if (value.y < -0.25)
            {
                translateVect += m.MultiplyPoint3x4(minusZ);
            }
            if (value.x > 0.25)
            {
                translateVect += m.MultiplyPoint3x4(plusX);
                //translateVect.y = 0;
            }

            //translateVect *= value;
            //translateVect.y = 0;

            // TODO: check for object colision,
            //before moving the rig, you can check if it's still in a certain area and modify the translation vector if it's trying to get out
            if (RigContainer.transform.position.y + translateVect.y < minHeight){
                translateVect.y = 0;
            } else if (RigContainer.transform.position.y + translateVect.y > maxHeight){
                translateVect.y = 0;
            }
            if (xRange.Length >= 2 && zRange.Length >= 2)
            {
                if (RigContainer.transform.position.x + translateVect.x < xRange[0] || RigContainer.transform.position.x + translateVect.x > xRange[1]){
                     translateVect.x = 0;
                } 
                 if (RigContainer.transform.position.z + translateVect.z < zRange[0] || RigContainer.transform.position.z + translateVect.z > zRange[1]){
                     translateVect.z = 0;
                } 
            }


            RigContainer.transform.position += translateVect;
        }

        //arrow movement for testing purposes below

        float speed = 1f;

        if (Input.GetKey(KeyCode.RightArrow))
        {
            translateVect.z = speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            translateVect.z = - speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            translateVect.x = speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            translateVect.x= - speed * Time.deltaTime;
        }
        if (RigContainer.transform.position.x + translateVect.x < xRange[0] || RigContainer.transform.position.x + translateVect.x > xRange[1]){
                 translateVect.x = 0;
        } 
        if (RigContainer.transform.position.z + translateVect.z < zRange[0] || RigContainer.transform.position.z + translateVect.z > zRange[1]){
                 translateVect.z = 0;
        } 
        RigContainer.transform.position += translateVect;
        //arrow movement for testing purposes above

        //Here I tried to use the volume key as up and down but didn't work
        // if (InputDevices.GetDeviceAtXRNode(XRNode.LeftHand).TryGetFeatureValue(CommonUsages.primaryButton, out up) && up && RigContainer.transform.position.y < 100)
        // {
        //     RigContainer.transform.position += new Vector3(0, moveSpeed, 0);
        // }
        // if (InputDevices.GetDeviceAtXRNode(XRNode.LeftHand).TryGetFeatureValue(CommonUsages.secondaryButton, out down) && down && RigContainer.transform.position.y > 0)
        // {
        //     RigContainer.transform.position -= new Vector3(0, moveSpeed, 0);
        // }
    }
}
