using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWeaponController : MonoBehaviour
{
    private PLayer pLayer;
    private Animator animator;
    [SerializeField] private GameObject Bullet;
    [SerializeField] private Transform gunPoint;
    [SerializeField] private float bulletSpeed;

    [SerializeField] private Transform weaponHolder;
    [SerializeField] private Transform aim;
    private bool isReloading_ChangeCompete;
    private void OnEnable()
    {
        WeaponChangerScript.IsReloading_ChangeCompeted += IsReloading_ChangeCompeted;
    }

    private void IsReloading_ChangeCompeted(bool obj)
    {
        isReloading_ChangeCompete = obj;
    }

    private void OnDisable()
    {
        WeaponChangerScript.IsReloading_ChangeCompeted -= IsReloading_ChangeCompeted;
    }
    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        pLayer = GetComponent<PLayer>();
        pLayer.playerControls.Character.Fire.performed += ctx => shoot("Fire");
      
    }

    private void shoot(string anim)
    {
      
        GameObject newBullets = Instantiate(Bullet,gunPoint.position,Quaternion.LookRotation(gunPoint.forward));
        newBullets.GetComponent<Rigidbody>().velocity = BulletDirection()*bulletSpeed;
        Destroy(newBullets,10.0f);
        animator.SetTrigger(anim);
    }
    public Vector3 BulletDirection()
    {
        Vector3 dir =(aim.position - gunPoint.position).normalized;
        if(pLayer.playerAim.getPreciseAim() == false) {
        dir.y = 0;
        }
        if (isReloading_ChangeCompete == true)
        {
            weaponHolder.LookAt(aim);
            gunPoint.LookAt(aim);
        }
     
        return dir;
    }
    public Transform GunPoint () => gunPoint;
}
