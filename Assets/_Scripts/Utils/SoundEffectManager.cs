using System.Collections.Generic;
using UnityEngine;

namespace SimpleSurvivors.Utils
{
    public class SoundEffectManager : MonoBehaviour
    {
        [SerializeField] private AudioClip attackSound;
        [SerializeField] private AudioClip healthPickUpSound;
        [SerializeField] private AudioClip levelUpSound;
        [SerializeField] private AudioClip defeatSound;
        [SerializeField] private AudioClip takeDamageSound;

        private readonly Dictionary<string, AudioClip> _soundDictionary = new();
        private AudioSource _audioSource;

        void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        void Start()
        {
            _soundDictionary[nameof(SoundEffect.Attack)] = attackSound;
            _soundDictionary[nameof(SoundEffect.HealthPickUp)] = healthPickUpSound;
            _soundDictionary[nameof(SoundEffect.LevelUp)] = levelUpSound;
            _soundDictionary[nameof(SoundEffect.Defeat)] = defeatSound;
            _soundDictionary[nameof(SoundEffect.TakeDamage)] = takeDamageSound;
        }

        public void PlaySoundEffect(string sound)
        {
            if (!_soundDictionary.TryGetValue(sound, out AudioClip clip))
            {
                print($"Sound {sound} not found in dictionary.");
            }

            _audioSource.clip = clip;
            _audioSource.Play();
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
