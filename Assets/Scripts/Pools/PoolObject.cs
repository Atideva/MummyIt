using System;
using UnityEngine;

namespace Pools
{
    public class PoolObject : MonoBehaviour, IPoolObject
    {
        public event Action OnReturnToPool = delegate { };
        IPool _pool;
        public void InitPool(IPool pool) => _pool = pool;

        public void ReturnToPool()
        {
            OnReturnToPool();
            _pool.ReturnToPool(this);
        }
    }
}