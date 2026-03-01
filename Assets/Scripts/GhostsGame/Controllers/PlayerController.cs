using UnityEngine;
using GhostsGame.Character;

namespace GhostsGame.Controllers
{
    public class PlayerController : IEntityController
    {
        private readonly CharacterEntity _character;
        private readonly PlayerInputController _input;
        private readonly float _deadZone = 0.1f;

        private readonly int IsRunningKey = Animator.StringToHash("IsRunning");
        private readonly int IsWinKey = Animator.StringToHash("IsWin");

        public PlayerController(CharacterEntity character)
        {
            _character = character;
            _input = new PlayerInputController();

            _character.Health.Died += OnDeath;
        }

        public void OnUpdate()
        {
            if (_character == null || !_character.IsActive)
                return;

            Vector3 inputAxis = _input.GetMovementDirection();

            if (_input.IsFiring() && _character.Weapon != null)
                _character.Weapon.Fire();

            bool isRunning = inputAxis.magnitude >= _deadZone;

            if (_character.Animator != null)
                _character.Animator.SetBool(IsRunningKey, isRunning);

            if (!isRunning)
                return;

            _character.Mover?.MoveTo(inputAxis);
            _character.Rotator?.RotateTo(inputAxis);
        }

        private void OnDeath()
        {
            if (_character != null && _character.Animator != null)
            {
                _character.Animator.SetBool(IsRunningKey, false);
                _character.Animator.SetBool(IsWinKey, true);
            }
        }
    }
}
