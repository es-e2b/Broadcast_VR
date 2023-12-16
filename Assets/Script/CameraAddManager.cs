using Photon.Pun;
using Photon.Pun.Demo.PunBasics;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
namespace Broadcast.JES
{
    public class CameraAddManager : MonoBehaviourPunCallbacks
    {
        public GameObject CameraPrefab;
        private Vector3 currentPosition;
        bool isCreating = false;
        PlayerAnimatorManager animatorMovement;
        GameObject newCamera;

        private void Start()
        {
            animatorMovement = GetComponent<PlayerAnimatorManager>();
        }

        private void createCamera()
        {
            isCreating = true;
            newCamera = PhotonNetwork.Instantiate(CameraPrefab.name, transform.position, Quaternion.Euler(transform.rotation.x, 2f, transform.rotation.z), 0);
            PlayerMovement cameraMovement = newCamera.GetComponent<PlayerMovement>();
            animatorMovement.enabled = false;
            cameraMovement.enabled = true;
            if (photonView.IsMine)
            {
                PlayerManager.LocalPlayerCamera.GetComponent<FirstPersonCamera>().enabled = false;
                PlayerManager.currentCamera.enabled = false;
            }
            newCamera.GetNamedChild("Main Camera").GetComponent<Camera>().enabled = true;
            newCamera.GetNamedChild("Main Camera").GetComponent<FirstPersonCamera>().enabled = true;
            newCamera.GetComponent<PhotonTransformView>().enabled = true;
        }
        private void setPosition()
        {
            isCreating = false;
            PlayerMovement cameraMovement = newCamera.GetComponent<PlayerMovement>();
            animatorMovement.enabled = true;
            cameraMovement.enabled = false;
            if (photonView.IsMine)
            {
                PlayerManager.LocalPlayerCamera.GetComponent<FirstPersonCamera>().enabled = true;
                PlayerManager.currentCamera.enabled = true;
            }
            newCamera.GetNamedChild("Main Camera").GetComponent<Camera>().enabled = false;
            newCamera.GetNamedChild("Main Camera").GetComponent<FirstPersonCamera>().enabled = false;
            newCamera.GetComponent<PhotonTransformView>().enabled = false;
        }
        private void Update()
        {
            if (!isCreating && photonView.IsMine &&Input.GetKeyDown(KeyCode.F1))
            {
                createCamera();
            }
            if(isCreating && photonView.IsMine && Input.GetMouseButtonDown(0))
            {
                setPosition();
            }
        }
    }
}