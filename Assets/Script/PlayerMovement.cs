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
                // 수평 및 수직 입력을 받아 이동 방향을 계산
                float horizontalInput = Input.GetAxis("Horizontal");
                float verticalInput = Input.GetAxis("Vertical");
                float upInput = Input.GetAxis("Jump");
                float downInput = -Input.GetAxis("Sprint");
                Vector3 updown = new Vector3(0f, upInput+downInput, 0f);
                Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput)+updown;

                // 이동 방향에 속도를 곱하여 이동 벡터 생성
                Vector3 moveDirection = movement.normalized * speed * Time.deltaTime;

                // 현재 객체의 Transform을 사용하여 이동
                playerTransform.Translate(moveDirection);
            }
        }
        #endregion
    }
}