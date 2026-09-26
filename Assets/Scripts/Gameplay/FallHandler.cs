using DG.Tweening;
using UnityEngine;
using Zenject;

public class FallHandler : MonoBehaviour
{
    [Inject] private GameSettingsSO _gameSettings;

    [Header("Настройки падения")]
    [SerializeField] private Ease _fallEase = Ease.InQuad;
    [SerializeField] private float _tiltAngle = 50f;

    public void FallToPoint(Transform target, GameObject fallPoint)
    {
        Vector3 startPos = target.position;

        Vector3 lookDirection = fallPoint.transform.position - startPos;
        lookDirection.y = 0f;

        if (lookDirection.sqrMagnitude > 0.001f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
            Quaternion limitedRotation = Quaternion.Slerp(
                target.rotation,
                targetRotation,
                _tiltAngle / 180f
            );

            target.DORotateQuaternion(limitedRotation, _gameSettings.LookDuration)
                .SetEase(Ease.OutQuad)
                .SetLink(target.gameObject, LinkBehaviour.KillOnDisable);
        }

        float fallProgress = 0f;
        DOTween.To(
                () => fallProgress,
                x =>
                {
                    fallProgress = x;
                    target.position = Vector3.Lerp(startPos, fallPoint.transform.position, x);
                },
                1f,
                _gameSettings.MoveDuration
            )
            .SetEase(_fallEase)
            .SetLink(target.gameObject, LinkBehaviour.KillOnDisable);
    }
}
