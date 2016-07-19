using AI;

namespace AI
{
    public interface IShipMovementController
    {
        void ApplyMovementControl(AITargetInfo targetInfo);
    }
}
