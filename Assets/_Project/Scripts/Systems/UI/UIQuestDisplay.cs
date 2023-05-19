using System.Collections.Generic;
using _Project.Scripts.Systems.Quest;
using UnityEngine;

namespace _Project.Scripts.Systems.UI
{
    public class UIQuestDisplay : MonoBehaviour
    {
        [SerializeField] private Transform _parentQuest;
        [SerializeField] private UIQuest _questTemplate;

        private List<GameObject> _questsDisplayed = new();

        public void AddQuests(QuestInfo info)
        {
            UIQuest quest = Instantiate(_questTemplate, _parentQuest);
            _questsDisplayed.Add(quest.gameObject);
            quest.SetVisual(info.Name, info.Description);
            info.Quest.OnCompleteAll += _ =>
            {
                if (quest != null) quest.SetState(true);
            };
            info.Quest.OnUncomplete += _ =>
            {
                if (quest != null) quest.SetState(false);
            };
            foreach (TaskInfo task in info.Tasks)
            {
                quest.AddTask(task.Name, task.Description, task.Task);
            }
        }

        public void RemoveAll()
        {
            foreach (GameObject o in _questsDisplayed)
            {
                Destroy(o);
            }
        }
    }
}