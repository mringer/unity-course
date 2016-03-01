using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
	public static class InputHelpers
	{
		public static KeyCode FindUniqueKey(this List<KeyCode> keys, KeyCode key)
		{
			bool isUnique = false;
			while (!isUnique) {
				if (!keys.Contains(key)) {
					isUnique = true;
					keys.Add(key);
				} else if(key >= KeyCode.Z){
					key = KeyCode.A;
				} else {
					key = key + 1;
				}
			}
			return key;
		}
	}
}

