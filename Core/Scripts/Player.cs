using Godot;

namespace ThumbCTRL.Core.Scripts
{
    public class Player : KinematicBody2D
    {
        [Signal] public delegate void HitWall();
        [Signal] public delegate void ExitLevel();
        [Signal] public delegate void KeyCollected();
        [Signal] public delegate void PuzzleCollected();
        [Signal] public delegate void TriggerSet(string trigger);
        [Signal] public delegate void SetCheckpoint(string checkpoint);

        public int Keys;
        public int Puzzles;
        
        private const int Speed = 400;

        private Area2D _bodyArea;

        public override void _Ready()
        {
            _bodyArea = GetNode<Area2D>("BodyArea");
            _bodyArea.Connect("area_entered", this, nameof(OtherCollisionChecker));
            
            GD.Print(":::Player Script Loaded");
        }

        public override void _PhysicsProcess(float delta)
        {
            Joystick.LetsSetConnectionStatuses();

            if(Joystick.IsConnected(Joystick.Sticks.First))
            {
                var axis = Joystick.Axis(Joystick.Sticks.First);
                var direction = new Vector2();
                var xVelocity = Speed * Mathf.Abs(axis.x) * delta;
                var yVelocity = Speed * Mathf.Abs(axis.y) * delta;

                if(axis.x > Joystick.Deadzone)
                {
                    direction.x += xVelocity;
                }
                else if(axis.x < -Joystick.Deadzone)
                {
                    direction.x -= xVelocity;
                }

                if(axis.y > Joystick.Deadzone)
                {
                    direction.y += yVelocity;
                }
                else if(axis.y < -Joystick.Deadzone)
                {
                    direction.y -= yVelocity;
                }

                if(direction != Vector2.Zero)
                {
                    var collide = MoveAndCollide(direction);
                    HandleTilemapCollision(collide);
                }
            }
        }

        public void HandleTilemapCollision(KinematicCollision2D node)
        {
            if(node?.Collider is TileMap tilemap)
            {
                var offset = GetCellOffset(node);
                var (_, location) = GetCellLocation(offset, tilemap);

                switch(tilemap.Name)
                {
                    case "Level":
                        EmitSignal(nameof(HitWall));
                        break;
                    case "Keys":
                        if(Keys > 0)
                        {
                            Keys -= 1;
                            tilemap.SetCellv(location, -1);
                        }
                        break;
                    case "Puzzles":
                        if(Puzzles > 0)
                        {
                            Puzzles -= 1;
                            tilemap.SetCellv(location, -1);
                        }
                        break;
                }
            }    
        }
        
        public (int, Vector2) GetCellLocation(Vector2 offset, TileMap map)
        {
            //TODO: Figure out why the hell this only works when you run it from the level1 scene
            var location = map.WorldToMap(map.ToLocal(Position));
            location += offset;

            return (map.GetCellv(location), location);
        }
        
        public Vector2 GetCellOffset(KinematicCollision2D node)
        {
            var offset = new Vector2();
            
            if(node.Position.x > Position.x)
            {
                offset.x += 1;
            } else if(node.Position.x < Position.x)
            {
                offset.x -= 1;
            }
            
            if(node.Position.y > Position.y)
            {
                offset.y += 1;
            } else if(node.Position.y < Position.y)
            {
                offset.y -= 1;
            }

            return offset;
        }

        public void OtherCollisionChecker(Area2D node)
        {
            switch(node)
            {
                case Key key:
                    Keys += 1;
                    EmitSignal(nameof(KeyCollected));
                    key.QueueFree();
                    break;
                case Puzzle puzzle:
                    Puzzles += 1;
                    EmitSignal(nameof(PuzzleCollected));
                    puzzle.QueueFree();
                    break;
                case Checkpoint checkpoint:
                    EmitSignal(nameof(SetCheckpoint), checkpoint.Name);
                    break;
                case Exit exit:
                    EmitSignal(nameof(ExitLevel));
                    exit.QueueFree();
                    break;
                case Trigger trigger:
                    EmitSignal(nameof(TriggerSet), trigger.Name);
                    trigger.QueueFree();
                    break;
            }
        }
        
    }
}