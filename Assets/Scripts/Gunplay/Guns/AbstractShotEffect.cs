using UnityEngine;

namespace Assets.Scripts.Gunplay.Guns
{
    public abstract class AbstractShotEffect : MonoBehaviour, IShotEffect
    {
        public abstract void Play();
    }
}
