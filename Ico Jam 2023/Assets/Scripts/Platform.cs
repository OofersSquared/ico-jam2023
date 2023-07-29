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
    private SpriteRenderer _spriteRenderer;
    private bool _isOnX = true;

    private void Start()
    {
        _collision = GetComponent<Collider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        if (platformType == PlatformType.Moving)
            transform.DOLocalMoveX(target.x, 0f);
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
                _spriteRenderer.sprite = _spriteRenderer.sprite == activeSprite ? deActiveSprite : activeSprite;
                break;
            case PlatformType.Moving:
                if (_isOnX)
                {
                    transform.DOLocalMoveX(target.y, moveDuration).SetEase(moveEase);
                    _isOnX = false;
                }
                else
                {
                    transform.DOLocalMoveX(target.x, moveDuration).SetEase(moveEase);
                    _isOnX = true;
                }

                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
