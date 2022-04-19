using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace TileBasedPlatformer.Src.CameraSystem
{
    internal interface ICameraTarget
    {
        public Vector2 Pos { get; }
    }
}
