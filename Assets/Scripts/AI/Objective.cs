namespace AI
{
    public abstract class Objective
    {
        public bool objectiveComplete;

        public abstract AITargetInfo GetTargetInfo(AITargetInfo targetInfo);
    }
}
