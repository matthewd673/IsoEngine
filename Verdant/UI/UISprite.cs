﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Verdant.UI
{
    /// <summary>
    /// A UIElement that displays a sprite.
    /// </summary>
    public class UISprite : UIElement
    {
        // The RenderObject to draw.
        public RenderObject Sprite { get; set; }
        // The index of the RenderObject to draw (only use if it is an Animation or SpriteSheet).
        public int SpriteIndex { get; set; } = 0;

        private int _repeat = 1;
        // The number of times the RenderObject should repeat.
        public int Repeat
        {
            get { return _repeat; }
            set
            {
                _repeat = value;
                if (_repeatVertical)
                {
                    AbsoluteWidth = Sprite.Width;
                    AbsoluteHeight = Sprite.Height * Repeat;
                }
                else
                {
                    AbsoluteWidth = Sprite.Width * Repeat;
                    AbsoluteHeight = Sprite.Height * Repeat;
                }
            }
        }

        private bool _repeatVertical = false;
        // Determines if the RenderObject repeats vertically or horizontally.
        public bool RepeatVertical
        {
            get { return _repeatVertical; }
            set
            {
                _repeatVertical = value;
                if (_repeatVertical)
                {
                    AbsoluteWidth = Sprite.Width;
                    AbsoluteHeight = Sprite.Height * Repeat;
                }
                else
                {
                    AbsoluteWidth = Sprite.Width * Repeat;
                    AbsoluteHeight = Sprite.Height * Repeat;
                }
            }
        }

        /// <summary>
        /// Initialize a new UISprite.
        /// </summary>
        /// <param name="sprite">The sprite to draw.</param>
        /// <param name="position">The position of the sprite on the UI.</param>
        public UISprite(RenderObject sprite, Vec2 position)
            : base(position, sprite.Width, sprite.Height)
        {
            if (sprite != RenderObject.None)
            {
                if (sprite.GetType() == typeof(Animation) ||
                    sprite.GetType().IsSubclassOf(typeof(Animation)))
                {
                    Sprite = ((Animation)sprite).Copy();
                }
                else
                {
                    Sprite = sprite;
                }
            }
        }

        /// <summary>
        /// Initialize a new UISprite.
        /// </summary>
        /// <param name="sprite">The sprite to draw.</param>
        /// <param name="position">The position of the sprite on the UI.</param>
        /// <param name="width">The width of the sprite.</param>
        /// <param name="height">The height of the sprite.</param>
        public UISprite(RenderObject sprite, Vec2 position, int width, int height)
            : base(position, width, height)
        {
            if (sprite != RenderObject.None)
            {
                if (sprite.GetType() == typeof(Animation) ||
                    sprite.GetType().IsSubclassOf(typeof(Animation)))
                {
                    Sprite = ((Animation)sprite).Copy();
                }
                else
                {
                    Sprite = sprite;
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Sprite.DrawIndex(spriteBatch,
                             new Rectangle(
                                 (int)((InnerPosition.X + Padding.Left) * Renderer.Scale),
                                 (int)((InnerPosition.Y + Padding.Top) * Renderer.Scale),
                                 (int)(AbsoluteWidth * Renderer.Scale),
                                 (int)(AbsoluteHeight * Renderer.Scale)),
                             SpriteIndex);
        }

    }
}
