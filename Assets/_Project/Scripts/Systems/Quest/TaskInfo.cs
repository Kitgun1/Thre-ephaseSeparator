﻿using System;
using System.Collections.Generic;
using System.Linq;
using KimicuUtilities.Attributes;
using UnityEngine;

namespace _Project.Scripts.Systems.Quest
{
    [Serializable]
    public struct TaskInfo
    {
        public string Name;
        public string Description;
        public bool IsRequiredClose;

        [RequireInterface(typeof(ITaskQuest)), SerializeField]
        public GameObject TaskObjects;

        public ITaskQuest Task => TaskObjects.GetComponent<ITaskQuest>();
    }
}