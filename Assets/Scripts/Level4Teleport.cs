using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level4Teleport : MonoBehaviour
{
    // ���������� ������� ���������� ����� ��� � ��������� Unity
    public Vector2 targetPosition;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("FirstPlayer") || collision.CompareTag("SecondPlayer"))
        {
            // ���������� ������ �� �������� ����������
            collision.transform.position = targetPosition;
        }
    }
}
