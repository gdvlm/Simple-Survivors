using SimpleSurvivors.Variables;
using TMPro;
using UnityEngine;

namespace SimpleSurvivors.Player
{
    public class PlayerLevelUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text levelText;
        [SerializeField] private IntVariable playerLevel;

        void Update()
        {
            levelText.text = $"LV {playerLevel.RuntimeValue}";
        }
    }
}
