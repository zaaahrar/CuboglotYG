using Unity.VisualScripting;
using UnityEngine;
using YG;
using Zenject;

[RequireComponent (typeof(Rigidbody))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private UpgradeSO _speedUpgrade;
    [SerializeField] private Joystick _joystick;
    [Inject] private GameSettingsSO _gameSettings;

    private IInputProvider _inputProvider;
    private float _currentSpeed;

    private void Start()
    {
        _currentSpeed = _gameSettings.Speed
        + YandexGame.savesData.LevelSpeedUpgrade * _speedUpgrade.StatValue;

        if (YandexGame.EnvironmentData.isMobile && _joystick != null)
        {
            _inputProvider = new MobileInputProvider(_joystick);
            _joystick.gameObject.SetActive(true);
        }
        else
        {
            _inputProvider = new DesktopInputProvider();
            _joystick.gameObject.SetActive(false);
        }
            

    }

    private void FixedUpdate() => Move();

    private void Move()
    {
        Vector3 direction = _inputProvider.GetMovementDirection();
        _rigidbody.velocity = direction * _currentSpeed;
    }
}
