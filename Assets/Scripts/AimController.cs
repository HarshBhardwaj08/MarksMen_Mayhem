using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimController : IAimController
{
    private readonly Transform _playerTransform;
  
    private readonly LayerMask _aimLayerMask;
    private Vector2 _aimput;
    private RaycastHit lastraycastHit;
    public AimController(Transform playerTransform, LayerMask aimLayerMask)
    {
        _playerTransform = playerTransform;

        _aimLayerMask = aimLayerMask;
    }

    public void TurnPlayer(Vector2 aimInput, float turnSpeed)
    {
        _aimput = aimInput;
        Vector3 MousePos = GetMouseRayInfo().point;
        Vector3 lookDirection = MousePos - _playerTransform.position;
        lookDirection.y = 0;
        lookDirection.Normalize();

        Quaternion desriedRotation = Quaternion.LookRotation(lookDirection);
        _playerTransform.rotation = Quaternion.Slerp(_playerTransform.rotation, desriedRotation, turnSpeed * Time.deltaTime);
    }
    public RaycastHit GetMouseRayInfo()
    {
        Ray ray = Camera.main.ScreenPointToRay(_aimput);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, _aimLayerMask))
        { 
            lastraycastHit = hitInfo;
            return hitInfo;
        }
        return lastraycastHit ;
    }
    public Vector3 DesieredCamPosition(PLayer player, float minimumCameraDistance,float maximumCameraDistance)
    {
        float actualMaxCameraDistance = player.playerMovement.movementInput.y < -0.5f ? minimumCameraDistance : maximumCameraDistance;
        Vector3 desiredAimPos = GetMouseRayInfo().point;
        Vector3 aimDir = (desiredAimPos - _playerTransform.position).normalized;

        float distanceToDesiredPostion = Vector3.Distance(_playerTransform.position, desiredAimPos);
        float clampDistance = Mathf.Clamp(distanceToDesiredPostion, minimumCameraDistance, actualMaxCameraDistance);

        desiredAimPos = _playerTransform.position + aimDir * clampDistance;
        desiredAimPos.y = _playerTransform.position.y + 1;
        return desiredAimPos;
    }
}
