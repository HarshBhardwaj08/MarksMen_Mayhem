using System;
using UnityEngine;

public class PLayerAim : MonoBehaviour
{
    [SerializeField] private LayerMask aimLayerMask;
    [SerializeField] private float turnSpeed;
    private Vector2 aimInput;
    private PLayer player;
    private AimController aimController;
    [Header("Camera Control Info")]
    [SerializeField] private GameObject cameraTarget;
    [SerializeField] private float camerafollowSenitivity;
    [SerializeField] private float minimumCameraDistance;
    [SerializeField] private float maximumCameraDistance;

    [Space]

    [Header("Aim Control")]
    [SerializeField] private GameObject aimTarget;
    public bool isPreciseAim;

    [Header("Laser")]
    [SerializeField] private LineRenderer aimlaser;
    [SerializeField] private float tipLength;
    void Start()
    {
        player = GetComponent<PLayer>();
        aimController = new AimController(this.transform, aimLayerMask);
        var playerControls = PLayer.playerControls.Character;
        playerControls.Aim.performed += ctx => aimInput = ctx.ReadValue<Vector2>();
        playerControls.Aim.canceled += ctx => aimInput = Vector2.zero;
    }
    private void Update()
    {
        UpdateAimLaser();
        aimTarget.transform.position = aimController.GetMouseRayInfo().point;
        if(isPreciseAim == false)
        {
          aimTarget.transform.position = new Vector3(aimTarget.transform.position.x, transform.position.y + 1, aimTarget.transform.position.z);
        }
        if(Input.GetKey(KeyCode.Space))
        {
         isPreciseAim = true;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            isPreciseAim = false;
        }
        aimController.TurnPlayer(aimInput, turnSpeed);
        cameraTarget.transform.position = Vector3.Lerp(cameraTarget.transform.position, aimController.DesieredCamPosition(player,minimumCameraDistance,maximumCameraDistance) 
            , camerafollowSenitivity * Time.deltaTime);
    }

    private void UpdateAimLaser()
    {
        float laserTipLength = tipLength;
        Transform gunPoint = player.playerWeaponController?.GunPoint();
        Vector3 laserDirection = player.playerWeaponController.BulletDirection();
        float gunDistance = 4f;
        if (laserDirection != null)
        {
            Vector3 endPoint = gunPoint.position + laserDirection * gunDistance;
            if (Physics.Raycast(gunPoint.position, laserDirection, out RaycastHit hit, gunDistance))
            {
                endPoint = hit.point;
                laserTipLength = 0;
            }
            aimlaser.SetPosition(0, gunPoint.position);
            aimlaser.SetPosition(1, endPoint);
            aimlaser.SetPosition(2, endPoint + laserDirection * laserTipLength);
        }
       
    }

    private Transform hitTarget()
    { 
        Transform target = null;
        if (aimController.GetMouseRayInfo().transform.GetComponent<Target>())
        {
            target = aimController.GetMouseRayInfo().transform;
        }
        return target;
    }
    public void setPreciseAim(bool aim)
    {
        isPreciseAim = aim;
    }
    public bool getPreciseAim()
    {
        return isPreciseAim;
    }
}
