using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace TileBasedPlatformer.Src.FileManagment
{
    public class ConfigData
    {
        public int Volume { get; set; }
        public List<string> KeyNames { get; set; }
        public List<Keys> KeyBindings { get; set; }

        public int Count
        {
            get { return KeyNames.Count; }
        }

        public ConfigData()
        {
            Volume = 100;

            KeyNames = new List<string>();
            KeyBindings = new List<Keys>();
        }
    }
}
