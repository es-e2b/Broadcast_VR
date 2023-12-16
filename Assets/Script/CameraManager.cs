using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Broadcast.JES
{
    public class CameraManager : MonoBehaviour
    {
        public static List<GameObject> Cameras;
        int index = 0;

        private void Awake()
        {
            Cameras = new List<GameObject>();
        }
        private void changeCamera()
        {
            int sizeCameras = Cameras.Count;
            for (int i = 0; i < sizeCameras; i++)
            {
                if (PlayerManager.currentCamera == Cameras[i].GetComponent<Camera>())
                    index = i;
            }
            PlayerManager.currentCamera.enabled = false;
            Cameras[index].GetComponent<FirstPersonCamera>().enabled = false;

            PlayerManager.currentCamera = Cameras[(index + 1) % sizeCameras].GetComponent<Camera>();
            Cameras[(index + 1) % sizeCameras].GetComponent<FirstPersonCamera>().enabled = true;
            PlayerManager.currentCamera.enabled = true;
        }

        // Update is called once per frame
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Tab))
                changeCamera();
        }
    }
}