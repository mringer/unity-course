using UnityEngine;

namespace Game.Entities
{
	public class MaidCostume : ItemBase, IItem
	{
		public MaidCostume() 
		{
			Name = "Maid's Costume";
			Text = "A sexy  "+ Name +".";
			InspectKey = KeyCode.U;
		}
	}
}

