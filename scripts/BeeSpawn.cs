using Godot;
using System;
using System.Runtime.CompilerServices;
using System.Threading;

public partial class BeeSpawn : Node
{
	[Export] public PackedScene BeeScene { get; set; }
    [Export] public StaticBody2D SpawnArea { get; set; }
    [Export] public Node2D Flowers { get; set; }

    [Signal] public delegate void BirthedBeeEventHandler();
	//private Timer spawnTimer;

// dependency injection. Whatever creates the mob should also be responsible for making sure it's wired up correctly (think factory pattern). That way your mob doesn't have to go out of scope and dig around the scene tree.
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		//spawnTimer = new Timer();
		//addChild(spawnTimer);

		// Connect the timer signal to the method that spawns bees
		//spawnTimer.Connect("timeout", this, nameof(OnSpawnTimeout));

		//set timer to run every few seconds
		//spawnTimer.WaitTime = 5.0f;
		//spawnTimer.Autostart = true;
	}
    public void _on_timer_timeout() 
    {
        SpawnBee();
    }

    private void SpawnBee() 
    {
        if (BeeScene != null && SpawnArea != null)
        {
            //get box of spawn area
            var collisionShape = SpawnArea.GetNode<CollisionShape2D>("BeeSpawner_col");
            //var collisionShape_x = collisionShape.Shape.Size.x;
            var bee = (LegallyDistinctBee)BeeScene.Instantiate();

            if (Flowers != null) 
            {
                bee.SetFlowers(Flowers);
            }
            EmitSignal(SignalName.BirthedBee);
            AddChild(bee);
        }
    }
}