using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wavy
{
    public class Player : CharacterBase
	{
        /// <summary>
        /// start
        /// </summary>
        protected override void Start ()
		{
            base.Start();
            //LevelUp(2);   // ブースト
		}

        /// <summary>
        /// update
        /// </summary>
        protected override void Update ()
		{
            base.Update();
            CameraUpdate();
            WinCheck();

            Velocity = InputManager.Instance.JoystickGesture.VectorXZ.normalized;
            if (Velocity == Vector3.zero)
            {
                Velocity = transform.forward * 3f;
            }
        }

        protected override void OnDestroy()
        {
            GameControllerDispachier.Instance.GameResult(() =>
            {
                ScoreDispachier.Instance.Ranking(CharacterManager.Instance.CharacterList.Count);
            });
            base.OnDestroy();
            //CharacterManager.Instance.IsPause = true;
        }

        protected override void OnCollisionEnter(Collision other)
        {
            base.OnCollisionEnter(other);

            if (Velocity == Vector3.zero) { return; }
            if (!other.gameObject.GetComponent<BumperCollision>()) { return; }
            if (other.gameObject == gameObject) { return; }
            TapticPlugin.TapticManager.Impact(TapticPlugin.ImpactFeedback.Medium);
        }


        public override void HitAttack(Collision other, CharacterBase character)
        {
            base.HitAttack(other, character);
            TapticPlugin.TapticManager.Impact(TapticPlugin.ImpactFeedback.Light);
        }
        public override void HitSuperDamage(Collision other, CharacterBase character)
        {
            base.HitSuperDamage(other, character);
            TapticPlugin.TapticManager.Impact(TapticPlugin.ImpactFeedback.Heavy);
        }

        protected override void LevelUp(int level)
        {
            base.LevelUp(level);
            print($"level = {Level}");
            ScoreDispachier.Instance.Kill();
        }


        // カメラの更新
        protected void CameraUpdate()
        {
            var mainCamera = CameraManager.Instance.MainCamera;
            mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
            if (!mainCamera) { return; }

            if (IsFall)
            {
                mainCamera.gameObject.transform.rotation = Quaternion.LookRotation((transform.position - mainCamera.transform.position).normalized, Vector3.up);
            }
            else
            {
                Vector3 cameraAxis = Vector3.up * 10f + Vector3.back * 5f;
                cameraAxis = cameraAxis.normalized;
                //CameraManager.Instance.SetFollow(gameObject, Vector3.zero, transform.position + cameraAxis * 13f);
                mainCamera.gameObject.transform.position = transform.position + cameraAxis * 22.5f;
                mainCamera.gameObject.transform.rotation = Quaternion.LookRotation(-cameraAxis, Vector3.up);
            }
        }

        public void Win()
        {
            gameObject.transform.Find("MM_Crown").gameObject.SetActive(true);
        }
        private bool _isWin = false;
        public void WinCheck()
        {
            if (GameControllerDispachier.Instance.gameState != GameState.GamePlay) { return; }
            if (_isWin) { return; }
            if (CharacterManager.Instance.CharacterList.Count <= 1)
            {
                _isWin = true;
                GameControllerDispachier.Instance.GameResult(() =>
                {
                    ScoreDispachier.Instance.Ranking(1);
                    gameObject.transform.Find("MM_Crown").gameObject.SetActive(true);
                });
                CharacterManager.Instance.IsPause = true;
            }
        }
    }
}
