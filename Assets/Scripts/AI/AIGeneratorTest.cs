using UnityEngine;
using AI.Objective;

namespace AI
{
    public class AIGeneratorTest : MonoBehaviour
    {
        public void Start()
        {
            AttackArea attackArea = gameObject.AddComponent<AttackArea>();
        }
    }
}