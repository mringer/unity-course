using System;
using UnityEngine;

namespace Game.Entities
{
	public class OpenDoor : ExitBase
	{
		public OpenDoor()
		{ 
			Key = null;
			KeyType = null;
			Name = "Door";
			KeyCode = KeyCode.O;
			LockedText = "This should never showup!";
			UnlockedText = "An unlocked "+ Name +" that may lead somewhere!";
		}
	}
}

