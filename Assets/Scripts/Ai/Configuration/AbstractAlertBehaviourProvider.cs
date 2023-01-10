using Assets.Scripts.Ai.Behaviour;
using Assets.Scripts.Misc;
using UnityEngine;

namespace Assets.Scripts.Ai.Configuration
{
    public abstract class AbstractAlertBehaviourProvider : MonoBehaviour, IAlertBehaviourProvider, IVerifyable
    {
        public abstract IBehaviour GetAlertBehaviour();
        public abstract void Verify();
    }
}