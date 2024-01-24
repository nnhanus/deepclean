using UnityEngine;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;


public class move : MonoBehaviour
{
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
    public const float moveSpeed = 3f;
    Vector3 plusX = new Vector3(moveSpeed, 0f, 0f);
    Vector3 minusX = new Vector3(-moveSpeed, 0f, 0f);
    Vector3 plusZ = new Vector3(0f, 0f, moveSpeed);
    Vector3 minusZ = new Vector3(0f, 0f, -moveSpeed);
    //Modify this value for the rotation speed control
    private const float joystickRotation = 0.5f;


    // Update is called once per frame
    void Update()
    {
        if (InputDevices.GetDeviceAtXRNode(XRNode.LeftHand).TryGetFeatureValue(CommonUsages.primary2DAxisClick, out triggerValue) && triggerValue) {
            InputDevices.GetDeviceAtXRNode(XRNode.LeftHand).TryGetFeatureValue(CommonUsages.primary2DAxis, out fingerPress);
            Quaternion rotation = Quaternion.Euler(xRController.gameObject.transform.rotation.eulerAngles);
            Matrix4x4 m = Matrix4x4.Rotate(rotation);
            Vector3 translateVect = new Vector3(0, 0, 0);

            if (fingerPress.x < -0.5)
            {
                translateVect = m.MultiplyPoint3x4(minusX);
                //translateVect.y = 0;
            }
            if (fingerPress.y > 0.5)
            {
                translateVect = m.MultiplyPoint3x4(plusZ);
            }
            if (fingerPress.y < -0.5)
            {
                translateVect = m.MultiplyPoint3x4(minusZ);
            }
            if (fingerPress.x > 0.5)
            {
                translateVect = m.MultiplyPoint3x4(plusX);
                //translateVect.y = 0;
            }
            //translateVect.y = 0;
            // TODO: check for building colision,
            //before moving the rig, you can check if it's still in a certain area and modify the translation vector if it's trying to get out
            if (RigContainer.transform.position.y + translateVect.y < minHeight){
                translateVect.y -= minHeight;
            } else if (RigContainer.transform.position.y + translateVect.y > maxHeight){
                translateVect.y -= maxHeight;
            }

            RigContainer.transform.position += translateVect;
        }
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
