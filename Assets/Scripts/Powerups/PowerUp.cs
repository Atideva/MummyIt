using UnityEngine;

namespace Powerups
{
    public abstract class PowerUp : MonoBehaviour
    {
        [field: SerializeField] public bool IsAvailable { get; private set; }
        protected abstract void OnUse();

        public void Use()
        {
            IsAvailable = false;
            OnUse();
        }
        protected void ReturnToPool()
        {
            IsAvailable = true;
        }
    }
}