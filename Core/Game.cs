using Godot;
using ThumbCTRL.Core.Scripts;

namespace ThumbCTRL.Core
{
    public class Game : Node
    {
        private Node _scene;

        public override void _Ready()
        {
            _scene = Singleton.CurrentScene();
            Joystick.LetsSetConnectionStatuses();
            
            GD.Print("Autoload script loaded");
        }

        public override void _Input(InputEvent @event)
        {
            if(Input.IsActionPressed("ui_quit"))
            {
                Quit();
            }
        }
        
        public override void _Notification(int what)
        {
            if (what == MainLoop.NotificationWmQuitRequest)
                Quit();
        }

        public void Quit()
        {
            GetTree().Quit(); // default behavior
        }

        public void SwitchScene(string path)
        {
            CallDeferred(nameof(SwitchCurrentScene), path);
        }

        public void SwitchCurrentScene(string path)
        {
            _scene.Free();
            _scene = GD.Load<PackedScene>(path).Instance();
            
            Singleton.GetRoot().AddChild(_scene);
            GetTree().SetCurrentScene(_scene);
        }
    }
}