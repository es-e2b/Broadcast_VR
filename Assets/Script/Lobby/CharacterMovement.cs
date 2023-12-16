using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // 이동 속도 조절 변수

    void Update()
    {
        MoveCharacter(); // 캐릭터 이동 함수 호출
    }

    void MoveCharacter()
    {
        float horizontal = Input.GetAxis("Horizontal"); // 좌우 이동 입력 (A와 D 키 혹은 화살표 좌우)
        float vertical = Input.GetAxis("Vertical"); // 앞뒤 이동 입력 (W와 S 키 혹은 화살표 위아래)

        Vector3 movement = new Vector3(horizontal, 0f, vertical); // 이동 벡터 생성
        movement.Normalize(); // 벡터를 정규화하여 대각선 이동 시 일정한 속도로 이동하도록 함

        // 캐릭터를 이동 방향으로 이동
        transform.Translate(movement * moveSpeed * Time.deltaTime, Space.World);
    }
}
