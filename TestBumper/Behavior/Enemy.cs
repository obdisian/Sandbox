using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Wavy
{
    public class Enemy : CharacterBase
	{
        public NavMeshAgent NavMeshAgent { get; set; } = null;

        protected override void Start()
        {
            base.Start();
            _movePos = transform.position;

            Vector3 vec = -transform.position;
            vec.y = 0;
            transform.rotation = Quaternion.LookRotation(vec, Vector3.up);

            NavMeshAgent = GetComponent<NavMeshAgent>();
            NavMeshAgent.updatePosition = false;
            NavMeshAgent.updateRotation = false;
        }

        protected override void Update()
        {
            base.Update();
            RandomMove();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
        }

        public override void HitAttack(Collision other, CharacterBase character)
        {
            base.HitAttack(other, character);
            if (character is Player)
            {
                TapticPlugin.TapticManager.Impact(TapticPlugin.ImpactFeedback.Light);
            }
        }
        public override void CreateSkin(int index = -1)
        {
            var list = ParamaterManager.Instance.CharacterParam.SkinPrefabs;
            base.CreateSkin(Random.Range(0, list.Count));
        }



        // 適当に動かす
        Vector3 _movePos = Vector3.zero;
        private void RandomMove()
        {
            Vector3 p1 = transform.position; p1.y = 0f;
            Vector3 p2 = _movePos; p2.y = 0f;
            if (Vector3.Distance(p1, p2) < (Level + 1) * 0.5f)
            {
                _movePos = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)) * 15f;
                //float len = _movePos.Length();
                //_movePos = _movePos.normalized * Mathf.Min(len, 15f);
                RandomPoint(transform.position, out _movePos);
                NavMeshAgent.SetDestination(_movePos);
            }
            //Vector3 diff = _movePos - transform.position;
            Vector3 diff = NavMeshAgent.steeringTarget - transform.position;
            diff.y = 0f;
            Velocity = diff.normalized * 2.75f;
            NavMeshAgent.nextPosition = transform.position;
        }

        private bool RandomPoint(Vector3 center, out Vector3 result)
        {
            for (int i = 0; i < 100; i++)
            {
                Vector3 randomPoint = Random.insideUnitSphere * 50f;
                randomPoint.y = 0f;
                NavMeshHit hit;
                if (NavMesh.SamplePosition(center + randomPoint, out hit, 100.0f, NavMesh.AllAreas))
                {
                    result = hit.position;
                    return true;
                }
            }
            result = Vector3.zero;
            return false;
        }
    }
}
