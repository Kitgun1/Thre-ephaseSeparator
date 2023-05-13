using System;
using System.Collections.Generic;
using System.Linq;
using KimicuUtilities.Attributes;
using UnityEngine;

namespace _Project.Scripts.Systems.Quest
{
    [Serializable]
    public struct QuestInfo
    {
        public string Name;
        public string Description;
        public List<TaskInfo> Tasks;
        public IQuest Quest;
    }
}