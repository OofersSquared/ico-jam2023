using UnityEngine;

public class Lever : MonoBehaviour
{
    [SerializeField] private GameObject platform;
    [SerializeField] private Sprite upSprite;
    [SerializeField] private Sprite downSprite;

    private SpriteRenderer _spriteRenderer;
    private bool _isUp = true;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = upSprite;
        platform.SetActive(!_isUp);
    }

    private void OnMouseDown()
    {
        _isUp = !_isUp;
        _spriteRenderer.sprite = _isUp ? upSprite : downSprite;
        platform.SetActive(!_isUp);
    }
}
