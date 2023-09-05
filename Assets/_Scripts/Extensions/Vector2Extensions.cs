using UnityEngine;

namespace SimpleSurvivors.Extensions
{
    public static class Vector2Extensions
    {
        /// <summary>
        /// Get a random position based on a certain distance from the center.
        /// </summary>
        public static Vector2 GetRandomPositionByDistance(float distance)
        {
            return Random.insideUnitCircle.normalized * distance;
        }    
    }
}