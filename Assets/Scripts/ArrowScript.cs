using UnityEngine;

public class ArrowScript : MonoBehaviour {
    public Transform targetPoint; // Задайте целевую точку (куда бежать) через инспектор

    void Update() {
        if (this.targetPoint == null) return;

        // Определение направления к целевой точке
        var directionToTarget = this.targetPoint.position - this.transform.position;
        directionToTarget.Normalize();

        // Рассчет угла в радианах и преобразование его в угол в градусах
        var angle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;

        // Применение поворота к стрелке
        this.transform.rotation = Quaternion.Euler(0f, 0f, angle + 90f); // Исправление угла поворота
    }
}