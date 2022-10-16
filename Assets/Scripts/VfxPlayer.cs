using System.Collections;
using System.Collections.Generic;
using AttackModificators;
using Pools;
using UnityEngine;

public class VfxPlayer : MonoBehaviour
{
    readonly Dictionary<VFX, VFXPool> _pools = new();

    void Start()
    {
        Events.Instance.OnVfxPlayRequest += PlayVfx;
    }

    void PlayVfx(VFX vfxPrefab, Vector3 pos, Quaternion rot, float delay)
    {
        if (!vfxPrefab) return;
        if (delay > 0)
            StartCoroutine(DelayPlay(vfxPrefab, pos, rot, delay));
        else
        {
            var vfx = Pool(vfxPrefab).Get();
            vfx.transform.position = pos;
            vfx.transform.rotation = rot;
        }
    }

    IEnumerator DelayPlay(VFX vfxPrefab, Vector3 pos, Quaternion rot, float delay)
    {
        yield return new WaitForSeconds(delay);
        var vfx = Pool(vfxPrefab).Get();
        vfx.transform.position = pos;
        vfx.transform.rotation = rot;
    }

    VFXPool Pool(VFX vfx)
    {
        if (_pools.ContainsKey(vfx)) return _pools[vfx];

        var container = new GameObject {name = "Pool: " + vfx.name};
        container.transform.SetParent(transform);

        var pool = container.AddComponent<VFXPool>();
        pool.SetPrefab(vfx);

        _pools.Add(vfx, pool);
        return _pools[vfx];
    }
}