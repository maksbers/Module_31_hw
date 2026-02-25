using UnityEngine;

namespace GhostsGame.Controllers
{
    public interface IEntityController
    {
        void OnUpdate();
        void OnDeath();
        void OnControllerColliderHit(ControllerColliderHit hit);
    }
}
