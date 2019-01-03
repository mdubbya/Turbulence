
namespace Component
{
    public abstract class Component
    {
        private float _powerUsage;
        public float powerUsage { get; }
        private float _mass;
        public float mass { get; }
        //where prefab contains an (optional) model, and any other scripts/unity-related objects necessary
        private string _prefabResourcePath;
        string prefabResourcePath { get; }
    }   
}       
