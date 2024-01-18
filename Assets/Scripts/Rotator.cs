using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float bounceHeight = 0.1f; // Высота подпрыгивания
    public float bounceSpeed = 8f; // Скорость подпрыгивания

    private Vector3 initialPosition;

    void Start()
    {
        // Сохраняем начальную позицию для возврата при подпрыгивании
        initialPosition = transform.position;
    }

    void Update()
    {
        // Подпрыгивание вверх-вниз
        float bounce = Mathf.Sin(Time.time * bounceSpeed) * bounceHeight;
        transform.position = initialPosition + new Vector3(0f, bounce, 0f);
    }
}
