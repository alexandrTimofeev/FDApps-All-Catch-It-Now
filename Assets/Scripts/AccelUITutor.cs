using DG.Tweening;
using UnityEngine;

public class AccelUITutor : MonoBehaviour
{
    [SerializeField] private Vector3 pointA;
    [SerializeField] private Vector3 pointB;

    void Start()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DORotate(pointA, 1f));
        sequence.Append(transform.DORotate(pointB, 1f));
        sequence.Append(transform.DORotate(pointA, 1f));
        sequence.Append(transform.DORotate(pointB, 1f));
        sequence.SetLoops(-1, LoopType.Yoyo);
        sequence.Play();
    }

    float time;
    private void Update()
    {
        time += Time.deltaTime;

        if (time > 10f)
            if (AccelerometrDetector.GetCurrentDirection(0.1f).magnitude >= 0.8f)
            {
                gameObject.SetActive(false);
            }
    }
}
