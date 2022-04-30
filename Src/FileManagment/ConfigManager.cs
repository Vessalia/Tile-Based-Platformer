using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;
using TileBasedPlatformer.Src.Core;

namespace TileBasedPlatformer.Src.FileManagment
{
    public class ConfigManager
    {
        private ConfigData data;

        private static readonly FileManager<ConfigData> configFileManager = new FileManager<ConfigData>(Constants.configPath);

        public ConfigManager(ConfigData data)
        {
            this.data = data;
        }

        public void SaveKeyBinding(string name, Keys key)
        {
            for (int i = 0; i < data.KeyNames.Count; i++)
            {
                if (name == data.KeyNames[i])
                {
                    data.KeyBindings[i] = key;
                }
            }

            configFileManager.WriteData(data);
        }

        public Keys GetKeyBinding(string name)
        {
            for (int i = 0; i < data.KeyNames.Count; i++)
            {
                if (name.Equals(data.KeyNames[i]))
                {
                    return data.KeyBindings[i];
                }
            }

            throw new Exception("key binding does not exist, check to see if it has been initialized");
        }

        public void SetVolume(int volume)
        {
            data.Volume = volume;

            configFileManager.WriteData(data);
        }
    }
}
