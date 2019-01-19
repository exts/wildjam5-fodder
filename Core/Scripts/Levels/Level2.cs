using Godot;

namespace ThumbCTRL.Core.Scripts.Levels
{
    public class Level2 : Level
    {
        protected TileMap Level;
        
        public override void _Ready()
        {
            base._Ready();

            // setup nodes
            Level = GetNode<TileMap>("Level");
            
            // player connection
            Player.Connect("ExitLevel", this, nameof(ExitLevel));
            Player.Connect("TriggerSet", this, nameof(HandleTriggers));
        }

        public void HandleTriggers(string name)
        {
        }

        public void ExitLevel()
        {
            EmitSignal(nameof(GameOver));
        }
    }
}