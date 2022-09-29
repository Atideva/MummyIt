using UnityEngine;

namespace AudioSystem
{
    public class PlayMusic : MonoBehaviour
    {
        public bool playAtStart;
        public AudioData music;
        [Range(0, 1)] public float volume = 1f;

        void Start()
        {
            if (playAtStart)
                Invoke(nameof(Play), 0.3f);
        }

        void Play() => Audio.PlayMusic(music, volume, 10f);
    }
}