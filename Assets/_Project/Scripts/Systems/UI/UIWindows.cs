using System;
using System.Collections.Generic;
using _Project.Scripts.Systems.UI.Structures;
using UnityEngine;

namespace _Project.Scripts.Systems.UI
{
    public class UIWindows : MonoBehaviour
    {
        [SerializeField] private List<Window> _windows = new List<Window>();

        private void Start()
        {
            OpenWindowAndCloseOther("Menu");
        }

        public void OpenWindow(string name)
        {
            SwitchWindow(name, true);
        }

        public void OpenAllWindow()
        {
            SwitchWindow("all", true);
        }

        public void OpenWindowAndCloseOther(string name)
        {
            foreach (Window window in _windows)
            {
                window.Parent.SetActive(window.Name == name);
            }
        }

        public void CloseWindow(string name)
        {
            SwitchWindow(name, false);
        }

        public void CloseAllWindow()
        {
            SwitchWindow("all", false);
        }

        private void SwitchWindow(string name, bool value)
        {
            foreach (Window window in _windows)
            {
                if (window.Name.ToLower() != name.ToLower() && window.Name.ToLower() != "all") continue;
                window.Parent.SetActive(value);
                if (window.Name.ToLower() != "all")
                {
                    break;
                }
            }
        }
    }
}