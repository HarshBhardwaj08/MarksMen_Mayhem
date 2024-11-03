using UnityEngine;

[System.Serializable]
public class Weapon
{
    public Weapontype currentWeaponType;
    public WeaponCategories currentWeaponCategory;
    public float weaponFireRate = 1f;
    public float lastShootTime;
    public int weaponCurrentAmmo;
    public int weaponMagzineSize;
    public int weaponMaxMagzine;
    public Transform gunPoint;
    public float fireSpread;
    public Vector3 spreadDirection(Vector3 position)
    {  

        return position;
    }
    public bool canShoot()
    {
        return HasEnoughBullets();
    }
    public bool isFireEnable()
    {
        if(Time.time > lastShootTime +1 / weaponFireRate)
        {
            lastShootTime = Time.time;
            return true;
        }
        return false;
    }
    public bool HasEnoughBullets()
    {
        if(weaponCurrentAmmo <= 0)
        {
            return false;
        }
        return true;
    }
}
public enum FireType
{
    Single,Burst,Automatic
}
public enum WeaponCategories
{
    lightWeapon,HeavyWeapon
}
public enum Weapontype
{
    Pistol,
    Revolver,
    Rifle,
    ShortGun,
    SniperRifle
}