using UnityEngine;


public interface IWeaponController
{
    Transform spawnLocation { get; set; }

    //phrased this way because this applies to any kind of weapon "output" projectile,beam,ray etc.
    float weaponOutputSpeed { get; }

    void Fire();
}
