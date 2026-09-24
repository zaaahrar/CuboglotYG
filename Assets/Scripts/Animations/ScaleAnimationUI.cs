using UnityEngine;
using DG.Tweening;

public class ScaleAnimationUI : MonoBehaviour
{
    [SerializeField] private RectTransform _rectTransform;

    [SerializeField] private float _duration;
    [SerializeField] private Vector2 _startScale;
    [SerializeField] private Vector2 _finsihScale;

    public void OnEnable()
    {
        _rectTransform.localScale = _startScale;
        _rectTransform.DOScale(_finsihScale, _duration).SetLoops(-1, LoopType.Yoyo).SetLink(_rectTransform.gameObject, LinkBehaviour.KillOnDestroy);
    }
    
}
