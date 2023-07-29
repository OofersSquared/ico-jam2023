using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileAnimation : MonoBehaviour
{
    [SerializeField] private List<Sprite> spritesAnimationList;
    [SerializeField] private float speed;

    private Image _image;
    private float _timer;
    private int _index;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    private void Update()
    {
        _timer -= Time.deltaTime;
        if (_timer <= 0)
        {
            _image.sprite = spritesAnimationList[_index];
            _index++;
            if (_index >= spritesAnimationList.Count)
                _index = 0;
            _timer = 1f / speed;
        }
    }
}
