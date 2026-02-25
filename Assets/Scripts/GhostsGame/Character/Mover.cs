using UnityEngine;

namespace GhostsGame.Character
{
    public class Mover
    {
        private readonly CharacterController _characterController;
        private readonly float _speed;

        public Mover(CharacterController characterController, float speed)
        {
            _characterController = characterController;
            _speed = speed;
        }

        public void MoveTo(Vector3 direction)
        {
            Vector3 moveVector = direction.normalized * _speed * Time.deltaTime;
            _characterController.Move(moveVector);
        }
    }
}
