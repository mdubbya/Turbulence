using UnityEngine;

namespace AI
{
    public interface ITargetModifier
    {
        AITargetInfo GetNewTargetInfo(AITargetInfo targetInfo);
        int priority { get; set; }
    }

    
}
