using DG.Tweening;
using UnityEngine;

public class FallHandler : MonoBehaviour
{
    [SerializeField] private GameSettingsSO _gameSettings;

    public void FallToPoint(Transform target, Transform fallPoint)
    {
        Vector3 lookDirection = fallPoint.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(lookDirection);

        target.DORotateQuaternion(rotation, _gameSettings.LookDuration)
            .SetEase(Ease.InOutQuad).SetLink(target.gameObject, LinkBehaviour.KillOnDisable);
        target.DOMove(fallPoint.position, _gameSettings.MoveDuration)
            .SetEase(Ease.InOutQuad).SetLink(target.gameObject, LinkBehaviour.KillOnDisable);
    }
}
