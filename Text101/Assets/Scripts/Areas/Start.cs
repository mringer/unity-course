using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Entities
{
	public class Start : AreaBase
	{
		private List<IItem> _inventory;
		private AreaBase _startArea;
		public AreaBase StartArea { get { return _startArea; } set { _startArea = value;}} 

		public Start ()
		{

		}

		public Start(AreaBase startArea = null)
		{
			_startArea = startArea;

			Text = "Welcome to the prisoner's escape!\n\n"
					+"Press the [space] bar to get started.";
		}

		public override AreaBase UpdateState(Text textHandle, ref List<IItem> Inventory)
		{
			textHandle.text =	"Welcome to the prisoner's escape!\n\n"
								+"Press the [space] bar to get started.";

			return (Input.GetKeyDown(KeyCode.Space)) ? _startArea : this;
		}
	}
}

