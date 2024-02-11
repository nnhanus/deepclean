using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWithPlayer : MonoBehaviour
{
    public Transform player;

     void Update()
     {
          if(player != null)
          {
               transform.LookAt(player);
          }
     }
}
