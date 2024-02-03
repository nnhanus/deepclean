//====================== Copyright 2017-2023, HTC.Corporation. All rights reserved. ======================
using UnityEngine;
using System;
using System.Runtime.InteropServices;

namespace HTC.UnityPlugin.Vive3DSP
{
    [AddComponentMenu("VIVE/3DSP_AudioOcclusion/Raycast")]
    [HelpURL("https://hub.vive.com/storage/3dsp/vive_3dsp_audio_sdk_unity_plugin.html#audio-occlusion-settings")]
    public class Vive3DSPAudioRaycastOcclusion : MonoBehaviour
    {
        public OccGeometryMode OcclusionGeometry
        {
            get { return occlusionGeometry; }
            private set { occlusionGeometry = value; }
        }
        [SerializeField]
        private OccGeometryMode occlusionGeometry;

        public int RayNumber
        {
            get { return rayNumber; }
        }
        [SerializeField]
        private int rayNumber = 12;

        public bool OcclusionEffect
        {
            set { occlusionEffect = value; }
            get { return occlusionEffect; }
        }
        [SerializeField]
        private bool occlusionEffect = true;

        public OccMaterial OcclusionMaterial
        {
            set { occlusionMaterial = value; }
            get { return occlusionMaterial; }
        }
        [SerializeField]
        private OccMaterial occlusionMaterial = OccMaterial.Curtain;

        public float OcclusionIntensity
        {
            set { occlusionIntensity = value; }
            get { return occlusionIntensity; }
        }
        [SerializeField]
        private float occlusionIntensity = 1.5f;

        public float HighFreqAttenuation
        {
            set { highFreqAttenuation = value; }
            get { return highFreqAttenuation; }
        }
        [SerializeField]
        private float highFreqAttenuation = -50.0f;

        public float LowFreqAttenuationRatio
        {
            set { lowFreqAttenuationRatio = value; }
            get { return lowFreqAttenuationRatio; }
        }
        [SerializeField]
        private float lowFreqAttenuationRatio = 0.0f;

        public float OcclusionAngle
        {
            set { occlusionAngle = value; }
            get { return occlusionAngle; }
        }
        [SerializeField]
        private float occlusionAngle = 30.0f;

        private VIVE_3DSP_OCCLUSION_PROPERTY occProperty;
        public bool OcclusionCompEffect;
        
        public IntPtr PropPtr
        {
            get
            {
                if (propPtr == IntPtr.Zero)
                {
                    propPtr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(VIVE_3DSP_OCCLUSION_PROPERTY))); 
                }
                return propPtr;
            }
            private set
            {
                if (propPtr != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(propPtr);
                }
                propPtr = value;
            }
        }
        private IntPtr propPtr = IntPtr.Zero;

        public int Id
        {
            get { return id; }
            private set { id = value; }
        }
        private int id = -1;

        void Awake() {}

        void OnEnable()
        {
            OcclusionGeometry = OccGeometryMode.Preserved;
            OcclusionCompEffect = true;
        }

        void OnDisable()
        {
            occProperty.enable = 0u;
            OcclusionCompEffect = false;
        }

        void Start()
        {
            Id = Vive3DSPAudio.CreateRaycastOcclusion(this);
            occProperty.id = Id;
        }

        void OnDestroy()
        {
            PropPtr = IntPtr.Zero;
            Vive3DSPAudio.DestroyRaycastOcclusion(this);
            Id = -1;
        }

        void Update()
        {
            //occProperty.id = Id;
            occProperty.enable = OcclusionEffect ? 1u : 0u;

            occProperty.density = OcclusionIntensity;
            occProperty.material = (uint)OcclusionMaterial;
            occProperty.posX = transform.position.x;
            occProperty.posY = transform.position.y;
            occProperty.posZ = transform.position.z;
            occProperty.rhf = HighFreqAttenuation;
            occProperty.lfratio = LowFreqAttenuationRatio;
            occProperty.mode = (uint)OccGeometryMode.Preserved;

            Marshal.StructureToPtr(occProperty, PropPtr, true);
        }
    }
}

