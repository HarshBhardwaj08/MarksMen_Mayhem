[System.Serializable]
public class Weapon
{
    public Weapontype currentWeapon;
    public int WeaponCurrentAmmo;
    public int WeaponMagzineSize;
    public int WeaponMaxMagzine;

    public bool canShoot()
    {
        return HasEnoughBullets();
    }

    public bool HasEnoughBullets()
    {
        if(WeaponCurrentAmmo <= 0)
        {
            return false;
        }
        return true;
    }
    public void ChangeWeapon(Weapontype weapontype)
    {
        currentWeapon = weapontype;
    }
}
public enum Weapontype
{
    Pistol,
    Revolver,
    Rifle,
    ShortGun,
    SniperRifle
}