using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLayer : MonoBehaviour
{
    public Dave playerControls ;
    public PlayerMovement playerMovement ;
    public PLayerAim playerAim;
    public PlayerWeaponController playerWeaponController ;
    private void Awake()
    {
        playerControls = new Dave();
    }
    private void OnEnable() => playerControls.Enable();
    private void OnDisable() => playerControls.Disable();
}
