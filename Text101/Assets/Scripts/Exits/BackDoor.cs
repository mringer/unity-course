using System;
using UnityEngine;
//using UnityEngine.UI;

namespace Game.Entities
{
	public class BackDoor : ExitBase
	{
		public BackDoor ()
		{
			Name = "Back";
			KeyType = null;
			Key = null;
			KeyCode = KeyCode.B;
			LockedText = ""; 
			UnlockedText = "A way back to the last area.";
		}
	}
}

