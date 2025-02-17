using Godot;
using System;
using System.Collections.Generic;

public partial class LegallyDistinctBee : Node2D
{
	[Export] public float MoveSpeed = 30f; // Movement speed of the AI
	[Export] public float StopDistance = 1f; // Distance at which the AI stops moving
	[Export] public float RandomTargetCooldown = 2f; // Time before choosing a new random target
	[Export] public Node2D Flowers { get; set; }
	[Export] public Node2D This { get; set; }
	[Export] public Node2D CharBod { get; set; }
	[Export] public AnimationPlayer AnimPlayer { get; set; }
	[Export] public Timer PollinationTimer { get; set; }

	[Signal] public delegate void HitByBiffyEventHandler();
	[Signal] public delegate void StartedHarvestEventHandler();
	[Signal] public delegate void EndedHarvestEventHandler();
	[Signal] public delegate void ReturnedToHiveEventHandler();


	private List<Node2D> flowerNodes = new List<Node2D>();
	private Vector2 targetPosition;
	private Vector2 selfPosition;
	private double cooldownTimer = 0;
	RandomNumberGenerator randFlower = new RandomNumberGenerator();
	Node2D randomItem;
	int randomIndex;
	Node2D randomFlower;

	
	// Enum for states
	private enum BeeState { Moving }
	private BeeState currentState = BeeState.Moving;
	//Bee should delete itself when it re-enters the BeeSpawn region

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		PopulateFlowerList();
		// Pick an initial target
		randomFlower = GetRandFlower(flowerNodes);
		//pick random index
		//GD.Print(flowerNodes);
		PlayAnimation("walk1");
	}

	public override void _PhysicsProcess(double delta)
	{
		selfPosition = This.GlobalPosition;
		if (randomFlower != null)
		{
			Move2Flower(randomFlower);
		} else {
			GetRandFlower(flowerNodes);
		}
	}

	public void SetFlowers(Node2D flowers)
	{
		Flowers = flowers; //Inject flowers into bee like drugs or IV drippage
	}

	//populates list with all child nodes of Flowers
	private void PopulateFlowerList()
	{
		flowerNodes.Clear(); //clears at the start. Just in case.
		if (Flowers != null)
		{
			//Selects random flowers
			foreach (Node2D child in Flowers.GetChildren())
			{
				flowerNodes.Add(child);
				//GetFlowersFromList();
			}
			GD.Print("Random Flower: " + GetRandFlower(flowerNodes).Name);

		}
	}

	private Node2D GetRandFlower(List<Node2D> flowerNodes)//Selects a random flower, use this to send the 'Bee' to said flower
	{
		int randomIndex = randFlower.RandiRange(0, flowerNodes.Count - 1);
		randomItem = flowerNodes[randomIndex];
		return randomItem;
	}

	private void GetFlowersFromList() 
	{
		foreach (Node2D flower in flowerNodes)
		{
			GD.Print($"Flower Name: {flower.Name}");
			GD.Print("Flower Position: " + flower.GlobalPosition);
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void Move2Flower(Node2D flower) 
	{
		if (CharBod is CharacterBody2D character)
		{
			Vector2 direction = (flower.GlobalPosition - CharBod.GlobalPosition).Normalized();
			character.Velocity = direction * MoveSpeed;
			character.MoveAndSlide();
		}
	else
		{
			GD.PrintErr("CharBod is not a CharacterBody2D!");
		}
	}

	
	private void PlayAnimation(string animationName)
	{
		if (AnimPlayer != null && AnimPlayer.HasAnimation(animationName))
		{
			AnimPlayer.Play(animationName);
		}
		else
		{
			GD.PrintErr("AnimationPlayer is missing or animation not found!");
		}
	}

	private void Pollinate(Node2D flower)
	{
		
	}
}
