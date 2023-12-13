using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float mouseSensitivity = 100.0f;
    private float xRotation = 0.0f;

    void Start()
    {
        // ���콺 Ŀ���� ��޴ϴ�.
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // ���콺 �������� ����ϴ�.
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;  // ���� ������:citation[23]
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;  // ���� ������:citation[23]

        // ���Ʒ����� ī�޶� ȸ���� �����մϴ�.
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);  // ȸ���� -90������ 90�� ���̷� �����մϴ� :citation[24].

        // ���콺 �Է��� ī�޶��� ȸ���� �����մϴ�.
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);  // ���� ȸ��:citation[25]
        transform.Rotate(Vector3.up * mouseX);  // ���� ȸ��:citation[25]
    }
}