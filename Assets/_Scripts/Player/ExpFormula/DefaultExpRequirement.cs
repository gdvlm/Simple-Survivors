using UnityEngine;

namespace SimpleSurvivors.Player
{
    /// <summary>
    /// Formula referenced from: https://vampire-survivors.fandom.com/wiki/Level_up
    /// Player starts at level 1 and has to collect 5 XP to level up to level 2. Thereafter, the requirement increases
    /// by 10 XP each level until level 20 (i.e. 15 XP is required to go from level 2 to 3, 25 XP from 3 to 4 and so on).
    /// From level 21 to 40 the requirement increases by 13 XP each level, and from level 41 onwards the requirement
    /// increases by 16 XP each level.
    ///
    /// Additionally, at levels 20 and 40 an additional amount of XP - 600 and 2400 respectively - is required to
    /// level up to the next level. However, at these levels the player also gains +100% Growth, increasing their
    /// experience gain, until they reach the next level. 
    /// </summary>
    public class DefaultExpRequirement : IExpRequirement
    {
        public int GetTotalExpRequirement(int currentLevel)
        {
            int totalExperienceNeeded = 5 + Mathf.Clamp(currentLevel - 1, 0, 19) * 10;
            if (currentLevel < 20)
            {
                return totalExperienceNeeded;
            }

            totalExperienceNeeded += 600;
            if (currentLevel == 20)
            {
                return totalExperienceNeeded;
            }
            
            totalExperienceNeeded += Mathf.Clamp(currentLevel - 20, 1, 19) * 13;
            if (currentLevel is > 20 and < 40)
            {
                return totalExperienceNeeded;
            }

            totalExperienceNeeded += 2400;
            if (currentLevel == 40)
            {
                return totalExperienceNeeded;
            }
            
            totalExperienceNeeded += (currentLevel - 40) * 16;
            return totalExperienceNeeded;
        }

        /// <summary>
        /// Custom method to override the total gained experience depending on the current level.
        /// </summary>
        public int GetTotalGainedExp(int currentLevel, int experience)
        {
            if (currentLevel == 20 || currentLevel == 40)
            {
                return experience * 2;
            }

            return experience;
        }
    }
}
