using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Entities
{
	public abstract class AreaBase 
	{
		public string Name {get; protected set;}
		protected string Text {get; set;}

		private List<IItem> _ItemBase = new List<IItem>();
		public List<IItem> Items {get{return _ItemBase;} set{_ItemBase = value;}}

		private List<ExitBase> _DoorBase = new List<ExitBase>();
		public List<ExitBase> Exits {get{return _DoorBase;} set{_DoorBase = value;}}

		public bool DeadEnd {get; set;}

		private IItem _inspectedItem = null;
		private ExitBase _inspectedDoor = null;

		protected delegate void AreaState(Text textHandle, ref AreaBase area, ref AreaState areaState, ref List<IItem> Inventory, ref IItem inspectedItem, ref ExitBase inspectedDoor);
		private AreaState _areaState;

		public AreaBase ()
		{
			_areaState = WanderAreaState;
		}

		/// <summary>
		/// Check for user actions and update the inventory, door locks and area state.
		/// </summary>
		/// <returns>The new state.</returns>
		/// <param name="Inventory">A reference to the player Inventory.</param>
		public virtual AreaBase UpdateState (Text textHandle, ref List<IItem> Inventory)
		{
			AreaBase area = this;
			_areaState(textHandle, ref area, ref _areaState, ref Inventory, ref _inspectedItem, ref _inspectedDoor);
			return area;
		}

		private void WanderAreaState (Text textHandle, ref AreaBase area, ref AreaState areaState, ref List<IItem> Inventory, ref IItem inspectedItem, ref ExitBase inspectedDoor)
		{
			if (area.GetType () != typeof(Freedom)) {
				//Display Acitons Menu
				var sb = new StringBuilder ("You are wandering in " + Text + "\n");
				sb.AppendLine ("Here you can see:\n");

				foreach (var door in Exits) {
					sb.Append (door.Text + " ");
				}
				sb.AppendLine ("\nYou can:\n");
				sb.AppendLine ("Press [I] to view your Invenrtory");
				if (Input.GetKeyDown (KeyCode.I)) {
					this._areaState = InventoryState;
				}

				sb.AppendLine ("Press [S] to Search the room for useful items.");
				if (Input.GetKeyDown (KeyCode.S)) {
					this._areaState = SearchItemsState;
				}

				sb.AppendLine ("Press [E] to check the Exits.");
				if (Input.GetKeyDown (KeyCode.E)) {
					this._areaState = CheckExitsState;
				}
				textHandle.text = sb.ToString ();
			} else {
				textHandle.text = area.Text;
				if (Input.GetKeyDown (KeyCode.Return)) { area = null; }
			}

		}
	
		private void InventoryState (Text textHandle, ref AreaBase area, ref AreaState areaState, ref List<IItem> inventory, ref IItem inspectedItem, ref ExitBase inspectedDoor)
		{
			var sb = new StringBuilder (Text);
			if (inventory.Any()) {
				sb.AppendLine("Your Inventory contains:");
				var keys = new List<KeyCode>();
				foreach (var item in inventory) {
					KeyCode takeKey = keys.FindUniqueKey(item.InspectKey);
					sb.AppendLine("Press ["+ Enum.GetName(typeof(KeyCode), takeKey) +"] to Inspect the " + item.Name);
					if(Input.GetKeyDown((KeyCode)takeKey)) { _areaState = InspectInventoryItemState; inspectedItem = item; }
				}
			} else {
				sb.AppendLine("Your Inventory is empty!");
			}

			sb.AppendLine("Press ["+ Enum.GetName(typeof(KeyCode), KeyCode.Return) +"] to Return to wandering the " + this.Name);
			if(Input.GetKeyDown(KeyCode.Return)) { areaState = WanderAreaState; inspectedItem = null; inspectedDoor = null;}

			textHandle.text = sb.ToString();
		}

		private void SearchItemsState (Text textHandle, ref AreaBase area, ref AreaState areaState, ref List<IItem> inventory, ref IItem inspectedItem, ref ExitBase inspectedDoor)
		{
			//List Items in room and go back
			var sb = new StringBuilder();
			if (area.Items.Any ()) {
				foreach (var item in Items) { sb.Append(item+" "); }
				sb.AppendLine ("You have found :");
				var keys = new List<KeyCode>();
				foreach (var item in Items) {
					KeyCode takeKey = keys.FindUniqueKey(item.InspectKey);
					sb.AppendLine("Press ["+ Enum.GetName(typeof(KeyCode), takeKey) +"] to Inspect the " + item.Name);
					if(Input.GetKeyDown((KeyCode)takeKey)) { _areaState = InspectItemState; inspectedItem = item; }
				}
			}
			else{
				sb.AppendLine("There doesn't seem to be anything useful here.");
			}

			sb.AppendLine("Press ["+ Enum.GetName(typeof(KeyCode), KeyCode.Return) +"] to Return to wandering the " + this.Name);
			if(Input.GetKeyDown(KeyCode.Return)) { areaState = WanderAreaState; inspectedItem = null; inspectedDoor = null;}

			textHandle.text = sb.ToString();
		}

		private void InspectItemState (Text textHandle, ref AreaBase area, ref AreaState areaState, ref List<IItem> inventory, ref IItem inspectedItem, ref ExitBase inspectedDoor)
		{
			var sb = new StringBuilder (Text);
			if(inspectedItem != null) {
				sb.AppendLine(inspectedItem.Text);
			}

			sb.AppendLine("Press ["+ Enum.GetName(typeof(KeyCode), KeyCode.T) +"] to add the " + inspectedItem.Name + " to your Inventory.");
			if(Input.GetKeyDown(KeyCode.T)) 
			{ 
				inventory.Add(inspectedItem);
				var itemType = inspectedItem.GetType();
				area.Items.RemoveAt(area.Items.FindIndex(x=>x.GetType() == itemType));
				areaState = SearchItemsState; inspectedItem = null;
				return;
			}

			sb.AppendLine("Press ["+ Enum.GetName(typeof(KeyCode), KeyCode.Return) +"] to put the " + inspectedItem.Name + " back.");
			if(Input.GetKeyDown(KeyCode.Return)) { areaState = SearchItemsState; inspectedItem = null; return; }

			textHandle.text = sb.ToString();
		}

		private void InspectInventoryItemState (Text textHandle, ref AreaBase area, ref AreaState areaState, ref List<IItem> inventory, ref IItem inspectedItem, ref ExitBase inspectedDoor)
		{
			var sb = new StringBuilder (Text);

			sb.AppendLine (inspectedItem.Text);
		
			sb.AppendLine ("Press [" + Enum.GetName (typeof(KeyCode), KeyCode.D) + "] to Drop the " + inspectedItem.Name + " to in the " + area.Name + ".");
			if (Input.GetKeyDown (KeyCode.D)) { 
				area.Items.Add (inspectedItem);
				var itemType = inspectedItem.GetType();
				inventory.RemoveAt(inventory.FindIndex (x=>x.GetType() == itemType));
				areaState = InventoryState;
				inspectedItem = null;
				return;
			}

			sb.AppendLine("Press ["+ Enum.GetName(typeof(KeyCode), KeyCode.Return) +"] to put the " + inspectedItem.Name + " back.");
			if(Input.GetKeyDown(KeyCode.Return)) { areaState = WanderAreaState; inspectedItem = null; return;}

			textHandle.text = sb.ToString();
		}

		private void CheckExitsState (Text textHandle, ref AreaBase area, ref AreaState areaState, ref List<IItem> inventory, ref IItem inspectedItem, ref ExitBase inspectedDoor)
		{
			//List Items in room and go back
			var sb = new StringBuilder (Text);
			sb.AppendLine ("You can see these potential Exits:");
			var keyOptions = new List<KeyCode> ();
			foreach (var exit in Exits) {
				KeyCode exit_key = keyOptions.FindUniqueKey (exit.KeyCode);
				if (exit.IsLocked) {
					sb.AppendLine ("Press [" + Enum.GetName (typeof(KeyCode), exit_key) + "] to Inspect the " + exit.Name);
					if (Input.GetKeyDown ((KeyCode)exit_key)) { _areaState = InspectExitState; inspectedDoor = exit; }
				} else {
					sb.AppendLine ("Press [" + Enum.GetName (typeof(KeyCode), exit_key) + "] to Exit through the " + exit.Name + ".");
					if (Input.GetKeyDown (exit_key)) { area = exit.Fwd; return; } // Go to the next area.
				}
			}

			sb.AppendLine("Press ["+ Enum.GetName(typeof(KeyCode), KeyCode.Return) +"] to Return to wandering the " + this.Name);
			if(Input.GetKeyDown(KeyCode.Return)) { areaState = WanderAreaState; inspectedItem = null; inspectedDoor = null;}

			textHandle.text = sb.ToString();
		}

		private void InspectExitState (Text textHandle, ref AreaBase area, ref AreaState areaState, ref List<IItem> inventory, ref IItem inspectedItem, ref ExitBase inspectedDoor)
		{
			var sb = new StringBuilder (Text);
			sb.AppendLine (inspectedDoor.Text);
			var keyType = inspectedDoor.KeyType;
			if (inventory.Any (x => x.GetType () == keyType)) { // we have a key for this door.
				var keyIndex = inventory.FindIndex (x => x.GetType () == keyType);
				sb.AppendLine ("Press [" + Enum.GetName (typeof(KeyCode), KeyCode.U) + "] to use the " + inventory [keyIndex].Name + " in the " + inspectedDoor.Name + ".");
				if (Input.GetKeyDown (KeyCode.U)) { 
					inspectedDoor.Key = inventory [keyIndex];
					inventory.RemoveAt(keyIndex);
					areaState = CheckExitsState;
					inspectedDoor = null;
					return;
				}
			}

			//if the door is unlocked
			if (!inspectedDoor.IsLocked) {
				sb.AppendLine ("Press [" + Enum.GetName (typeof(KeyCode), KeyCode.E) + "] to Exit through the " + inspectedDoor.Name + ".");
				if (Input.GetKeyDown (KeyCode.E)) {
					area = inspectedDoor.Fwd;
					inspectedDoor = null;
					return;
				}
			}

			sb.AppendLine ("Press [" + Enum.GetName (typeof(KeyCode), KeyCode.Return) + "] to put the " + inspectedDoor.Name + " back.");
			if (Input.GetKeyDown (KeyCode.Return)) {
				areaState = WanderAreaState;
				inspectedDoor = null;
				return;
			}

			textHandle.text = sb.ToString ();
		}

	}
}
