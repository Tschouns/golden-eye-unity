using Assets.Scripts.Ai.Behaviour;
using Assets.Scripts.Misc;
using UnityEngine;

namespace Assets.Scripts.Ai.Configuration
{
    public abstract class AbstractAlertBehaviourProvider : MonoBehaviour, IVerifyable, IAlertBehaviourProvider
    {
        public abstract void Verify();
        public abstract IBehaviour GetAlertBehaviour();

    }
}