using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.Systems.UI
{
    public class UITask : MonoBehaviour
    {
        [SerializeField] private Image _stateImage;
        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _description;

        [SerializeField] private Sprite _spriteComplete;
        [SerializeField] private Sprite _spriteUncomplete;

        public UITask SetVisual(string name, string description)
        {
            _name.text = name;
            _description.text = description;
            SetState(false);
            
            return this;
        }

        public UITask SetState(bool value)
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
    }
}