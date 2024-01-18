using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level4Teleport : MonoBehaviour
{
    // ќпределите целевые координаты здесь или в редакторе Unity
    public Vector2 targetPosition;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("FirstPlayer") || collision.CompareTag("SecondPlayer"))
        {
            // ѕеремещаем игрока на заданные координаты
            collision.transform.position = targetPosition;
        }
    }
}
