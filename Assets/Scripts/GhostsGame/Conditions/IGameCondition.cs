using System;

namespace GhostsGame.Conditions
{
    public interface IGameCondition
    {
        event Action ConditionMet;
        void StartChecking();
        void StopChecking();
    }
}
