using System.Collections;
using System.Collections.Generic;
using Godot;

namespace ThumbCTRL.Core
{
    public class Joystick
    {
        public enum Sticks
        {
            First,
            Second,
            Third,
            Fourth
        }

        public const float Deadzone = 0.2f; 
        
        public static Joystick Instance => _instance ?? (_instance = new Joystick());
        private static Joystick _instance;

        private Dictionary<int, JoystickDevice> _connected = new Dictionary<int, JoystickDevice>();
        private List<int> _devices = new List<int>{0, 1, 2, 3};

        private Joystick()
        {
            SetupDefaultDevices();
        }

        public void SetupDefaultDevices()
        {
            foreach(var device in _devices)
            {
                _connected[device] = new JoystickDevice();
            }
        }

        public static void LetsSetConnectionStatuses()
        {
            Instance.SetConnectionStatuses();
        }
        
        public void SetConnectionStatuses()
        {
            var pads = fixPads(Input.GetConnectedJoypads());
            foreach(var device in _devices)
            {
                var current = _connected[device];
                var now = pads.Contains(device);
                if(current.Connected == now) continue;

                var currentDeviceName = Input.GetJoyName(device);
                var deviceName = currentDeviceName.Empty() ? _connected[device].Name : currentDeviceName;  
                _connected[device] = _connected[device].Copy(deviceName, now);
                
                GD.Print($"[{device}] {_connected[device].Name} {(now ? "Connected" : "Disconnected")}");
            }
        }

        private List<int> fixPads(IEnumerable pads)
        {
            var items = new List<int>();
            foreach(var pad in pads)
            {
                items.Add((int) pad);
            }

            return items;
        }

        public static bool IsConnected(Sticks device)
        {
            return Instance.Connected((int) device);
        } 
       
        public bool Connected(int device)
        {
            return _connected.ContainsKey(device) && _connected[device].Connected;
        }

        public static Vector2 Axis(Sticks device)
        {
            var current = (int) device;
            return new Vector2(
                Input.GetJoyAxis(current, (int) JoystickList.Axis0),
                Input.GetJoyAxis(current, (int) JoystickList.Axis1)
            );
        }
    }

    public struct JoystickDevice
    {
        public string Name;
        public bool Connected;

        public JoystickDevice(string name, bool connected)
        {
            Name = name;
            Connected = connected;
        }

        public JoystickDevice Copy(string name, bool connected)
        {
            return new JoystickDevice(name, connected);
        }
    }
}