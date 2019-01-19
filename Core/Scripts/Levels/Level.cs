using Godot;

namespace ThumbCTRL.Core.Scripts.Levels
{
    public class Level : Node2D
    {
        [Signal] public delegate void GameOver();
        [Signal] public delegate void SwitchLevel();

        protected Player Player;
        protected Node2D Triggers;
        protected Node2D Checkpoints;

        protected Vector2 Checkpoint;

        public override void _Ready()
        {
            // setup nodes
            Player = GetNode<Player>("Player");
            Triggers = GetNode<Node2D>("Triggers");
            Checkpoints = GetNode<Node2D>("Checkpoints");

            // set the first checkpoint
            Checkpoint = GetCheckpoint("Checkpoint1");

            // setup default signal connections
            Player.Connect("HitWall", this, nameof(MoveToCheckpoint));
            Player.Connect("SetCheckpoint", this, nameof(SetCheckpoint));

            // move player to the first spawn point
            MoveToCheckpoint();
        }

        public void MoveToCheckpoint()
        {
            Player.Position = Checkpoint;
        }

        public Vector2 GetCheckpoint(string point)
        {
            return Checkpoints.GetNode<Area2D>(point).Position;
        }

        public void SetCheckpoint(string point) => Checkpoint = GetCheckpoint(point);
    }
}