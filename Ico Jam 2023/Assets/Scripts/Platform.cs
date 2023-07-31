using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

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
    [SerializeField] private List<Sprite> activeSpritesList;
    [SerializeField] private List<Sprite> deActiveSpritesList;
    [SerializeField] private Vector2 target;
    [SerializeField] private float moveDuration;
    [SerializeField] private Ease moveEase;
    [SerializeField] private float speed = 9;
    
    private Collider2D _collision;
    private SpriteRenderer _spriteRenderer;
    private bool _isOnX = true;
    private bool _isOn = true;
    private float _timer;
    private int _index;

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

    private void Update()
    {
        _timer -= Time.deltaTime;
        if (_timer <= 0)
        {
            var spritesAnimationList = _isOn ? activeSpritesList : deActiveSpritesList;
            _spriteRenderer.sprite = spritesAnimationList[_index];
            _index++;
            if (_index >= spritesAnimationList.Count)
                _index = 0;
            _timer = 1f / Random.Range(speed - 2f, speed);
        }
    }
    
    private void OnTimerEnd()
    {
        switch (platformType)
        {
            case PlatformType.Intangibility:
                _collision.enabled = !_collision.enabled;
                if(platformType != PlatformType.Intangibility)
                    _spriteRenderer.sprite = _spriteRenderer.sprite == activeSprite ? deActiveSprite : activeSprite;
                else
                    _isOn = !_isOn;
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
