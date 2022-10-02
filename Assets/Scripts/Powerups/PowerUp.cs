using UnityEngine;

namespace Powerups
{
    public abstract class PowerUp : MonoBehaviour
    {
        public bool IsAvailable { get; private set; }
        public abstract void Use();

        protected void ReturnToPool()
        {
            IsAvailable = true;
        }
    }
}