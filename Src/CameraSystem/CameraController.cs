using Microsoft.Xna.Framework;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Text;

namespace TileBasedPlatformer.Src.CameraSystem
{
    internal class CameraController
    {
        public OrthographicCamera Camera { get; private set; }
        private List<ICameraTarget> targets = new List<ICameraTarget>();

        public CameraController(OrthographicCamera Camera)
        {
            this.Camera = Camera;
            this.Camera.Zoom = 1.8f;
        }

        public void AddTargets(params ICameraTarget[] targets)
        {
            foreach(var target in targets)
            {
                this.targets.Add(target);
            }
        }

        public void Update(float dt)
        {
            Vector2 avPos = Vector2.Zero;
            int numTargets = targets.Count;
            foreach(var target in this.targets)
            {
                avPos += target.Pos;
            }
            avPos /= numTargets;

            int easeScalar = 3;
            Camera.Position = Vector2.Lerp(Camera.Position, avPos - (Vector2)Constants.WorldSpace / 2, Math.Clamp(easeScalar * dt, 0.0f, 1.0f));
            Camera.Position = Vector2.Clamp(Camera.Position, // deduced clamp postions using the approach of considering extrema
                                           (Vector2)Constants.WorldSpace / (2 * Camera.Zoom) - (Vector2)Constants.WorldSpace / 2,
                                           (Vector2)Constants.WorldSpace - (Vector2)Constants.WorldSpace / 2 - ((Vector2)Constants.WorldSpace - (Vector2)Constants.WorldSpace / 2) / Camera.Zoom);
        }
    }
}
