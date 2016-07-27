namespace AI
{
    public abstract class Objective
    {
        public bool objectiveComplete;
        public int priority;

        public abstract AITargetInfo GetTargetInfo(AITargetInfo targetInfo);
    }
}
