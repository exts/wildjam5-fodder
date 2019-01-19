using Godot;

namespace ThumbCTRL.Core.Scripts
{
    public class Main : Node2D
    {
        protected Node2D Reconnect;

        public override void _Ready()
        {
            Reconnect = GetNode<Node2D>("Reconnect");
            Reconnect.Visible = false;
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
    }
}