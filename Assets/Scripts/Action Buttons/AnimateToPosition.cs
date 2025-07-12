using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class AnimateToPosition : MonoBehaviour
{
    [SerializeField] private float duration = 1f;
    [SerializeField] private AnimationCurve animationCurve;

    private float targetHeight = 0f; // The target height to animate to
    private float startHeight = 0f; // The starting height of the RectTransform
    private float elapsedTime = 0f;

    private bool started = false;

    private RectTransform rectTransform;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (started && elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;
            float curveValue = animationCurve.Evaluate(t);
            rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, Mathf.Lerp(startHeight, targetHeight, curveValue));
        }
    }

    public void StartAnimation(float _targetHeight)
    {
        started = true;
        elapsedTime = 0f; // Reset elapsed time
        startHeight = rectTransform.anchoredPosition.y; // Store the starting height
        targetHeight = _targetHeight; // Set the new target height
    }
}
