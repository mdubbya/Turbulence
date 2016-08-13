using UnityEngine;
using AI.Process;

namespace AI
{
    public class AIGeneratorTest : MonoBehaviour
    {
        public void Start()
        {
            AttackArea attackArea = gameObject.AddComponent<AttackArea>();
            attackArea.basePriority = 1;
        }
    }
}