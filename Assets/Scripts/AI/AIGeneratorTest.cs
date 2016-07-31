using UnityEngine;
using AI.Objective;

namespace AI
{
    public class AIGeneratorTest : MonoBehaviour
    {
        public void Start()
        {
            DefendArea defendArea = gameObject.AddComponent<DefendArea>();
            defendArea.position = new Vector3(0, 0, 0);
        }
    }
}