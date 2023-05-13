using System;
using System.Collections.Generic;
using _Project.Scripts.Systems.Quest;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.Systems.UI
{
    public class UIQuest : MonoBehaviour
    {
        [SerializeField] private Image _stateImage;
        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _description;
        [SerializeField] private Transform _taskParent;
        [SerializeField] private UITask _taskTemplate;

        [SerializeField] private Sprite _spriteComplete;
        [SerializeField] private Sprite _spriteUncomplete;

        public UIQuest SetVisual(string name, string description)
        {
            _name.text = name;
            _description.text = description;
            SetState(false);
            return this;
        }

        public UIQuest SetState(bool value)
        {
            if (value)
            {
                _stateImage.sprite = _spriteComplete;
                _stateImage.color = new Color(0.5f, 1f, 0.25f);
            }
            else
            {
                _stateImage.sprite = _spriteUncomplete;
                _stateImage.color = new Color(1f, 0.25f, 0.2f);
            }

            return this;
        }

        public UIQuest AddTask(string name, string description, ITaskQuest iTask)
        {
            UITask task = Instantiate(_taskTemplate, _taskParent);
            task.SetVisual(name, description);
            iTask.OnComplete += _ => task.SetState(true);
            iTask.OnUncomplete += _ => task.SetState(false);
            return this;
        }
    }
}