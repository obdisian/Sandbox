using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wavy
{
	public class EmSet : MonoBehaviour
	{
        private List<Vector3> _startPositions = new List<Vector3>();


        /// <summary>
        /// start
        /// </summary>
        private void Start()
        {
            StartCoroutine(SetupCoroutine());
        }

        private IEnumerator SetupCoroutine()
		{
            var startPosObj = GameObject.Find("StartPosition").transform;
            while (true)
            {
                if (GameControllerDispachier.Instance.gameState == GameState.Tutorial)
                {
                    break;
                }
                yield return null;
            }
            for (int i = 0; i < startPosObj.childCount; i++)
            {
                yield return new WaitForSeconds(0.1f);
                Instantiate(ParamaterManager.Instance.CharacterParam.EnemyPrefab, startPosObj.GetChild(i).position, Quaternion.identity);
            }
            Destroy(startPosObj.gameObject);
        }
		
		/// <summary>
		/// update
		/// </summary>
		private void Update ()
		{
			
		}
	}
}
