using System;
using UnityEngine;

namespace _Project.Scripts.Systems.UI
{
    public class UIWindows : MonoBehaviour
    {
        [SerializeField] private GameObject _tipsParent;

        public void SwitchTip(UIItem item)
        {
            switch (item)
            {
                case UIItem.Tip:
                    _tipsParent.SetActive(!_tipsParent.activeSelf);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(item), item, null);
            }
        }
    }

    public enum UIItem
    {
        Tip
    }
}