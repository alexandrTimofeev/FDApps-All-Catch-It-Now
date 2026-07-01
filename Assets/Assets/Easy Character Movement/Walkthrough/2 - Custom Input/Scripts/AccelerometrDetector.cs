using UnityEngine;

public static class AccelerometrDetector
{
    /// <summary>
    /// Возвращает нормализованное направление, полученное с акселерометра.
    /// В упрощённой реализации это вектор Input.acceleration в локальных координатах устройства.
    /// </summary>
    public static Vector3 GetCurrentDirection(float minimal)
    {
        Vector3 accel = Input.acceleration;
        // Защита от нулевого вектора
        if (accel.magnitude < minimal)
            return Vector3.zero;
        return accel.normalized;
    }

    /// <summary>
    /// Возвращает направление из акселерометра, преобразованное в локальное пространство переданного трансформа.
    /// Внимание: данная реализация предполагает, что вектор, полученный из GetCurrentDirection(),
    /// задан в мировых координатах. На практике это не так (акселерометр возвращает локальные координаты
    /// устройства), поэтому для корректной работы может потребоваться дополнительное преобразование
    /// с учётом ориентации устройства (например, через гироскоп).
    /// </summary>
    public static Vector3 GetLocalDirection(Transform target, float minimal)
    {
        if (target == null)
            return Vector3.zero;

        Vector3 worldDir = GetCurrentDirection(minimal);
        Vector3 result = target.InverseTransformDirection(worldDir);
        if (result.magnitude <= minimal)
            return Vector3.zero;
        else
            return result;
    }
}