using System;
using UnityEngine;

namespace SimpleSurvivors.Enemy
{
    [CreateAssetMenu(fileName = "Wave Template", menuName = "ScriptableObjects/Wave")]
    public class WaveSO : ScriptableObject
    {
        public WaveDetail[] waveDetails;
        public float spawnTimer;

        [Serializable]
        public class WaveDetail
        {
            public GameObject enemyPrefab;
            public int spawnCount;
            public float spawnFrequency;
            public float nextSpawnTime;
            [Range(0, 10f)] public float distance;
        }
    }
}