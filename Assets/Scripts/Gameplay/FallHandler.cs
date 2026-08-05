using DG.Tweening;
using UnityEngine;
using Zenject;

public class FallHandler : MonoBehaviour
{
    [Inject] private GameSettingsSO _gameSettings;

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
