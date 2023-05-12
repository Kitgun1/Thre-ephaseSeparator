using UnityEngine;

namespace KimicuUtilities
{
    public static class KiComponent
    {
        private static readonly Camera Camera = Camera.main;

        public static void SetPositionInWorldSpace<T>(this T component, Vector2 screenPosition) where T : Component
        {
            component.transform.position = Camera.ScreenToWorldPoint(screenPosition);
        }
    }
}