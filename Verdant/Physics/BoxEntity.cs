﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Verdant.Physics;

/// <summary>
/// An Entity with a Box body.
/// </summary>
public class BoxEntity : PhysicsEntity
{
    /// <summary>
    /// Initialize a new BoxEntity.
    /// </summary>
    /// <param name="sprite">The Entity's sprite.</param>
    /// <param name="position">The position of the center of the Entity.</param>
    /// <param name="width">The width of the Box.</param>
    /// <param name="height">The height of the Box.</param>
    /// <param name="mass">The mass of the Entity's Body. 0 = infinite mass.</param>
    public BoxEntity(RenderObject sprite, Vec2 position, int width, int height, float mass)
        : base(sprite, position, width, height, mass)
    {
        float x1 = position.X;
        float y1 = position.Y;
        float x2 = x1;
        float y2 = y1 + height;
        float r = width / 2;

        Vec2 top = new Vec2(x1, y1);
        Vec2 bottom = new Vec2(x2, y2);

        Vec2 recVec1 = bottom + (bottom - top).Unit().Normal() * r;
        Vec2 recVec2 = top + (bottom - top).Unit().Normal() * r;

        Rectangle rectangle1 = new Rectangle(recVec1.X, recVec1.Y, recVec2.X, recVec2.Y, 2 * r);
        rectangle1.CalculateVertices();

        Components = new Shape[] { rectangle1 };

        Inertia = Mass * (
            (float)Math.Pow(2 * rectangle1.Width, 2) +
            (float)Math.Pow(height + 2 * rectangle1.Width, 2)
            ) / 12;
    }

    /// <summary>
    /// Move according to WASD input.
    /// </summary>
    public void SimpleInput()
    {
        bool up = InputHandler.KeyboardState.IsKeyDown(Keys.W);
        bool down = InputHandler.KeyboardState.IsKeyDown(Keys.S);
        bool left = InputHandler.KeyboardState.IsKeyDown(Keys.A);
        bool right = InputHandler.KeyboardState.IsKeyDown(Keys.D);

        Vec2 dir = Components[0].Dir;
        if (up) Acceleration = dir * -Speed;
        if (down) Acceleration = dir * Speed;
        if (!up && !down) Acceleration = new Vec2(0, 0);

        if (left) AngleSpeed -= 0.3f;
        if (right) AngleSpeed += 0.3f;
    }

    public override void Move()
    {
        base.Move();

        AngleSpeed *= 1 - AngleFriction;
        ((Rectangle)Components[0]).Angle += AngleSpeed;
        ((Rectangle)Components[0]).CalculateVertices();
    }
}

