using System.Collections.Generic;
using UnityEngine;

namespace SimpleSurvivors.Utils
{
    [CreateAssetMenu(fileName = "AudioData Template", menuName = "ScriptableObjects/AudioData")]
    public class AudioDataSO : ScriptableObject
    {
        [SerializeField] private AudioClip attackSound;
        [SerializeField] private AudioClip healthPickUpSound;
        [SerializeField] private AudioClip levelUpSound;
        [SerializeField] private AudioClip defeatSound;
        [SerializeField] private AudioClip takeDamageSound;

        private readonly Dictionary<string, AudioClip> _soundDictionary = new();

        public void Initialize()
        {
            _soundDictionary[nameof(SoundEffect.Attack)] = attackSound;
            _soundDictionary[nameof(SoundEffect.HealthPickUp)] = healthPickUpSound;
            _soundDictionary[nameof(SoundEffect.LevelUp)] = levelUpSound;
            _soundDictionary[nameof(SoundEffect.Defeat)] = defeatSound;
            _soundDictionary[nameof(SoundEffect.TakeDamage)] = takeDamageSound;
        }

        public AudioClip GetAudioClip(string sound)
        {
            if (!_soundDictionary.TryGetValue(sound, out AudioClip clip))
            {
                Debug.LogError($"Sound {sound} not found in dictionary.");
            }

            return clip;
        }
    }

    public class SoundEffect
    {
        public const string Attack = nameof(Attack);
        public const string HealthPickUp = nameof(HealthPickUp);
        public const string LevelUp = nameof(LevelUp);
        public const string Defeat = nameof(Defeat);
        public const string TakeDamage = nameof(TakeDamage);
    }
}