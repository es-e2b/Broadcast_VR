using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Broadcast.JES
{
    public class FirstPersonCamera : MonoBehaviourPunCallbacks
    {
        public float sensitivity = 50f;  // 마우스 감도
        public Transform playerBody;    // 플레이어의 Transform

        float verticalRotation = 0f;

        void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;  // 마우스 커서 고정
            Cursor.visible = false;  // 마우스 커서 숨김
        }

        void Update()
        {
            if(transform.parent.GetComponent<PhotonView>().IsMine && Input.GetKeyUp(KeyCode.Escape))
            {
                reverseState();


            }
            if (!Cursor.visible)
            {
                // 마우스 입력 받기
                float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
                float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

                // 플레이어 바디 회전
                playerBody.Rotate(Vector3.up * mouseX);

                // 카메라 회전 (상하)
                verticalRotation -= mouseY;
                verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);
                transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
            }
        }
        void reverseState()
        {
            if(Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }
}