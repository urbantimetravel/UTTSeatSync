using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script transfers the rotation of the VR goggles to the head of the character
/// </summary>
namespace UrbanTimetravel.SeatSync
{
    public class ControlHeadRotation : MonoBehaviour
    {
        public bool controlHead = true;
        [HideInInspector] public Transform target;

        void Update()
        {
            if (controlHead)
            {
                if (target != null)
                {
                    var rot = Camera.main.transform.rotation;
                    target.rotation = rot;
                }
            }
        }
    }

}
