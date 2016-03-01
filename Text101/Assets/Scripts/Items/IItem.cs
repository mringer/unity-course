using System;
using UnityEngine;

namespace Game.Entities
{
	public interface IItem {
		string Name { get; set;}
		string Text {get; set;}
		//Action Take {get; set;}
		KeyCode InspectKey {get; set;}
	}

	public interface IKey {
		string Name { get; set;}
		string Text {get; set;}
		//Action Take {get; set;}
		//Action Use {get; set;}
	}

	public class ItemBase {
		public string Name { get; set;}
		public string Text {get; set;}
		public KeyCode InspectKey {get; set;}
	}
}

