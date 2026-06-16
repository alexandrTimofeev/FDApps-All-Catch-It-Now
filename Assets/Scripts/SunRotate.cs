using UnityEngine;
using DG.Tweening;

public class SunRotate : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = 10f; // градусов в секунду
    private Tween _rotateTween;

    void Start()
    {
        // Считаем время одного полного оборота
        float duration = 360f / _rotationSpeed;

        _rotateTween = transform
            .DORotate(new Vector3(0, 0, 360), duration, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear) // равномерное вращение
            .SetLoops(-1, LoopType.Restart); // бесконечно
    }

    void OnDestroy()
    {
        _rotateTween?.Kill();
    }

    void OnDisable()
    {
        _rotateTween?.Kill();
    }
}