using Pools;

namespace AttackModificators
{
    public class VFX : PoolObject
    {
        void OnDisable() => ReturnToPool();
        
    }
}