using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

public class Camera
{
    public Matrix Transform { get; private set; }

    public Vector2 Position { get; private set; }
    private float shakeDuration = 0f;
    private float shakeIntensity = 0f;

    public void Follow(Vector2 position)
    {
        Position = position;
    }

    public void ApplyShake(float duration, float intensity)
    {
        shakeDuration = duration;
        shakeIntensity = intensity;
    }

    public void Update(GameTime gameTime)
    {
        if (shakeDuration > 0)
        {
            Position = Position + new Vector2((float)Math.Sin(gameTime.TotalGameTime.TotalSeconds * 50) * shakeIntensity, 0);
            shakeDuration -= (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        var transform = Matrix.CreateTranslation(-Position.X, -Position.Y, 0);
        Transform = transform;
    }
}
