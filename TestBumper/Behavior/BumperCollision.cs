using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wavy
{
	public class BumperCollision : MonoBehaviour
	{
        private CharacterBase Self { get; set; } = null;

        /// <summary>
        /// start
        /// </summary>
        private void Start ()
		{
            Self = transform.parent.GetComponent<CharacterBase>();
		}

        /// <summary>
        /// Ons the collision enter.
        /// </summary>
        /// <param name="other">Collision.</param>
        private void OnCollisionEnter(Collision other)
        {
            //var otherCharacter = other.gameObject.GetComponent<CharacterBase>();
            //if (!otherCharacter) { return; }
            //Self.HitSuperDamage(other, otherCharacter);
            var character = other.gameObject.GetComponent<CharacterBase>();
            if (Self != character)
            {
                Self.HitSuperDamage(other, character);
                StartCoroutine(WeakHitEffect(other.contacts[0].point));
            }
        }

        private IEnumerator WeakHitEffect(Vector3 hitPoint)
        {
            Vector3 offset = transform.position - hitPoint;
            float timer = 0f;
            while (timer < 0.7f)
            {
                timer += Time.deltaTime;
                Self.EffectController.CreateEffect(CharacterEffectController.EffectType.DestroyCharacter, transform.position + offset, transform.rotation, null, Vector3.one * 1.5f);
                yield return null;
            }
        }
    }
}
