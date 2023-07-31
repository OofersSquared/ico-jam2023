using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Lever : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private GameObject platform;
    [SerializeField] private List<Sprite> upSpritesList;
    [SerializeField] private List<Sprite> downSpritesList;
    [SerializeField] private float speed = 9;

    private Image _image;
    private float _timer;
    private int _index;
    private bool _isUp = true;

    private void Awake()
    {
        _image = GetComponent<Image>();
        platform.SetActive(!_isUp);
    }

    private void Update()
    {
        _timer -= Time.deltaTime;
        if (_timer <= 0)
        {
            var spritesAnimationList = _isUp ? upSpritesList : downSpritesList;
            _image.sprite = spritesAnimationList[_index];
            _index++;
            if (_index >= spritesAnimationList.Count)
                _index = 0;
            _timer = 1f / Random.Range(speed - 1.5f, speed + 1.5f);
        }
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            _isUp = !_isUp;
            platform.SetActive(!_isUp);
        }
    }
}