﻿
using Assets.Scripts.Ai.Behaviour;

namespace Assets.Scripts.Ai.Configuration
{
    public interface IPeacefulBehaviourProvider
    {
        IBehaviour GetPeacefulBehaviour();
    }
}