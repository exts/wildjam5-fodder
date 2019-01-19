using Godot;

namespace ThumbCTRL.Core.Scripts.Scenes
{
    public class Thanks : Node2D
    {
        protected Button _quit;
        protected LinkButton _github;
        protected LinkButton _github2;
        protected LinkButton _twitter;
        protected LinkButton _twitter2;

        public override void _Ready()
        {
            // setup objects
            _quit = GetNode<Button>("Quit");
            _github = GetNode<LinkButton>("Github");
            _github2 = GetNode<LinkButton>("Github2");
            _twitter = GetNode<LinkButton>("Twitter");
            _twitter2 = GetNode<LinkButton>("Twitter2");
            
            // connect signals
            _quit.Connect("pressed", this, nameof(QuitGame));
            _github.Connect("pressed", this, nameof(Github));
            _github2.Connect("pressed", this, nameof(Github2));
            _twitter.Connect("pressed", this, nameof(Twitter));
            _twitter2.Connect("pressed", this, nameof(Twitter2));
        }

        public void QuitGame()
        {
            Singleton.Game().Quit();
        }

        public void Github() => OS.ShellOpen("https://github.com/exts");
        public void Github2() => OS.ShellOpen("https://github.com/exts/wildjam5-fodder");
        public void Twitter() => OS.ShellOpen("https://twitter.com/IMG4MR");
        public void Twitter2() => OS.ShellOpen("https://twitter.com/G4MR");
    }
}