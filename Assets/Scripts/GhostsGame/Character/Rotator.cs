using UnityEngine;

namespace GhostsGame.Character
{
    public class Rotator
    {
        private readonly Transform _transform;
        private readonly float _rotationSpeed;

        public Rotator(Transform transform, float rotationSpeed)
        {
            _transform = transform;
            _rotationSpeed = rotationSpeed;
        }

        public void RotateTo(Vector3 direction)
        {
            if (direction == Vector3.zero)
                return;

            Quaternion lookRotation = Quaternion.LookRotation(direction);
            _transform.rotation = Quaternion.Slerp(_transform.rotation, lookRotation, Time.deltaTime * _rotationSpeed);
        }
    }
}
