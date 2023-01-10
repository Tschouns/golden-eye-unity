using Assets.Scripts.Ai.Behaviour;
using UnityEngine;

namespace Assets.Scripts.Ai.Configuration
{
    public abstract class AbstractAlertBehaviourProvider : MonoBehaviour, IAlertBehaviourProvider
    {
        public abstract IBehaviour GetAlertBehaviour();
    }
}