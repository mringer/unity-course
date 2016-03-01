using System;
using UnityEngine;

namespace Game.Entities
{
	public class SpadeDoor : ExitBase
	{
		public SpadeDoor()
		{ 
			KeyType = typeof(SpadeKey);
			KeyCode = KeyCode.S;
			Name = this.GetType().Name;
			LockedText = "A locked "+ Name +" that can't be forced. There is an engraving of a Spade over it.";
			UnlockedText = "An unlocked "+ Name +" that may lead somewhere!";
		}
	}
}

