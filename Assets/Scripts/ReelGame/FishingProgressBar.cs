using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class FishingProgressBar : MonoBehaviour
{
    public Action OnProgressComplete;
    public Action OnProgressLose;
    [SerializeField] private GameObject _reelBackground;
    [SerializeField] private Gradient _progressGradient;
    private float _leftOffset;
    public float Progress = 0.0f;
    private void Awake()
    {
        _leftOffset = (_reelBackground.transform.localScale.x / 2 + transform.localScale.x / 2) * -1;
    }
    private void UpdatePosition()
    {
        transform.localPosition = new Vector3(
            _leftOffset,
            _reelBackground.transform.localScale.y * Progress / 2 - _reelBackground.transform.localScale.y / 2,
            0
        );
        transform.localScale = new Vector3(
            transform.localScale.x,
            Progress * _reelBackground.transform.localScale.y,
            transform.localScale.z
        );
    }

    private void UpdateColor()
    {
        GetComponent<SpriteRenderer>().color = _progressGradient.Evaluate(Progress);
    }
    public void AddProgress(float progress)
    {
        SetProgress(Progress + progress);
    }
    public void SetProgress(float progress)
    {
        Progress = Mathf.Clamp(progress, 0.0f, 1.0f);
        UpdatePosition();
        UpdateColor();
        if(Progress >= 1.0f)
        {
            OnProgressComplete?.Invoke();
        }
        else if(Progress <= 0.0f)
        {
            OnProgressLose?.Invoke();
        }
    }
}
