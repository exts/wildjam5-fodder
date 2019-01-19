using Godot;

namespace ThumbCTRL.Core
{
    public class Singleton
    {
        public static Singleton Instance => _instance ?? (_instance = new Singleton());
        private static Singleton _instance;

        private bool _setup;
        private Viewport _root;
        private Scenes Scenes = new Scenes();
        
        private Singleton()
        {
            Setup();
        }

        public void Setup()
        {
            if(_setup) return;
            _setup = true;
            
            _root = ((SceneTree) Engine.GetMainLoop()).GetRoot();
        }
        
        public static Viewport GetRoot()
        {
            return Instance._root;
        }
        
        public static Node CurrentScene()
        {
            var root = GetRoot();
            return root.GetChild(root.GetChildCount() - 1);
        }

        public static Game Game()
        {
            return GetRoot().GetNode<Game>("Game");
        }

        public static void SwitchScenes(Scenes.Scene scene)
        {
            Game().SwitchScene(Instance.Scenes.Path(scene));
        }
    }
}