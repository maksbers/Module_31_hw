using UnityEngine;

namespace GhostsGame.Controllers
{
    public class PlayerInputController
    {
        private readonly string _horizontalAxis = "Horizontal";
        private readonly string _verticalAxis = "Vertical";
        private readonly KeyCode _fireKey = KeyCode.Space;

        public Vector3 GetMovementDirection()
        {
            return new Vector3(Input.GetAxisRaw(_horizontalAxis), 0, Input.GetAxisRaw(_verticalAxis));
        }

        public bool IsFiring()
        {
            return Input.GetKeyDown(_fireKey);
        }
    }
}
