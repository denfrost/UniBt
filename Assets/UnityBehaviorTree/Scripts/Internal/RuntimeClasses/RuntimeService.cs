﻿using UnityEngine;
using System.Collections;
using UniRx;

namespace UBT
{
    public class RuntimeService
    {
        public Composite parent;
        public System.Action serviceAction;
        public float tick;
        public System.IDisposable subscription;

        public RuntimeService(Composite parent, float tick)
        {
            this.parent = parent;
            this.tick = tick;
        }
    }
}