using Godot;
using System;
using System.Dynamic;

public partial class AddBee2ui : Node
{
	[Export] public PackedScene UserInterface { get; set; }
	[Export] public PackedScene BeeSpawner { get; set; }

	

	private ItemList beeList;  // The UI element to display bees


	//Pass bee from BeeSpawner to UserInterface

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
			// Assuming the ListBox (or ItemList) is a child of the UserInterface scene
			//beeList = UserInterface.GetNode<ItemList>("BeeList");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
