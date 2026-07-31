using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class PlayerMover : MonoBehaviour
{
    private const string HORIZONTAL_AXIS = "Horizontal";
    private const string VERTICAL_AXIS = "Vertical";

    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private GameSettingsSO _gameSettings;

    private void FixedUpdate() => Move();

    private void Move()
    {
        float horizontalMove = Input.GetAxisRaw(HORIZONTAL_AXIS);
        float verticalMove = Input.GetAxisRaw(VERTICAL_AXIS);
        Vector3 direction = new Vector3 (horizontalMove, 0, verticalMove).normalized;

        _rigidbody.velocity = direction * _gameSettings.Speed;
    }
}
