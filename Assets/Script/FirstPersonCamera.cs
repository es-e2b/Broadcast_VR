using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Broadcast.JES
{
    public class FirstPersonCamera : MonoBehaviourPunCallbacks
    {
        public float sensitivity = 50f;  // ���콺 ����
        public Transform playerBody;    // �÷��̾��� Transform

        float verticalRotation = 0f;

        void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;  // ���콺 Ŀ�� ����
            Cursor.visible = false;  // ���콺 Ŀ�� ����
        }

        void Update()
        {
            if(transform.parent.GetComponent<PhotonView>().IsMine && Input.GetKeyUp(KeyCode.Escape))
            {
                reverseState();


            }
            if (!Cursor.visible)
            {
                // ���콺 �Է� �ޱ�
                float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
                float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

                // �÷��̾� �ٵ� ȸ��
                playerBody.Rotate(Vector3.up * mouseX);

                // ī�޶� ȸ�� (����)
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