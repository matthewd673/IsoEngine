using Microsoft.Xna.Framework.Graphics;
using Verdant;

namespace TransformDemo;

public class Crate : Entity
{
    public Crate(Vec2 position)
        : base(position, Resources.Crate, 32, 32)
    {
        // Empty
    }

    public void AddTransform(Transform transform)
    {
        TransformStates.Add(transform);
    }

    public bool RemoveTransform(Transform transform)
    {
        return TransformStates.Remove(transform);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        base.Draw(spriteBatch);

        ((PlayScene)Manager.Scene).SetTransformInfo(BaseTransform.Position.X,
                                                    BaseTransform.Position.Y,
                                                    BaseTransform.Width,
                                                    BaseTransform.Height,
                                                    BaseTransform.Angle
            );
    }
}
