using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wavy.MyInput
{
    /// <summary>
    /// ジェスチャーベース
    /// </summary>
    public abstract class GestureBase
    {
        protected virtual bool IsDown { get { return Input.GetMouseButtonDown(0); } }
        protected virtual bool IsUp { get { return Input.GetMouseButtonUp(0); } }
        protected virtual bool IsHold { get { return Input.GetMouseButton(0); } }
        protected virtual Vector3 TouchPosition { get { return Input.mousePosition; } }

        /// <summary>
        /// 更新処理
        /// </summary>
        public virtual void update()
        {
        }
    }

    /// <summary>
    /// スワイプ
    /// </summary>
    public class SwipeGesture : GestureBase
    {
        // 入力開始座標から現在までの座標までのベクトル
        public Vector3 StartEndVector { get { return TouchPosition - _inputStartPos; } }
        // 直前の座標から現在座標までのベクトル
        public Vector3 FrameVector { get; set; } = Vector3.zero;
        // 入力方向
        public Vector3 Direction { get; set; } = Vector3.zero;

        private Vector3 _prevFramePos = Vector3.zero;
        private Vector3 _inputStartPos = Vector3.zero;


        /// <summary>
        /// 更新処理
        /// </summary>
        public override void update()
        {
            base.update();

            if (IsHold)
            {
                if (TouchPosition != _prevFramePos)
                {
                    Direction = (TouchPosition - _prevFramePos).normalized;
                }
                FrameVector = TouchPosition - _prevFramePos;
                _prevFramePos = TouchPosition;
            }
            else
            {
                reset();
            }
        }
        private void reset()
        {
            _prevFramePos = TouchPosition;
            _inputStartPos = TouchPosition;
            FrameVector = Vector3.zero;
            Direction = Vector3.zero;
        }
    }

    /// <summary>
    /// 仮想スティック
    /// </summary>
    public class JoystickGesture : GestureBase
    {
        public Vector3 Vector { get; set; } = Vector3.zero;
        public Vector3 VectorXZ => new Vector3(Vector.x, 0f, Vector.y);
        private Vector3 _basePos = Vector3.zero;
        private float _threshold = 60f;


        public override void update()
        {
            base.update();

            if (IsDown)
            {
                _basePos = TouchPosition + Vector3.up * 0.001f;
            }
            if (IsHold)
            {
                Vector3 diff = TouchPosition - _basePos;
                float length = diff.Length();

                if (length < _threshold)
                {
                    Vector = diff.normalized * (length / _threshold);
                }
                else
                {
                    Vector = diff.normalized;
                    _basePos = TouchPosition - diff.normalized * _threshold;
                }
            }
            if (IsUp)
            {
                Vector = Vector3.zero;
            }
        }
    }
}

namespace Wavy
{
    using MyInput;

    /// <summary>
    /// 入力管理クラス
    /// </summary>
    public class InputManager : SingletonMonoBehaviour<InputManager>
	{
        public SwipeGesture SwipeGesture { get; set; } = new SwipeGesture();
        public JoystickGesture JoystickGesture { get; set; } = new JoystickGesture();

        private List<GestureBase> _gestureList = new List<GestureBase>();

        // 初期化
        protected override void Initialize()
		{
            _gestureList.Add(SwipeGesture);
            _gestureList.Add(JoystickGesture);
        }

        // 更新
        void Update()
		{
            foreach (var gesture in _gestureList)
            {
                gesture.update();
            }
		}
	}
}
