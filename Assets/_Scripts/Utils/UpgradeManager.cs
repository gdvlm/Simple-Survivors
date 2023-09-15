using System;
using System.Collections.Generic;
using SimpleSurvivors.Upgrade;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SimpleSurvivors.Utils
{
    public class UpgradeManager : MonoBehaviour
    {
        [SerializeField] private UpgradeSO[] availableUpgrades;

        /// <summary>
        /// Gets random specified count of Upgrade SOs.
        /// </summary>
        public UpgradeSO[] GetRandomUpgrades(int count)
        {
            int minCount = Math.Min(count, availableUpgrades.Length);
            HashSet<int> indexes = new();
            UpgradeSO[] randomSos = new UpgradeSO[minCount];

            while (indexes.Count < minCount)
            {
                int randomIndex = Random.Range(0, availableUpgrades.Length);
                randomSos[indexes.Count] = availableUpgrades[randomIndex];
                indexes.Add(randomIndex);
            }

            return randomSos;
        }
    }
}
