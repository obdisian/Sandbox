using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wavy
{
    public class SubBehaviour
    {
        public SubBehaviour()
        {
            SubController.Instance.Add(this);
            Start();
        }
        ~SubBehaviour()
        {
            SubController.Instance.Remove(this);
            Destroy();
        }
        public void Start() { DoStart(); }
        public void Update() { DoUpdate(); }
        public void LateUpdate() { DoLateUpdate(); }
        public void Destroy() { DoDestroy(); }
        protected virtual void DoStart()
        {

        }
        protected virtual void DoUpdate()
        {

        }
        protected virtual void DoLateUpdate()
        {

        }
        protected virtual void DoDestroy()
        {

        }
    }

    public class SubController : SingletonMonoBehaviour<SubController>
    {
        private List<SubBehaviour> _list = new List<SubBehaviour>();
        public void Add(SubBehaviour subBehaviour)
        {
            _list.Add(subBehaviour);
            _serial++;
        }
        public void Remove(SubBehaviour subBehaviour)
        {
            _list.Remove(subBehaviour);
        }
        private int _serial = 0;
        private int _prevSerial = 0;

        /// <summary>
        /// start
        /// </summary>
        private void Start ()
		{
			
		}
		
		/// <summary>
		/// update
		/// </summary>
		private void Update ()
		{
            if (_serial != _prevSerial)
            {
                _prevSerial = _serial;

                // 処理順を定義後、ソート処理
            }

            foreach (var behavior in _list)
            {
                behavior.Update();
            }
		}

        /// <summary>
        /// late update
        /// </summary>
        private void LateUpdate()
        {
            foreach (var behavior in _list)
            {
                behavior.LateUpdate();
            }
        }
    }
}
