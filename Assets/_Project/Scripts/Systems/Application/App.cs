using UnityEngine;

namespace _Project.Scripts.Systems.Application
{
    public class App : MonoBehaviour
    {
        public void Quit()
        {
            AppManager.ApplicationQuit();
        }

        public void Restart()
        {
            PlayerPrefs.SetInt("restarted", 1);
            AppManager.RestartScene();
        }

        public void MoveTo(int value)
        {
            AppManager.SetScene(value);
        }
    }
}