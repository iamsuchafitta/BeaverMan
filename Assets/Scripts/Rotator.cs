using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float bounceHeight = 0.1f; // ������ �������������
    public float bounceSpeed = 8f; // �������� �������������

    private Vector3 initialPosition;

    void Start()
    {
        // ��������� ��������� ������� ��� �������� ��� �������������
        initialPosition = transform.position;
    }

    void Update()
    {
        // ������������� �����-����
        float bounce = Mathf.Sin(Time.time * bounceSpeed) * bounceHeight;
        transform.position = initialPosition + new Vector3(0f, bounce, 0f);
    }
}
