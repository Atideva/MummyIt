using UnityEngine;
using UnityEngine.Audio;

namespace AudioSystem
{
    public class AudioManagerAutoCreator : MonoBehaviour
    {
        [SerializeField] AudioPrefab poolPrefab;
        [SerializeField] AudioMixerGroup musicMixerGroup;
        [SerializeField] AudioMixerGroup sfxMixerGroup;
        [SerializeField] AudioMixerSnapshot normal;
        [SerializeField] AudioMixerSnapshot subdued;
        [SerializeField] AudioMixerSnapshot timeslow;

        void Start()
        {
            if (AudioManager.Instance) return;

            var obj = new GameObject
            {
                name = "AudioManager"
            };

            var pool = (AudioPool) obj.AddComponent(typeof(AudioPool));
            pool.SetPrefab(poolPrefab);

            var am = (AudioManager) obj.AddComponent(typeof(AudioManager));
            am.Init(pool, musicMixerGroup, sfxMixerGroup, normal, subdued, timeslow);
        }
    }
}