using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TransformTest.General;
using UnityEngine;
using Zenject;

namespace TransformTest
{
    public class AxisLogic
    {
        private AxisView _view;
        private InputHandler _inputHandler;
        [Inject] private AxisModel _model;

        private Vector3 _slopeUnit;
        

        public AxisLogic(AxisView view, InputHandler inputHandler)
        {
            _view = view;
            _inputHandler = inputHandler;
            _inputHandler.onDrag = OnDrag;
            _inputHandler.onDragEnd = OnDragEnd;
            _inputHandler.onBeginDrag = OnBeginDrag;
        }

        void OnBeginDrag()
        {
            TryGetSlope();
        }

        void TryGetSlope()
        {
            if(_slopeUnit!=default)
                return;
            _slopeUnit = GetSlope(_view.axis);
        }

        Vector2 GetSlope(Axis aixs)
        {
            var info = _model.axisInfos.FirstOrDefault(i => i.axis == aixs);
            if (info == default)
                return default;
            return info.slope.normalized;
            
        }

        void OnDragEnd(Vector3 pos)
        {
            _view.GoBackToInitPoint();
        }

        void OnDrag(Vector3 currentPos)
        {
            var pos = currentPos - _view.initPos;
            var size = Vector2.Dot(pos, _slopeUnit);
            size = Mathf.Clamp(size, -_model.maxDeltaPos, _model.maxDeltaPos);
            pos = size * _slopeUnit+_view.initPos;
            _view.SetPosition(pos);
            EventController.TriggerEvent(EventController.OnAxisDrag,(_view.axis,size/_model.maxDeltaPos));
            
        }

        public class Factory : PlaceholderFactory<AxisView, InputHandler, AxisLogic>
        {
        }
    }
}

