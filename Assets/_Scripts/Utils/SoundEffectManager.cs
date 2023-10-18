using UnityEngine;

namespace SimpleSurvivors.Utils
{
    public class SoundEffectManager : MonoBehaviour
    {
        [SerializeField] private AudioDataSO audioData;

        private AudioSource _audioSource;

        void Awake()
        {
            audioData.Initialize();
            _audioSource = GetComponent<AudioSource>();
        }

        public void PlaySoundEffect(string sound)
        {
            AudioClip audioClip = audioData.GetAudioClip(sound);
            _audioSource.clip = audioClip;
            _audioSource.Play();
        }
    }
}