using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TransformTest.General;
using UnityEngine;
using Zenject;

namespace TransformTest
{
    public class TweakingObjectLogic
    {
        [Inject] private TweakingObjectModel _model;
        private TweakingObjectView _view;
        private InputHandler _inputHandler;
        private Transform _camTransform;
        private Quaternion _initRot;
        private Vector3 _initScale;

        public TweakingObjectLogic(TweakingObjectView view, InputHandler inputHandler)
        {
            _view = view;
            _inputHandler = inputHandler;
            _inputHandler.onDragDelta = OnDragDelta;
            _camTransform = _view.GetMainCamera();
            _initRot = _view.parentTransform.rotation;
            _initScale = _view.objectTransform.localScale;
            RegisterToTheEvents();
        }

        ~TweakingObjectLogic()
        {
            UnregisterFromEvents();
        }

        void OnResetScale()
        {
            _view.objectTransform.DOScale(_initScale, .5f);
            _view.boxCollider.size = _view.objectTransform.localScale;
        }

        void OnResetRotation()
        {
            _view.parentTransform.DORotateQuaternion(_initRot, .5f);
        }

        void RegisterToTheEvents()
        {
            EventController.AddListener(EventController.OnAxisDrag, OnAxisDrag);
            EventController.AddListener(EventController.OnScaleReset, OnResetScale);
            EventController.AddListener(EventController.OnRotationReset, OnResetRotation);
        }

        void UnregisterFromEvents()
        {
            EventController.RemoveListener(EventController.OnAxisDrag, OnAxisDrag);
            EventController.RemoveListener(EventController.OnScaleReset, OnResetScale);
            EventController.RemoveListener(EventController.OnRotationReset, OnResetRotation);
        }

        void OnAxisDrag((Axis axis, float dragFactor) dragInfo)
        {
            var unitVector = GetWorldUnitVector(dragInfo.axis);
            var scaleVector = _view.objectTransform.InverseTransformVector(unitVector);

            scaleVector *= dragInfo.dragFactor * _model.scaleFactor;
            scaleVector = CoSigner(scaleVector, dragInfo.dragFactor);
            _view.objectTransform.localScale += scaleVector;
            CheckScale();
            _view.boxCollider.size = _view.objectTransform.localScale;
        }

        void CheckScale()
        {
            var scaleVector = _view.objectTransform.localScale;
            scaleVector.x = GetScale(scaleVector.x);
            scaleVector.y = GetScale(scaleVector.y);
            scaleVector.z = GetScale(scaleVector.z);
            _view.objectTransform.localScale = scaleVector;
        }

        float GetScale(float scale)
        {
            return Mathf.Clamp(scale, _model.minScale, _model.maxScale);
        }

        Vector3 CoSigner(Vector3 value, float reference)
        {
            value.x = CoSigner(value.x, reference);
            value.y = CoSigner(value.y, reference);
            value.z = CoSigner(value.z, reference);
            return value;
        }

        float CoSigner(float value, float reference)
        {
            var output = value;
            if (value * reference < 0)
                output *= -1.0f;
            return output;
        }

        Vector3 GetWorldUnitVector(Axis axis)
        {
            var outPut = Vector3.zero;
            switch (axis)
            {
                case Axis.X:
                    outPut.x = 1.0f;
                    break;
                case Axis.Y:
                    outPut.y = 1.0f;
                    break;
                case Axis.Z:
                    outPut.z = 1.0f;
                    break;
            }

            return outPut;
        }

        void OnDragDelta(Vector2 delta)
        {
            var rotX = delta.x * _model.rotSpeed;
            var rotY = delta.y * _model.rotSpeed;
            var right = Vector3.Cross(_camTransform.up,
                _view.parentTransform.position - _camTransform.position);
            var up = Vector3.Cross(_view.parentTransform.position - _camTransform.position,
                right);
            _view.parentTransform.rotation = Quaternion.AngleAxis(-rotX, up) * _view.parentTransform.rotation;
            _view.parentTransform.rotation = Quaternion.AngleAxis(rotY, right) * _view.parentTransform.rotation;
        }

        public class Factory : PlaceholderFactory<TweakingObjectView, InputHandler, TweakingObjectLogic>
        {
        }
    }
}