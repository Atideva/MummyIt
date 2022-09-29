using System.Collections;
using UnityEngine;

namespace AudioSystem
{
    public class AudioPrefab : MonoBehaviour
    {
        public AudioSource audioSource;
        public void Init(AudioPool pool) => _myPool = pool;

        AudioPool _myPool;
        void OnEnable() => StartCoroutine(Kostil());

        IEnumerator Kostil()
        {
            yield return new WaitForEndOfFrame();
            var clp = audioSource.clip;
            var delay = clp 
                ? clp.length / audioSource.pitch
                : 1f;
            yield return new WaitForSeconds(delay * Time.timeScale);
            _myPool.ReturnToPool(audioSource);
        }
    }
}