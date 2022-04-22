using System.Collections;
using System.Collections.Generic;
using TransformTest.General;
using UnityEngine;
using Zenject;

namespace TransformTest
{
    public class TweakingObjectView : MonoBehaviour
    {
        [SerializeField] private Transform _parentTransform;
        [SerializeField] private Transform _objectTransform;
        [SerializeField] private BoxCollider _boxCollider;
        public BoxCollider boxCollider => _boxCollider;
        
        private TweakingObjectLogic _logic;
        [Inject] private TweakingObjectLogic.Factory _logicFactory;
        public Transform parentTransform => _parentTransform;
        public Transform objectTransform => _objectTransform;

        [Inject]
        void Construct()
        {
            TryGetComponent(out InputHandler handler);
            _logic = _logicFactory.Create(this, handler);
        }


        public Transform GetMainCamera()
        {
            return Camera.main.transform;
        }
    }
}