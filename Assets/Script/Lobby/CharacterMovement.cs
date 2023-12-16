using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // �̵� �ӵ� ���� ����

    void Update()
    {
        MoveCharacter(); // ĳ���� �̵� �Լ� ȣ��
    }

    void MoveCharacter()
    {
        float horizontal = Input.GetAxis("Horizontal"); // �¿� �̵� �Է� (A�� D Ű Ȥ�� ȭ��ǥ �¿�)
        float vertical = Input.GetAxis("Vertical"); // �յ� �̵� �Է� (W�� S Ű Ȥ�� ȭ��ǥ ���Ʒ�)

        Vector3 movement = new Vector3(horizontal, 0f, vertical); // �̵� ���� ����
        movement.Normalize(); // ���͸� ����ȭ�Ͽ� �밢�� �̵� �� ������ �ӵ��� �̵��ϵ��� ��

        // ĳ���͸� �̵� �������� �̵�
        transform.Translate(movement * moveSpeed * Time.deltaTime, Space.World);
    }
}
