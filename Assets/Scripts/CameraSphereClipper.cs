using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CameraSphereClipper : MonoBehaviour
{
    public Camera mainCam;
    [SerializeField]
    public float[] distances = new float[32];
    
    // Start is called before the first frame update
    void Start()
    {
        distances[0] = 20; //smaller than your camera clipping planes's far value
        distances[6] = 500; //smaller than your camera clipping planes's far value
        mainCam.layerCullDistances = distances;
        mainCam.layerCullSpherical = true;
    }

    // Update is called once per frame
    void Update()
    {
        mainCam.layerCullDistances = distances;
    }
}
