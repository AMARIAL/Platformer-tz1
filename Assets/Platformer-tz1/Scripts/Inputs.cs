using UnityEngine;

[RequireComponent(typeof(Player))]

public class Inputs : MonoBehaviour
{
    public bool isActive;
    public float horizontalDirection;
    public bool isJumpPressed;
    public bool isFirePressed;

    private void Awake()
    {
        isActive = true;
    }
    private void Update()
    {
        if(!isActive) return;
        
        horizontalDirection = Input.GetAxis(GlobalParams.HORIZONTAL_AXIS);
        isJumpPressed = Input.GetButtonDown(GlobalParams.JUMP);
        isFirePressed = Input.GetButtonDown(GlobalParams.FIRE);
    }
}