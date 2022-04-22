using System.Collections;
using System.Collections.Generic;
using TransformTest.General;
using UnityEngine;
using Zenject;

namespace TransformTest
{
    public class MainCanvasView : MonoBehaviour
    {
        [Inject] private MainCanvasLogic.Factory _logicFactory;
        private MainCanvasLogic _logic;

        [Inject]
        void Construct()
        {
            _logic = _logicFactory.Create(this);
        }

        public void ResetRotationClicked()
        {
            _logic.ResetRotationClicked();
        }

        public void ResetScaleClicked()
        {
            _logic.ResetScaleClicked();
        }
    }
}