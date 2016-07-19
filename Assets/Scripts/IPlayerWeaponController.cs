using UnityEngine;


public interface IWeaponController
{
    Transform spawnLocation { get; set; }

    void Fire();
}
