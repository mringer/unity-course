using System;
using UnityEngine;

namespace Game.Entities
{
	public class ClubDoor : ExitBase
	{
		public ClubDoor()
		{ 
			Name = this.GetType().Name;
			KeyType = typeof(ClubKey);
			KeyCode = KeyCode.C;
			//Name = "Door";
			LockedText = "A locked "+ Name +" that can't be forced. There is an engraving of a Club over it.";
			UnlockedText = "An unlocked "+ Name +" that may lead somewhere!";
		}
	}
}

