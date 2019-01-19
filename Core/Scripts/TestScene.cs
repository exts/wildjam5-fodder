using Godot;

namespace ThumbCTRL.Core.Scripts
{
    public class TestScene : Node2D
    {
        private int Speed = 400;
        
        private Sprite _icon;
        
        public override void _Ready()
        {
            _icon = GetNode<Sprite>("icon");
        }

        public override void _Process(float delta)
        {
            Joystick.LetsSetConnectionStatuses();

            if(Joystick.IsConnected(Joystick.Sticks.First))
            {
                var axis = Joystick.Axis(Joystick.Sticks.First);
                var direction = new Vector2();
                var xVelocity = (Speed * Mathf.Abs(axis.x)) * delta;
                var yVelocity = (Speed * Mathf.Abs(axis.y)) * delta;
                
                if(axis.x > Joystick.Deadzone)
                {
                    direction.x += xVelocity;
                } else if(axis.x < -Joystick.Deadzone)
                {
                    direction.x -= xVelocity;
                }
                
                if(axis.y > Joystick.Deadzone)
                {
                    direction.y += yVelocity;
                } else if(axis.y < -Joystick.Deadzone)
                {
                    direction.y -= yVelocity;
                }

                _icon.Position += direction;

                if(direction != Vector2.Zero)
                {
//                    GD.Print(direction);
//                    GD.Print($"{(Speed - Speed * Mathf.Abs(axis.x))}, {(Speed - Speed * Mathf.Abs(axis.y))}");
                }
                if(Mathf.Abs(axis.x) > Joystick.Deadzone && Mathf.Abs(axis.y) > Joystick.Deadzone)
                {
                    GD.Print(axis);
                } else if(Mathf.Abs(axis.x) > Joystick.Deadzone)
                {
                    GD.Print($"x: {Mathf.Abs(axis.x)}");
                } else if(Mathf.Abs(axis.y) > Joystick.Deadzone)
                {
                    GD.Print($"y: {Mathf.Abs(axis.y)}");
                }
            }
        }
    }
}