using System;
using UnityEngine;

namespace SimpleSurvivors.Enemy
{
    [Serializable]
    public class SpawnSchedule
    {
        public GameObject prefab;
        public int count;
        public float distance;
        public float time;
    }
}