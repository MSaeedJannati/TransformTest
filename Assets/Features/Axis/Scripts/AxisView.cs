using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TransformTest.General;
using UnityEngine;
using Zenject;

namespace TransformTest
{
    public class AxisView : MonoBehaviour
    {
        [SerializeField] private Transform _axisDotTransform;
        [SerializeField] private Axis _axis;
        private Vector3 _initPos;
        private AxisLogic _logic;
        [Inject] private AxisLogic.Factory _axisLogicFactory;
        public Vector3 initPos => _initPos;
        public Axis axis => _axis;
        private void Awake()
        {
            _initPos = _axisDotTransform.position;
        }

        [Inject]
        void Construct()
        {
            TryGetComponent(out InputHandler handler);
            _logic = _axisLogicFactory.Create(this, handler);
        }

        public Transform AxisDotTransform => _axisDotTransform;

        public void SetPosition(Vector3 worldPos)
        {
            _axisDotTransform.position = worldPos;
        }

        public void GoBackToInitPoint()
        {
            transform.DOMove(_initPos, .5f);
        }
    }
}

