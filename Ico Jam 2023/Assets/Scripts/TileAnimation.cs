using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileAnimation : MonoBehaviour
{
    [SerializeField] private List<Sprite> spritesAnimationList;
    [SerializeField] private float speed;
    [SerializeField] private bool usingSpriteRenderer;

    private Image _image;
    private SpriteRenderer _spriteRenderer;
    private float _timer;
    private int _index;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        _timer -= Time.deltaTime;
        if (_timer <= 0)
        {
            if(usingSpriteRenderer)
                _spriteRenderer.sprite = spritesAnimationList[_index];
            else
                _image.sprite = spritesAnimationList[_index];
            _index++;
            if (_index >= spritesAnimationList.Count)
                _index = 0;
            _timer = 1f / Random.Range(speed - 2f, speed);
        }
    }
}
