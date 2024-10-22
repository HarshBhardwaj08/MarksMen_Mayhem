using UnityEngine;

public class PlayerAnimator : IPlayerAnimator
{
    private readonly Animator _animator;

    public PlayerAnimator(Animator animator)
    {
        _animator = animator;
    }

    public void UpdateMovementAnimation(Vector3 movementDirection)
    {
        float xVelocity = Vector3.Dot(movementDirection.normalized, _animator.transform.right);
        float zVelocity = Vector3.Dot(movementDirection.normalized, _animator.transform.forward);

        _animator.SetFloat("xVelocity", xVelocity, 0.1f, Time.deltaTime);
        _animator.SetFloat("zVelocity", zVelocity, 0.1f, Time.deltaTime);
    }

    public void TriggerFire()
    {
        _animator.SetTrigger("Fire");
    }
}
