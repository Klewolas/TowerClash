    using UnityEngine;

    [CreateAssetMenu (fileName = "SO_Turret",menuName = "ScriptableObjects/Turret", order = 1)]
    public class Turret : ScriptableObject
    {
        public string Name;
        public int Cost;
        public GameObject TurretPrefab;
    }
