using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wavy.Interpolation;

namespace Wavy
{
    public class CameraManager : SingletonMonoBehaviour<CameraManager>
    {
        public Camera MainCamera { get { return Camera.main; } }
        public Color BaseBackgroundColor { get; set; }


        // initialize
        protected override void Initialize()
        {
        }

        // start
        private void Start()
        {

        }

        // update
        private void Update()
        {
            if (_quakeTimer > 0f)
            {
                _quakeTimer -= Time.deltaTime;
                MainCamera.transform.position += _quakeBasePos + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * _quakePower;

                if (_quakeTimer <= 0f)
                {
                    _quakePower = 0f;
                    _quakeTimer = 0f;
                    MainCamera.transform.position = _quakeBasePos;
                }
            }

            if (_hitStopTimer > 0)
            {
                _hitStopTimer--;
                Time.timeScale = 0f;
            }
            else{
                _hitStopTimer = 0;
                Time.timeScale = 1f;
            }
        }


        // カメラ振動を起こす
        public void SetQuake(float power, float time)
        {
#if false
            StartCoroutine(QuakeCoroutine(power, time));
#else
            if (_quakeTimer > 0f)
            {
                if (_quakePower <= power)
                {
                    _quakePower = power;
                    _quakeTimer = time;
                }
            }
            else
            {
                _quakePower = power;
                _quakeTimer = time;
                _quakeBasePos = MainCamera.transform.position;
            }
#endif
        }
        private Vector3 _quakeBasePos = Vector3.zero;
        private float _quakePower = 0f;
        private float _quakeTimer = 0f;

        public void SetHitStop(int frame = 1)
        {
            _hitStopTimer = frame;
        }
        private float _hitStopTimer = 0f;

#if false
        private IEnumerator QuakeCoroutine(float power, float time)
		{
			Vector3 basePos = MainCamera.transform.position;
            float timer = time;
			while (timer > 0f)
			{
                timer -= Time.deltaTime;
				MainCamera.transform.position += basePos + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * power;
				yield return null;
			}
			MainCamera.transform.position = basePos;
		}
#endif



        #region 追従カメラ
        private int _followId = 0;
        public void ResetFollow()
        {
            _followId = -1;
        }
        public void SetFollow(GameObject target, Vector3 lookAtOffset, Vector3 cameraOffset)
        {
            _followId++;
            if (target)
            {
                StartCoroutine(FollowCoroutine(_followId, target, lookAtOffset, cameraOffset));
            }
        }
        private IEnumerator FollowCoroutine(int followId, GameObject target, Vector3 lookAtOffset, Vector3 cameraOffset)
        {
            while (_followId == followId)
            {
                MainCamera.transform.position = target.transform.position + cameraOffset;
                MainCamera.transform.rotation = Quaternion.LookRotation(target.transform.position + lookAtOffset - MainCamera.transform.position, Vector3.up);
                yield return null;
            }
        }
        #endregion
    }
}
