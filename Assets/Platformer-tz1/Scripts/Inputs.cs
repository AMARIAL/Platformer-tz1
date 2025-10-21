using UnityEngine;

[RequireComponent(typeof(Player))]

public class Inputs : MonoBehaviour
{
    public float horizontalDirection;
    public bool isJumpPressed;
    public bool isFirePressed;
    
    private void Update()
    {
        horizontalDirection = Input.GetAxis(GlobalParams.HORIZONTAL_AXIS);
        isJumpPressed = Input.GetButtonDown(GlobalParams.JUMP);
        isFirePressed = Input.GetButtonDown(GlobalParams.FIRE);
    }
}