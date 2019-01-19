using Godot.Collections;

namespace ThumbCTRL.Core
{
    public class LevelFactory
    {
        private static Dictionary<int, string> _levels = new Dictionary<int, string>
        {
            {1, "Level1"},
            {2, "Level2"},
        };

        private static string LevelPath = "res://Data/Levels";

        public static string GetLevel(int level)
        {
            return $"{LevelPath}/{_levels[level]}.tscn";
        }
    }
}