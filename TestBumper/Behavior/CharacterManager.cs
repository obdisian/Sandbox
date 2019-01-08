using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wavy
{
	public class CharacterManager : SingletonMonoBehaviour<CharacterManager>
	{
        public List<CharacterBase> CharacterList { get; set; } = new List<CharacterBase>();
        public bool IsPause { get; set; } = false;


		// initialize
		protected override void Initialize()
		{
		}
		
		// start
		private void Start()
		{
            Lib.SetFps(Define.BaseFps);
		}

		// update
		private void Update()
		{
            if (Input.GetKeyDown(KeyCode.Space))
            {
                IsPause = !IsPause;
            }
		}
	}
}
