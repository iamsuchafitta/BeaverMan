using UnityEngine;
using UnityEngine.UI;

public class Level2Arrow : MonoBehaviour
{
    public Transform targetPoint; // ������� ������� ����� (���� ������) ����� ���������

    void Update()
    {
        if (targetPoint != null)
        {
            // ����������� ����������� � ������� �����
            Vector3 directionToTarget = targetPoint.position - transform.position;
            directionToTarget.Normalize();

            // ������� ���� � �������� � �������������� ��� � ���� � ��������
            float angle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;

            // ���������� �������� � �������
            transform.rotation = Quaternion.Euler(0f, 0f, angle + 180f); // ����������� ���� ��������
        }
    }
}
