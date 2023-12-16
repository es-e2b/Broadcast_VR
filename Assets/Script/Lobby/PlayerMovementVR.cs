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
        // �ε�� ����̽��� Oculus Quest�� ��� isVRMode�� true�� �����մϴ�.
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
            // VR ���: �޼� PrimaryThumbstick �Է��� �̿��Ͽ� �̵��մϴ�.
            OVRInput.Update(); // OVRInput ���¸� ������Ʈ�մϴ�.
            Vector2 primaryThumbstick = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
            moveX = primaryThumbstick.x;
            moveZ = primaryThumbstick.y;
        }
        else
        {
            // �Ϲ� ���: WASD Ű �Է��� �̿��Ͽ� �̵��մϴ�.
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

        // �̵� ������ ���� ���� ��ü�� �̵���ŵ�ϴ�.
        Vector3 moveDirection = new Vector3(moveX, 0, moveZ).normalized;
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);
    }
}