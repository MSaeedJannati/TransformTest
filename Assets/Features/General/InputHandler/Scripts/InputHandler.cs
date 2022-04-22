using System;
using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TransformTest.General
{
    public class InputHandler : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IEndDragHandler, IDragHandler,
        IPointerUpHandler, IPointerExitHandler, IPointerDownHandler
    {
        #region Fields

        [SerializeField] private bool selectionBehaviour;
        [SerializeField] private bool clickable;
        [SerializeField] private bool draggable;

        [ShowIf(nameof(draggable))] [SerializeField]
        float _dragTimeTreshhold = .3f;

        private Vector3 _currentPos;
        private Transform _transform;
      [SerializeField]  private Camera _mainCamera;

        private float _timer = 0.0f;
        private bool _beingDragged;
        private bool _beingSelected;

        public Action onClick;
        public Action<Vector3> onDragEnd;
        public Action<float> onSelect;
        public Action onBeginSelect;
        public Action onDeselect;
        public Action onBeginDrag;
        public Action<Vector3> onDrag;

        #endregion

        #region Monobehaviour events

        private void Awake()
        {
            _transform = transform;
            _mainCamera = Camera.main;

        }

        public void OnPointerClick(PointerEventData pointerEventData)
        {
            if (!clickable)
                return;
            onClick?.Invoke();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (!draggable)
                return;
            if (selectionBehaviour)
                return;
            _beingDragged = true;
            onBeginDrag?.Invoke();
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (!draggable)
                return;
            onDragEnd?.Invoke(GetWorldPoint(eventData));
            Deselect();
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (!draggable)
                return;
            if (!_beingDragged)
            {
                return;
            }

            _currentPos = GetWorldPoint(eventData);
            _currentPos.z = _transform.position.z;
            onDrag?.Invoke(_currentPos);
        }

       

        public void OnPointerDown(PointerEventData eventData)
        {
            if (!selectionBehaviour)
                return;
            if (_beingDragged)
                return;
            if (_beingSelected)
                return;
            _beingSelected = true;
            onBeginSelect?.Invoke();
            StartCoroutine(ScaleSelectCoroutine());
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (!selectionBehaviour)
                return;
            if (!_beingSelected)
                return;
            Deselect();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (!selectionBehaviour)
                return;
            if (_beingDragged)
                return;
            if (!_beingSelected)
                return;
            Deselect();
        }

        #endregion

        #region Methods

        void Deselect()
        {
            _beingDragged = false;
            if (!selectionBehaviour)
                return;
            _beingSelected = false;
            onDeselect?.Invoke();
            _timer = 0.0f;
        }

        Vector3 GetWorldPoint(PointerEventData eventData)
        {
            Vector3 pos = eventData.position;
            pos.z = 10;
            return _mainCamera.ScreenToWorldPoint(pos);
        }

        IEnumerator ScaleSelectCoroutine()
        {
            while (_timer < _dragTimeTreshhold)
            {
                if (_beingDragged)
                    yield break;
                if (!_beingSelected)
                    yield break;
                onSelect?.Invoke(_timer / _dragTimeTreshhold);
                _timer += Time.deltaTime;
                yield return null;
            }

            _beingDragged = true;
            onBeginDrag?.Invoke();
        }

        #endregion
    }
}