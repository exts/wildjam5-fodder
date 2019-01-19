using Godot;

namespace ThumbCTRL.Core.Scripts.Levels
{
    public class Level1 : Level
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
            switch(name)
            {
                case "Trigger-Show-15x10":
                    Level.SetCellv(new Vector2(15, 10), 3);
                    break;
                case "Trigger-Hide-15x10":
                    Level.SetCellv(new Vector2(15, 10), -1);
                    break;
            }
        }

        public void ExitLevel()
        {
            EmitSignal(nameof(SwitchLevel), 2);
        }
    }
}