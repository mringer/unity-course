using System;
using UnityEngine;

namespace Game.Entities
{
	public class HeartDoor : ExitBase
	{
		public HeartDoor()
		{ 
			KeyCode = KeyCode.H; 
			KeyType = typeof(HeartKey);
			Name = this.GetType().Name;
			LockedText = "A locked "+ Name +" that can't be forced. There is an engraving of a Heart over it.";
			UnlockedText = "An unlocked "+ Name +" that may lead somewhere!";
		}
	}
}

