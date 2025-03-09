using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace StairwayGamesTest.Common
{
    public class AudioEngine : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private List<AudioClip> audioClips = new();

        private readonly Dictionary<string, AudioClip> _audioClipCache = new();
        
        private static AudioEngine _instance;

        private void Awake()
        {
            _instance = this;
        }

        public static void PlaySfx(string audioName)
        {
            if (_instance == null) return;
            
            _instance.InternalPlaySfx(audioName);
        }

        private void InternalPlaySfx(string audioName)
        {
            _audioClipCache.TryAdd(audioName, audioClips.Find(x => x.name == audioName));

            if (_audioClipCache[audioName] == null) return;

            audioSource.pitch = Random.Range(0.90f, 1.1f);
            audioSource.PlayOneShot(_audioClipCache[audioName]);
        }
    }
}
