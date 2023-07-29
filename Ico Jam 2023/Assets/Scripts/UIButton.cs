using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UIButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private AudioClip hoverSound;
    [SerializeField] private AudioClip clickSound;
    [SerializeField] private float scaleMultiplier;
    [SerializeField] private float scaleDuration;
    [SerializeField] private Ease scaleEase;

    public UnityEvent onClick;
    public UnityEvent<bool> onHover;
    
    private float _defaultScale;
    private AudioSource _source;

    private void Awake()
    {
        _source = GetComponentInChildren<AudioSource>();
        _defaultScale = transform.localScale.x;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.DOScale(_defaultScale * scaleMultiplier * Vector3.one, scaleDuration).SetEase(scaleEase);
        _source.clip = hoverSound;
        _source.Play();
        onHover?.Invoke(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOScale(_defaultScale * Vector3.one, scaleDuration).SetEase(scaleEase);
        _source.clip = hoverSound;
        _source.Play();
        onHover?.Invoke(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        transform.DOScale(_defaultScale * Vector3.one, scaleDuration).SetEase(scaleEase);
        _source.clip = clickSound;
        _source.Play();
        onClick?.Invoke();
    }
}
