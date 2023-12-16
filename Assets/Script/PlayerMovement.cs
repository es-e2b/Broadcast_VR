using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Broadcast.JES
{
    public class PlayerMovement : MonoBehaviourPun
    {
        #region Private Fields
        Transform playerTransform;
        #endregion

        #region Public Fields
        public float speed = 5f;
        #endregion
        #region MonoBehaivour Callbacks

        private void Start()
        {
            playerTransform=this.gameObject.transform;
            if (photonView.IsMine)
                Debug.Log("Cghec");
        }
        void Update()
        {
            if (photonView.IsMine)
            {
                // ���� �� ���� �Է��� �޾� �̵� ������ ���
                float horizontalInput = Input.GetAxis("Horizontal");
                float verticalInput = Input.GetAxis("Vertical");
                float upInput = Input.GetAxis("Jump");
                float downInput = -Input.GetAxis("Sprint");
                Vector3 updown = new Vector3(0f, upInput+downInput, 0f);
                Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput)+updown;

                // �̵� ���⿡ �ӵ��� ���Ͽ� �̵� ���� ����
                Vector3 moveDirection = movement.normalized * speed * Time.deltaTime;

                // ���� ��ü�� Transform�� ����Ͽ� �̵�
                playerTransform.Translate(moveDirection);
            }
        }
        #endregion
    }
}