using UnityEngine;

public class MovementController : IMovementController
{
    private readonly CharacterController _characterController;
    private float _verticalVelocity;

    public MovementController(CharacterController characterController)
    {
        _characterController = characterController;
    }

    public void Move(Vector3 direction, float speed)
    {
        ApplyGravity(ref direction);
        if(direction.magnitude > 0) { _characterController.Move(direction * speed * Time.deltaTime); }
      
    }

    private void ApplyGravity(ref Vector3 direction)
    {
        if (!_characterController.isGrounded)
        {
            _verticalVelocity -= 9.8f * Time.deltaTime;
            direction.y = _verticalVelocity;
        }
        else
        {
            _verticalVelocity = -0.05f;
        }
    }
}