using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class PlayerMover : MonoBehaviour
{
    private const string HorizontalAxis = "Horizontal";
    private const string VerticalAxis = "Vertical";

    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private GameSettingsSO _gameSettings;

    private void FixedUpdate() => Move();

    private void Move()
    {
        float horizontalMove = Input.GetAxisRaw(HorizontalAxis);
        float verticalMove = Input.GetAxisRaw(VerticalAxis);
        Vector3 direction = new Vector3 (horizontalMove, 0, verticalMove).normalized;

        _rigidbody.velocity = direction * _gameSettings.Speed;
    }
}
