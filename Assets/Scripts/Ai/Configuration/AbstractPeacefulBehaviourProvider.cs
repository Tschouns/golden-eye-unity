using Assets.Scripts.Ai.Behaviour;
using Assets.Scripts.Misc;
using UnityEngine;

namespace Assets.Scripts.Ai.Configuration
{
    public abstract class AbstractPeacefulBehaviourProvider : MonoBehaviour, IVerifyable, IPeacefulBehaviourProvider
    {
        public abstract void Verify();
        public abstract IBehaviour GetPeacefulBehaviour();
    }
}