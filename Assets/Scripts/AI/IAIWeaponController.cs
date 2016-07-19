using UnityEngine;

namespace AI
{
    public interface IAIWeaponController
    {
        Transform spawnLocation { get; set; }

        void Fire(AITargetInfo targetInfo);
    }
}