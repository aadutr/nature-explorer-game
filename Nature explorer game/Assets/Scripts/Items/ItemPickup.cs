using UnityEngine;

public class ItemPickup : Interactable {

	public Item item;	// Item to put in the inventory on pickup

	// When the player interacts with the item
	public override void Interact()
	{
		PickUp();	// Pick it up!
	}

	// Pick up the item
	void PickUp ()
	{
		Debug.Log("Picking up " + item.name);
		bool wasPickedUp = Inventory.instance.Add(item);	// Add to inventory

		// If successfully picked up
		if (wasPickedUp)
			this.Disable();	// Turn off interactability
	}

    public override string GetDescription() {
        if (this.isEnabled()) return "Press [E] to pick " + item.name + ".";
        return "";
    }

}