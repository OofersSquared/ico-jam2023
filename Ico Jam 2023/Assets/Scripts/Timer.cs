using DG.Tweening;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public static Timer instance;
    
    public float GetValue => _timer;
    
    [SerializeField] private Ease easingType;
    [SerializeField] private float animationDuration;

    private TMP_Text _text;
    private float _timer = 3f;
    private bool _isAnimating;

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
        instance = this;
    }

    private void Update()
    {
        _timer -= Time.deltaTime;
        if (_timer <= -1f)
            _timer = 3f;
        if(Mathf.CeilToInt(_timer).ToString() != _text.text && !_isAnimating)
            Next();
    }

    private void Next()
    {
        _isAnimating = true;
        transform.DORotate(new Vector3(0f, 0f, -180f), animationDuration).SetEase(easingType);
        _text.DOFade(0f, animationDuration).OnComplete(() =>
        {
            _text.text = Mathf.CeilToInt(_timer).ToString();
            transform.DORotate(new Vector3(0f, 0f, -360f), animationDuration).SetEase(easingType);
            _text.DOFade(1f, animationDuration).OnComplete(() => _isAnimating = false);
        });
    }
}