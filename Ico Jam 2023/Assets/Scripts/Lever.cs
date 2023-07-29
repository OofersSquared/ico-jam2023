using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Lever : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private GameObject platform;
    [SerializeField] private Sprite upSprite;
    [SerializeField] private Sprite downSprite;

    private Image _image;

    private bool _isUp = true;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _image.sprite = upSprite;
        platform.SetActive(!_isUp);
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            _isUp = !_isUp;
            _image.sprite = _isUp ? upSprite : downSprite;
            platform.SetActive(!_isUp);
        }
    }
}
