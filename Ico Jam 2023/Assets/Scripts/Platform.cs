using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public enum PlatformType
{
    Intangibility,
    Moving
}

public class Platform : MonoBehaviour
{
    [SerializeField] private PlatformType platformType;
    [SerializeField] private Sprite activeSprite;
    [SerializeField] private Sprite deActiveSprite;
    [SerializeField] private Vector2 target;
    [SerializeField] private float moveDuration;
    [SerializeField] private Ease moveEase;
    
    private Collider2D _collision;
    private Image _image;
    private RectTransform _rectTransform;

    private bool _isOnX = true;

    private void Start()
    {
        _collision = GetComponent<Collider2D>();
        _image = GetComponent<Image>();
        _rectTransform = GetComponent<RectTransform>();
        if (platformType == PlatformType.Moving)
            _rectTransform.DOAnchorPosX(target.x, 0f);
        Timer.instance.onTimerEnd += OnTimerEnd;
    }

    private void OnDestroy()
    {
        Timer.instance.onTimerEnd -= OnTimerEnd;
    }

    private void OnTimerEnd()
    {
        switch (platformType)
        {
            case PlatformType.Intangibility:
                _collision.enabled = !_collision.enabled;
                _image.sprite = _image.sprite == activeSprite ? deActiveSprite : activeSprite;
                break;
            case PlatformType.Moving:
                if (_isOnX)
                {
                    _rectTransform.DOAnchorPosX(target.y, moveDuration).SetEase(moveEase);
                    _isOnX = false;
                }
                else
                {
                    _rectTransform.DOAnchorPosX(target.x, moveDuration).SetEase(moveEase);
                    _isOnX = true;
                }

                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
