
using UnityEngine;

public interface IMovementController
{
    void Move(Vector3 direction, float speed);
}

public interface IAimController
{
    void TurnPlayer(Vector2 aimInput,float turnSpeed);
}

public interface IPlayerAnimator
{
    void UpdateMovementAnimation(Vector3 movementDirection);
    void TriggerFire();
}
public interface IplayerChangeWeapon
{

}