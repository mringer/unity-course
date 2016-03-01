using System;

namespace Game.Entities
{
	public class Ballroom : AreaBase
	{
		public Ballroom ()
		{
			Name = this.GetType().Name;
			Text = "You are in a dark "+ Name +".";
		}
	}
}

