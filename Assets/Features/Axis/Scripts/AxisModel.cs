using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace TransformTest
{
    [CreateAssetMenu(fileName = "AxisModel",
        menuName = "TransformTest/Models/AxisModel")]
    public class AxisModel : ScriptableObject
    {
        public float maxDeltaPos;
        public List<AxisInfo> axisInfos;
#if UNITY_EDITOR
        [Button]
        public void InitModel()
        {
            for (var i = Axis.Undifined + 1; i < Axis.End; i++)
            {
                if (axisInfos.Exists(item => item.axis == i))
                    continue;
                var info = new AxisInfo() {axis = i};
                axisInfos.Insert((int) (i-1), info);
            }

            for (int i = axisInfos.Count - 1; i >= 0; i--)
            {
                if (axisInfos[i].axis >= Axis.End ||
                    axisInfos[i].axis <= Axis.Undifined)
                    axisInfos.RemoveAt(i);
            }
        }
#endif
    }

    [Serializable]
    public class AxisInfo
    {
        public Axis axis;
        public Vector2 slope;
    }

    [Serializable]
    public enum Axis
    {
        Undifined,
        X,
        Y,
        Z,
        End
    }
}