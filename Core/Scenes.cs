using System.Collections.Generic;

namespace ThumbCTRL.Core
{
    public class Scenes
    {
        public enum Scene
        {
            Main,
            Menu,
            Thanks
        }

        private List<string> _paths = new List<string>
        {
            "Main", "Menu", "Thanks",
        };

        private const string Directory = "res://Data/Scenes";

        public string Path(Scene scene)
        {
            return $"{Directory}/{scene}.tscn";
        }
    }
}