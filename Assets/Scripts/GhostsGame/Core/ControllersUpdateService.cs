using System.Collections.Generic;
using System.Linq;
using GhostsGame.Controllers;

namespace GhostsGame.Core
{
    public class ControllersUpdateService
    {
        private readonly List<IEntityController> _controllers = new();

        public void Register(IEntityController controller)
        {
            if (!_controllers.Contains(controller))
                _controllers.Add(controller);
        }

        public void Unregister(IEntityController controller)
        {
            _controllers.Remove(controller);
        }

        public void Update()
        {
            foreach (IEntityController controller in _controllers.ToList())
            {
                if (_controllers.Contains(controller))
                {
                    controller.OnUpdate();
                }
            }
        }
    }
}
