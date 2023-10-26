using UnityEngine;

namespace SimpleSurvivors.Utils
{
    public class MusicManager : MonoBehaviour
    {
        [SerializeField] private AudioClip playMusic;

        private AudioSource _audioSource;
        
        void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            _audioSource.clip = playMusic;
        }

        public void Play()
        {
            _audioSource.Play();
        }

        public void Resume()
        {
            _audioSource.UnPause();
        }

        public void Pause()
        {
            _audioSource.Pause();
        }

        public void Stop()
        {
            _audioSource.Stop();
        }
    }
}
