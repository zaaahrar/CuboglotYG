using UnityEngine;

public class MobileInputProvider : IInputProvider
{
    [SerializeField] private Joystick _joystick;

    public MobileInputProvider(Joystick joystick) => _joystick = joystick;

    public Vector3 GetMovementDirection()
    {
        if(_joystick == null)
            return Vector3.zero;

        return new Vector3(_joystick.Horizontal, 0, _joystick.Vertical);
    }
}
