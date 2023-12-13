using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float mouseSensitivity = 100.0f;
    private float xRotation = 0.0f;

    void Start()
    {
        // 마우스 커서를 잠급니다.
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // 마우스 움직임을 얻습니다.
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;  // 수평 움직임:citation[23]
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;  // 수직 움직임:citation[23]

        // 위아래로의 카메라 회전을 제한합니다.
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);  // 회전을 -90도에서 90도 사이로 제한합니다 :citation[24].

        // 마우스 입력을 카메라의 회전에 적용합니다.
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);  // 수직 회전:citation[25]
        transform.Rotate(Vector3.up * mouseX);  // 수평 회전:citation[25]
    }
}