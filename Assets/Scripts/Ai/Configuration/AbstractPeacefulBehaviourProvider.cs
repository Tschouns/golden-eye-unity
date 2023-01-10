using Assets.Scripts.Ai.Behaviour;
using UnityEngine;

namespace Assets.Scripts.Ai.Configuration
{
    public abstract class AbstractPeacefulBehaviourProvider : MonoBehaviour, IPeacefulBehaviourProvider
    {
        public abstract IBehaviour GetPeacefulBehaviour();
    }
}