using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace  TransformTest.General
{
    public class EventController 
    {
        #region Events

        #region Genereal
        public static readonly HashSet<Action<(Axis,float)>> OnAxisDrag = new HashSet<Action<(Axis,float)>> ();
 
        #endregion

        #region UI
        public static readonly HashSet<Action> OnScaleReset = new HashSet<Action> ();
        public static readonly HashSet<Action> OnRotationReset = new HashSet<Action> ();
        

        #endregion

      

        #endregion

        public static void TriggerEvent(HashSet<Action> events)
        {
            foreach (var e in events)
            {
                e?.Invoke();
            }
        }

        public static void AddListener(HashSet<Action> events, Action listener)
        {
            if (events.Contains(listener))
                return;
            events.Add(listener);
        }

        public static void RemoveListener(HashSet<Action> events, Action listener)
        {
            if (!events.Contains(listener))
                return;
            events.Remove(listener);
        }

        public static void TriggerEvent<T>(HashSet<Action<T>> events, T amount)
        {
            foreach (var e in events)
            {
                e?.Invoke(amount);
            }
        }

        public static void AddListener<T>(HashSet<Action<T>> events, Action<T> listener)
        {
            if (events.Contains(listener))
                return;
            events.Add(listener);
        }

        public static void RemoveListener<T>(HashSet<Action<T>> events, Action<T> listener)
        {
            if (!events.Contains(listener))
                return;
            events.Remove(listener);
        }
    }
}

