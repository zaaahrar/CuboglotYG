using UnityEngine;

public class DesktopInputProvider : IInputProvider
{
    private const string HORIZONTAL_AXIS = "Horizontal";
    private const string VERTICAL_AXIS = "Vertical";

    public Vector3 GetMovementDirection()
    {
        float horizontalMove = Input.GetAxisRaw(HORIZONTAL_AXIS);
        float verticalMove = Input.GetAxisRaw(VERTICAL_AXIS);
        return new Vector3(horizontalMove, 0, verticalMove).normalized;
    }
}
