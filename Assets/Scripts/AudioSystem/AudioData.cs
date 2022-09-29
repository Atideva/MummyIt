using Ranged;
using UnityEngine;

namespace AudioSystem
{
    [CreateAssetMenu(menuName = "New Sound")]
    public class AudioData : ScriptableObject
    {
        public AudioClip[] sounds;
        [Header("Settings")] public RangedFloat volume;
        [RangeFloat(0, 3)] public RangedFloat pitch;
        [SerializeField] float maximumFrequency = 0f;
        bool SoundIsBlocked(int id) => AudioManager.Instance.ClipIsBlocked(sounds[id], maximumFrequency);
        public float Volume => Random.Range(volume.minValue, volume.maxValue);
        public float Pitch => Random.Range(pitch.minValue, pitch.maxValue);
        public AudioClip GetClip()
        {
            if (sounds.Length == 0)
            {
                return null;
            }

            var r = Random.Range(0, sounds.Length);
            return sounds[r];
        }

        public void Play(AudioSource source)
        {
            if (sounds.Length == 0) return;
            var id = Random.Range(0, sounds.Length);
            if (SoundIsBlocked(id)) return;

            source.volume = Volume;
            source.pitch = Pitch;
            source.clip = sounds[id];
            source.Play();
        }



        public void EditorTest(AudioSource source)
        {
            var id = Random.Range(0, sounds.Length);

            source.volume = Random.Range(volume.minValue, volume.maxValue);
            source.pitch = Random.Range(pitch.minValue, pitch.maxValue);
            source.PlayOneShot(sounds[id]);
        }
    }
}