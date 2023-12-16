using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Broadcast.JES
{
    public class CameraObject : MonoBehaviour
    {
        void Start()
        {
            CameraManager.Cameras.Add(gameObject);
        }
    }
}