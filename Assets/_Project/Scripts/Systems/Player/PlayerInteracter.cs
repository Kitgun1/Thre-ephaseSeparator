using _Project.Scripts.Systems.Interact;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Project.Scripts.Systems.Player
{
    public class PlayerInteracter
    {
        private Camera _camera;


        public PlayerInteracter(Camera camara)
        {
            _camera = camara;
        }

        public bool TryInteractObject(Vector2 screenPosition)
        {
            Ray ray = _camera.ScreenPointToRay(screenPosition);

            Physics.Raycast(ray, out RaycastHit hit);

            if (hit.collider == null) return false;
            hit.collider.TryGetComponent(out ISwitchable switchable);
            if (switchable == null) return false;
            switchable.Switch();
            return true;
        }
    }
}