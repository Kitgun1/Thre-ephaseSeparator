using System.Collections.Generic;
using _Project.Scripts.Systems.UI;
using UnityEngine;

namespace _Project.Scripts.Systems.Quest
{
    public class QuestManager : MonoBehaviour
    {
        [SerializeField] private UIQuestDisplay _display;
        [SerializeField] private UIWindows _windows;
        [SerializeField] private List<Quest> _quests = new List<Quest>();

        private int _currentQuest;

        private readonly List<IQuest> _questsCompleted = new List<IQuest>(16);

        private bool IsAllCompleted => _questsCompleted.Count == _quests.Count;


        private void Start()
        {
            Init();
        }

        private void Init()
        {
            _questsCompleted.Clear();
            Quest firstQuest = _quests[0];
            //firstQuest.InitListener();
            //firstQuest.OnCompleteAll += OnCompletedAll;
            //firstQuest.OnUncomplete += OnUncompleted;
            _currentQuest = 0;
            _display.RemoveAll();
            _display.AddQuests(firstQuest.QuestInfo);

            foreach (Quest questInfo in _quests)
            {
                questInfo.InitListener();
                questInfo.OnCompleteAll += OnCompletedAll;
                questInfo.OnComplete += OnCompleted;
                questInfo.OnUncomplete += OnUncompleted;
            }
        }

        private void OnCompleted(IQuest quest)
        {
            if (quest == _quests[_currentQuest].QuestInfo.Quest) return;

            _windows.OpenWindowAndCloseOther("GameOver");
            Init();
        }

        private void OnCompletedAll(IQuest quest)
        {
            _questsCompleted.Add(quest);
            if (_questsCompleted.Count != _quests.Count)
            {
                _currentQuest++;
                Quest nextQuest = _quests[_currentQuest];
                //nextQuest.InitListener();
                //nextQuest.OnCompleteAll += OnCompletedAll;
                //nextQuest.OnUncomplete += OnUncompleted;
                _display.AddQuests(nextQuest.QuestInfo);
            }

            if (IsAllCompleted)
            {
                _windows.OpenWindowAndCloseOther("GameWin");
                Init();
            }
            //quest.OnCompleteAll -= OnCompleted;
        }

        private void OnUncompleted(IQuest quest)
        {
            int index = -1;
            for (int i = 0; i < _quests.Count; i++)
            {
                if (quest != _quests[i].QuestInfo.Quest) continue;
                index = i;
                _windows.OpenWindowAndCloseOther("GameOver");
                print("GameOver");
                Init();
                break;
            }

            //if (_currentQuest != index)
            //{
            //    _windows.OpenWindowAndCloseOther("GameOver");
            //    print("GameOver2");
            //    Init();
            //}

            _questsCompleted.Remove(quest);
            //quest.OnCompleteAll -= OnCompleted;
        }
    }
}