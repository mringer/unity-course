using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Entities
{
	public abstract class ExitBase {
		public string Name {get; set;}
		public string LockedText {get; set;}
		public string UnlockedText {get; set;}
		public string Text { get { return  (IsLocked) ? LockedText : UnlockedText; } }

		public Type KeyType {get; protected set;}
		public IItem Key {set; private get;}
		public bool IsLocked { get { return (KeyType == null || Key != null) ? false : true; } }
		public AreaBase Fwd {get; set;}
		//public AreaBase Back {get; set;}
		public KeyCode KeyCode {get; set;}

		/// <summary>
		/// Initializes a new instance of the <see cref="Game.Entities.Exit"/> class.
		/// </summary>
		/// <param name="fwd">The next room in the graph</param>
		/// <param name="back">The previous room in the graph</param>

		/// <summary>
		/// Open the door and enter the adjacent area.
		/// </summary>
		/// <param name="area">The area from which we are opening the door.</param>
//		public AreaBase Exit (AreaBase area)
//		{
//			// trying to open a locked door
//			if (IsLocked) {
//				return area;
//			} else {
//				return Fwd;
//			}
////			//Check which side of the door we are on.
////			if (area.Name == Fwd.Name && area.Name != Back.Name) {
////				return Back;
////			} else if (area.Name == Back.Name && area.Name != Fwd.Name) {
////				return Fwd;
////			} else { //Edge case if there is a door loop.
////				return area;
////			}
//		}
	}
}

