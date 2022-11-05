using System.Collections.Generic;
using AttackModificators;
using UnityEngine;

namespace Pools
{
    public class Pool<T> : MonoBehaviour, IPool where T : PoolObject
    {
        T _prefab;
        readonly Queue<T> _queue = new();
        string poolName;
        int count;

        public void SetPrefab(T prefab, int prewarmCount = 0)
        {
            _prefab = prefab;
            for (var i = 0; i < prewarmCount; i++) Create().gameObject.SetActive(false);
#if UNITY_EDITOR
            poolName = gameObject.name;
#endif
        }

        public T Get() =>
            _queue.Count <= 0
                ? Create()
                : _queue.Dequeue().With(t => t.gameObject.SetActive(true));

        T Create()
        {
#if UNITY_EDITOR
            count++;
            gameObject.name = poolName + " (" + count + ")";
#endif
            return Instantiate(_prefab, transform).With(p => p.InitPool(this));
        }

        public void ReturnToPool(IPoolObject poolObject)
        {
            if (poolObject is not T obj) return;
            _queue.Enqueue(obj);
            obj.gameObject.SetActive(false);
        }
    }
}