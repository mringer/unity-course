using System;

namespace Game.Entities
{
	public class Corridor : AreaBase
	{
		public Corridor ()
		{
			Name = this.GetType().Name;
			Text = "You are in a dark "+ Name +".";
		}
	}
}

