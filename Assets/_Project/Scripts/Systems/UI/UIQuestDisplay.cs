using _Project.Scripts.Systems.Quest;
using UnityEngine;

namespace _Project.Scripts.Systems.UI
{
    public class UIQuestDisplay : MonoBehaviour
    {
        [SerializeField] private Transform _parentQuest;
        [SerializeField] private UIQuest _questTemplate;

        public void AddQuests(QuestInfo info)
        {
            UIQuest quest = Instantiate(_questTemplate, _parentQuest);
            quest.SetVisual(info.Name, info.Description);
            info.Quest.OnCompleteAll += _ => quest.SetState(true);
            info.Quest.OnUncomplete += _ => quest.SetState(false);
            foreach (TaskInfo task in info.Tasks)
            {
                quest.AddTask(task.Name, task.Description, task.Task);
            }
        }
    }
}