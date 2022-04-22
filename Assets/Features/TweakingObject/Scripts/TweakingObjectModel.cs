using System.Collections;
using System.Collections.Generic;
using TransformTest.General;
using UnityEngine;
using Zenject;

namespace TransformTest
{
    [CreateAssetMenu(fileName = "TweakingObjectView",
        menuName = "TransformTest/Models/TweakingObjectView")]
    public class TweakingObjectModel : ScriptableObject
    {
        public float rotSpeed;
        public float scaleFactor;
        public float minScale;
        public float maxScale;
    }
   
}

