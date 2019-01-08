using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wavy
{
    using Interpolation;

    public class CharacterEffectController : SingletonMonoBehaviour<CharacterEffectController>
	{
        public enum EffectType
        {
            LevelUp,
            DestroyCharacter,
        }
        public EffectType Type { get; set; } = EffectType.LevelUp;


        // エフェクトリスト
        [SerializeField]
        private List<GameObject> _effectList = null;



        public class EffectUnit
        {
            public GameObject Owner = null;
            public List<ParticleSystem> ParticleSystemList = new List<ParticleSystem>();
            public bool IsDestroy
            {
                get
                {
                    bool result = true;
                    foreach (var particle in ParticleSystemList)
                    {
                        if (particle && particle.isPlaying)
                        {
                            result = false;
                        }
                    }
                    return result;
                }
            }
        }
        public List<EffectUnit> EffectList { get; set; } = new List<EffectUnit>();



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
            if (EffectList.Count <= 0) { return; }
            for (int i = EffectList.Count - 1; i >= 0; i--)
            {
                var effect = EffectList[i];
                if (effect.IsDestroy)
                {
                    Destroy(effect.Owner);
                }
            }
		}


        public void CreateEffect(EffectType type, Vector3 pos, Quaternion quat, Transform parent)
        {
            CreateEffect(type, pos, quat, parent, Vector3.zero);
        }
        public void CreateEffect(EffectType type, Vector3 pos, Quaternion quat, Transform parent, Vector3 scale)
        {
            var effect = new EffectUnit();
            effect.Owner = Instantiate(_effectList[(int)type], pos, quat, parent);
            if (scale != Vector3.zero) { effect.Owner.transform.localScale = scale; }
            foreach (var particle in effect.Owner.transform.GetComponentsInChildren<ParticleSystem>())
            {
                var main = particle.main;
                main.scalingMode = ParticleSystemScalingMode.Hierarchy;
                effect.ParticleSystemList.Add(particle);
            }
            EffectList.Add(effect);
        }
	}
}
