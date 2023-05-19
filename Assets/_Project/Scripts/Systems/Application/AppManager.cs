using UnityEngine.SceneManagement;

namespace _Project.Scripts.Systems.Application
{
    public static class AppManager
    {
        public static void ApplicationQuit() => UnityEngine.Application.Quit();
        public static void RestartScene() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        public static void SetScene(int value) => SceneManager.LoadScene(value);
        public static void SetFPS(int value) => UnityEngine.Application.targetFrameRate = value;
    }
}