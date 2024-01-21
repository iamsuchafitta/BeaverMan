using UnityEngine;
using UnityEngine.UI;

public class Level2Arrow : MonoBehaviour
{
    public Transform targetPoint; // «адайте целевую точку (куда бежать) через инспектор

    void Update()
    {
        if (targetPoint != null)
        {
            // ќпределение направлени€ к целевой точке
            Vector3 directionToTarget = targetPoint.position - transform.position;
            directionToTarget.Normalize();

            // –ассчет угла в радианах и преобразование его в угол в градусах
            float angle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;

            // ѕрименение поворота к стрелке
            transform.rotation = Quaternion.Euler(0f, 0f, angle + 180f); // »справление угла поворота
        }
    }
}
