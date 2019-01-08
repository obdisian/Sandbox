using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wavy
{
	public class ParamaterManager : SingletonMonoBehaviour<ParamaterManager>
	{
        public CharacterParam CharacterParam { get; set; } = null;

        // initialize
        protected override void Initialize()
		{
            CharacterParam = Resources.Load("Param/CharacterParam") as CharacterParam;
        }
		
		// start
		private void Start()
		{

		}

		// update
		private void Update()
		{

		}
	}
}
