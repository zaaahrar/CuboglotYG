using DG.Tweening;
using UnityEngine;

public class LoadingSpinner : MonoBehaviour
{
    [SerializeField] private RectTransform _loadingSpinner;
    [SerializeField] private float _secondsPerTurn = 1;

    private float _angle = -360;

    private void OnEnable()
    {
        _loadingSpinner.DOLocalRotate(new Vector3(0, 0, _angle), _secondsPerTurn, RotateMode.LocalAxisAdd)
        .SetLoops(-1, LoopType.Restart)
        .SetEase(Ease.Linear)
        .SetLink(_loadingSpinner.gameObject, LinkBehaviour.KillOnDisable);
    }
}
