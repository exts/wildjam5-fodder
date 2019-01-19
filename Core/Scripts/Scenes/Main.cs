using Godot;
using ThumbCTRL.Core.Scripts.Levels;

namespace ThumbCTRL.Core.Scripts.Scenes
{
    public class Main : Node2D
    {
        protected Node2D Reconnect;
        protected Vector2 LevelScale = new Vector2(.8f, .8f);
        protected Vector2 LevePosition = new Vector2(128, 52);

        protected Level Level;
        protected Node2D LevelContainer;

        public override void _Ready()
        {
            Reconnect = GetNode<Node2D>("Reconnect");
            Reconnect.Visible = false;

            LevelContainer = GetNode<Node2D>("Level");
            SwitchLevel(1);
        }

        public override void _Process(float delta)
        {
            Joystick.LetsSetConnectionStatuses();
            
            if(!Joystick.IsConnected(Joystick.Sticks.First) && !Reconnect.Visible)
            {
                Reconnect.Visible = true;
            }

            if(Joystick.IsConnected(Joystick.Sticks.First) && Reconnect.Visible)
            {
                Reconnect.Visible = false;
            }
        }

        private void LoadLevel(int level)
        {
            var resource = GD.Load<PackedScene>(LevelFactory.GetLevel(level));
            Level = resource.Instance() as Level;
            Level.Scale = LevelScale;
            Level.Position = LevePosition;
            LevelContainer.AddChild(Level);
            Level.Connect("GameOver", this, nameof(GameOver));
            Level.Connect("SwitchLevel", this, nameof(SwitchLevel));
        }

        public void SwitchLevel(int level)
        {
            if(Level != null)
                Level.QueueFree();
            
            CallDeferred("LoadLevel", level);
        }

        public void GameOver()
        {
            GD.Print("Thanks for playing!");
        }
    }
}