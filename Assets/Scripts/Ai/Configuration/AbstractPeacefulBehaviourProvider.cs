using Assets.Scripts.Ai.Behaviour;
using Assets.Scripts.Misc;
using UnityEngine;

namespace Assets.Scripts.Ai.Configuration
{
    public abstract class AbstractPeacefulBehaviourProvider : MonoBehaviour, IPeacefulBehaviourProvider, IVerifyable
    {
        public abstract IBehaviour GetPeacefulBehaviour();
        public abstract void Verify();
    }
}