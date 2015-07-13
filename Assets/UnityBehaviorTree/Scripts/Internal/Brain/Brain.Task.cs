﻿using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace UBT
{
    public partial class Brain : MonoBehaviour
    {
        private Dictionary<Task, RuntimeTask> _runtimeTasks = new Dictionary<Task, RuntimeTask>();

        private void InitializeTask(Task task)
        {
            RuntimeTask rt = new RuntimeTask(task, task.targetMethod);
            MonoBehaviour comp = GetEqualTypeComponent(task.targetScript.GetType()) as MonoBehaviour;
            if (comp == null)
            {
                comp = gameObject.AddComponent(task.targetScript.GetType()) as MonoBehaviour;
                IInitializable initializable = comp as IInitializable;
                initializable.Initialize();
            }
            rt.taskFunc = Delegate.CreateDelegate(typeof(Func<IDisposable>), comp, task.targetMethod) as Func<IDisposable>;
            _runtimeTasks.Add(task, rt);
        }

        private void StartTask(Node node)
        {
            _aliveBehavior = node;
            if (node is Task)
            {
                RuntimeTask rt = GetRuntimeTask(_aliveBehavior as Task);
                rt.Start();
            }
        }

        private void FinishTask()
        {
            if (_aliveBehavior is Task)
            {
                RuntimeTask rt = GetRuntimeTask(_aliveBehavior as Task);
                rt.Finish();
            }
        }

        private RuntimeTask GetRuntimeTask(Task task)
        {
            RuntimeTask value = null;
            if (_runtimeTasks.ContainsKey(task))
            {
                value = _runtimeTasks[task];
            }
            return value;
        }
    }
}