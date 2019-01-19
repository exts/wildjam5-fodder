using Godot;

namespace ThumbCTRL.Core.Scripts.Scenes
{
    public class Menu : Node2D
    {
        protected Button _start;
        protected LinkButton _github;
        protected LinkButton _twitter;
        protected LinkButton _twitter2;

        public override void _Ready()
        {
            // setup objects
            _start = GetNode<Button>("Start");
            _github = GetNode<LinkButton>("Github");
            _twitter = GetNode<LinkButton>("Twitter");
            _twitter2 = GetNode<LinkButton>("Twitter2");
            
            // connect signals
            _start.Connect("pressed", this, nameof(StartGame));
            _github.Connect("pressed", this, nameof(Github));
            _twitter.Connect("pressed", this, nameof(Twitter));
            _twitter2.Connect("pressed", this, nameof(Twitter2));
        }

        public void StartGame()
        {
            Singleton.SwitchScenes(Core.Scenes.Scene.Main);
        }

        public void Github() => OS.ShellOpen("https://github.com/exts");
        public void Twitter() => OS.ShellOpen("https://twitter.com/G4MR");
        public void Twitter2() => OS.ShellOpen("https://twitter.com/IMG4MR");
    }
}