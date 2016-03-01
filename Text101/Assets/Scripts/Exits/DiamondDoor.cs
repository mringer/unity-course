using System;
using UnityEngine;

namespace Game.Entities
{
	public class DiamondDoor : ExitBase
	{
		public DiamondDoor()
		{ 
			Name = this.GetType().Name;
			KeyType = typeof(DiamondKey);
			KeyCode = KeyCode.D;
			LockedText = "A locked "+ Name +" that can't be forced. There is an engraving of a Diamond over it.";
			UnlockedText = "An unlocked "+ Name +" that may lead somewhere!";
		}
	}
}

