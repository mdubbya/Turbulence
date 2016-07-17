using UnityEngine;
using System.Collections;

public interface IWeaponController 
{
    Transform spawnLocation { get; set; }

    void Fire();
}
