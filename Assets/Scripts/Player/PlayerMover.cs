using UnityEngine;
using YG;
using Zenject;

[RequireComponent (typeof(Rigidbody))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private UpgradeSO _speedUpgrade;
    [Inject] private GameSettingsSO _gameSettings;

    private IInputProvider _inputProvider;
    private float _currentSpeed;
    private Joystick _joystick;

    public void Init(Joystick joystick)
    {
        _currentSpeed = _gameSettings.Speed
+ YandexGame.savesData.LevelSpeedUpgrade * _speedUpgrade.StatValue;
        _joystick = joystick;

        if (_joystick == null)
            return;

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
