using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wavy
{
    using Interpolation;

    [RequireComponent(typeof(Rigidbody))]
	public class CharacterBase : MonoBehaviour
	{
        // デリゲート
        public System.Action HitAttackHandler { get; set; } = null;
        public System.Action HitDamageHandler { get; set; } = null;
        public System.Action<int, int> LevelUpHandler { get; set; } = null;

        // コンポーネント
        public Rigidbody Rigidbody { get; set; } = null;
        public CharacterEffectController EffectController => CharacterEffectController.Instance;

        // プロパティ
        public Vector3 Acceleration { get; set; } = Vector3.zero;
        public Vector3 Gravity { get; set; } = Vector3.zero;
        public Vector3 GravityScale => Vector3.down * 0.075f * Time.deltaTime * Define.BaseFps;
        public CharacterLevelData LevelData => DataManager.Instance.CharacterLevelDataList[Level];
        public CharacterBase LastHitCharacter { get; set; } = null;
        public int Level { get; set; } = 0;
        public Vector3 Velocity
        {
            get{ return CharacterManager.Instance.IsPause ? Vector3.zero : _velocity; }
            set { _velocity = value; }
        }
        public GameObject SkinObject { get; set; } = null;

        // フィールド
        private Vector3 _velocity = Vector3.zero;

        // 落下フラグ
        private bool _isFall = false;
        public bool IsFall { get { return _isFall; } set { _isFall = value; /*if (value) { GetComponent<Collider>().enabled = false; }*/ } }



        /// <summary>
        /// awake
        /// </summary>
        protected virtual void Awake()
        {
            Rigidbody = GetComponent<Rigidbody>();

            CharacterManager.Instance.CharacterList.Add(this);
        }

        /// <summary>
        /// start
        /// </summary>
        protected virtual void Start ()
		{
            // UIの作成
            var uiObj = Instantiate(ParamaterManager.Instance.CharacterParam.UICanvas/*, transform*/);
            uiObj.GetComponent<CharacterUI>().Init(this);

            // スキンの作成
            CreateSkin(ParamaterManager.Instance.CharacterParam.SkinIndex);
        }

        /// <summary>
        /// update
        /// </summary>
        protected virtual void Update ()
		{
            Movement();
#if UNITY_EDITOR || UNITY_IOS
            Acceleration *= 0.9f/* * Time.deltaTime * Define.BaseFps*/;
#else
            Acceleration *= 0.9f * Time.deltaTime * Define.BaseFps;
#endif
            Gravity += GravityScale;

            // アニメーションを再生する
            AnimationChange();

            // 削除チェック
            DestroyCheck();


            // 地面チェック
            if (!Physics.Raycast(transform.position, Vector3.down, 10f, LayerDefine.GetBits(LayerDefine.Layer.Ground)))
            {
                IsFall = true;
            }
            else
            {
                IsFall = false;
            }
        }

        /// <summary>
        /// on destroy
        /// </summary>
        protected virtual void OnDestroy()
        {
            CharacterManager.Instance.CharacterList.Remove(this);
        }

        /// <summary>
        /// コリジョンにヒットした瞬間に呼ばれる
        /// </summary>
        /// <param name="other">Other.</param>
        protected virtual void OnCollisionEnter(Collision other)
        {
            if (Velocity == Vector3.zero) { return; }
            if (!other.gameObject.GetComponent<CharacterBase>()) { return; }
            if (other.gameObject == gameObject) { return; }


            float range = 70f;

            Vector3 targetDir = (other.transform.position - transform.position).normalized;
            float angle = Vector3.SignedAngle(targetDir, transform.forward, Vector3.up);

            //// left
            //if (angle < -range)
            //{
            //}
            //// right
            //else if (angle > range)
            //{
            //}
            //// forward
            //else
            {
                float otherAngle = Vector3.SignedAngle(transform.position - other.transform.position, -other.transform.forward, Vector3.up);
                bool isSuper = otherAngle > -range/2f && otherAngle < range/2f;

                var character = other.gameObject.GetComponent<CharacterBase>();
                if (character)
                {
                    HitAttack(other, character);
                }
            }
        }

        /// <summary>
        /// コリジョンにヒットしている間呼び出される
        /// </summary>
        /// <param name="other">Collision.</param>
        protected void OnCollisionStay(Collision other)
        {
            if (IsFall) { return; }
            if (other.gameObject.layer == LayerDefine.Layer.Ground.ToInt())
            {
                Gravity = Vector3.zero;
            }
        }



        // 攻撃ヒット時
        public virtual void HitAttack(Collision other, CharacterBase character)
        {
            float power = 25f;
            character.LastHitCharacter = this;

            EffectController.CreateEffect(CharacterEffectController.EffectType.DestroyCharacter, other.contacts[0].point, transform.rotation, transform);
            EffectController.CreateEffect(CharacterEffectController.EffectType.DestroyCharacter, other.contacts[0].point, transform.rotation, transform);
            Vector3 hitVec = (other.transform.position - transform.position).normalized;
            hitVec.y = 0f;
            float diff = (Level - character.Level) * 4f;
            diff = Mathf.Max(diff, -power / 2f);
            hitVec *= power + diff;
#if UNITY_EDITOR || UNITY_IOS
            character.Acceleration += hitVec/* * Time.deltaTime * Define.BaseFps*/;
#else
            character.Acceleration += hitVec * Time.deltaTime * Define.BaseFps;
#endif
        }

        // スーパーダメージ時
        public virtual void HitSuperDamage(Collision other, CharacterBase character)
        {
            float power = 40f;
            character.LastHitCharacter = character;

            EffectController.CreateEffect(CharacterEffectController.EffectType.DestroyCharacter, other.contacts[0].point, transform.rotation, transform);
            EffectController.CreateEffect(CharacterEffectController.EffectType.DestroyCharacter, other.contacts[0].point, transform.rotation, transform);
            Vector3 hitVec = (transform.position - other.transform.position).normalized;
            hitVec.y = 0f;
            float diff = (Level - character.Level) * 4f;
            diff = Mathf.Max(diff, -power / 2f);
            hitVec *= power + diff;
#if UNITY_EDITOR || UNITY_IOS
            Acceleration += hitVec/* * Time.deltaTime * Define.BaseFps*/;
#else
            Acceleration += hitVec * Time.deltaTime * Define.BaseFps;
#endif
        }


        // 移動処理
        protected virtual void Movement()
        {
            if (CharacterManager.Instance.IsPause)
            {
                Rigidbody.velocity = Vector3.zero;
                return;
            }
            float y = Rigidbody.velocity.y;
            Vector3 forward = transform.forward.zeroY() * (this is Player ? 3.0f : 2.9f);
            Rigidbody.velocity = forward * 2f + Vector3.up * y + Acceleration + Gravity;
            if (Velocity != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Velocity, Vector3.up), 0.1f);
            }
        }


        // レベルアップ
        protected virtual void LevelUp(int level)
        {
            if (Level < DataManager.Instance.CharacterLevelDataList.Count)
            {
                LevelUpHandler?.Invoke(Level, level);
                Level += level;
                transform.Easing_LocalScale(Easing.Type.Cubic_Out, Vector3.one + (Vector3.one * 0.25f * (Level + 1)), 1f);
                EffectController.CreateEffect(CharacterEffectController.EffectType.LevelUp, transform.position, transform.rotation, transform);
            }
        }

        // 削除チェック
        protected virtual void DestroyCheck()
        {
            // 落ちたら削除する
            if (transform.position.y <= -30f)
            {
                LastHitCharacter?.LevelUp(Level + 1);
                Destroy(gameObject);
            }
        }




        public virtual void CreateSkin(int index = -1)
        {
            Destroy(SkinObject);

            index = Mathf.Clamp(index, 0, ParamaterManager.Instance.CharacterParam.SkinPrefabs.Count - 1);
            SkinObject = Instantiate(ParamaterManager.Instance.CharacterParam.SkinPrefabs[index], transform);
            SkinObject.transform.localPosition = Vector3.down * 0.25f;
            SkinObject.transform.localScale = Vector3.one * 0.5f;
            SkinObject.transform.localRotation = Quaternion.AngleAxis(0, Vector3.up);
        }
        protected virtual void AnimationChange()
        {
            //if (Velocity != Vector3.zero)
            //{
            //    SkinObject?.GetComponent<Animation>()?.Play("Run", PlayMode.StopAll);
            //}
            //else
            //{
            //    SkinObject?.GetComponent<Animation>()?.Play("Idle", PlayMode.StopAll);
            //}
        }
    }
}
