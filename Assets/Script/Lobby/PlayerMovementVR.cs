using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using Oculus;

public class PlayerMovementVR : MonoBehaviour
{
    public float moveSpeed = 3.0f;
    public bool isVRMode = false;

    void Start()
    {
        XRDevice.deviceLoaded += OnDeviceLoaded;
    }

    void OnDeviceLoaded(string loadedDevice)
    {
        // 로드된 디바이스가 Oculus Quest인 경우 isVRMode를 true로 설정합니다.
        if (loadedDevice.Contains("Oculus Quest"))
        {
            isVRMode = true;
        }
    }

    void Update()
    {
        float moveX = 0f;
        float moveZ = 0f;

        if (isVRMode)
        {
            // VR 모드: 왼손 PrimaryThumbstick 입력을 이용하여 이동합니다.
            OVRInput.Update(); // OVRInput 상태를 업데이트합니다.
            Vector2 primaryThumbstick = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
            moveX = primaryThumbstick.x;
            moveZ = primaryThumbstick.y;
        }
        else
        {
            // 일반 모드: WASD 키 입력을 이용하여 이동합니다.
            if (Input.GetKey(KeyCode.W))
            {
                moveZ = +1f;
            }
            if (Input.GetKey(KeyCode.S))
            {
                moveZ = -1f;
            }
            if (Input.GetKey(KeyCode.A))
            {
                moveX = -1f;
            }
            if (Input.GetKey(KeyCode.D))
            {
                moveX = +1f;
            }
        }

        // 이동 방향을 토대로 게임 개체를 이동시킵니다.
        Vector3 moveDirection = new Vector3(moveX, 0, moveZ).normalized;
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);
    }
}