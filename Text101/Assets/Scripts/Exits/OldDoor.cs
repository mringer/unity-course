using System;
using UnityEngine;

namespace Game.Entities
{
	public class OldDoor : ExitBase
	{
		public OldDoor()
		{ 
			KeyType = typeof(RustyKey);
			Name = "Old Door";
			KeyCode = KeyCode.R;
			LockedText = "A locked "+ Name +" with a Rusty lock that can't be picked.";
			UnlockedText = "An unlocked "+ Name +" that may lead somewhere!";
		}
	}
}

