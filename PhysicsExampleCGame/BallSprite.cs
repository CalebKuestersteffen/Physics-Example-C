﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace PhysicsExampleCGame
{
    /// <summary>
    /// A class representing a ball that bounces off the edge of the screen
    /// </summary>
    public class BallSprite
    {
        // private variables
        Texture2D texture;
        Vector2 origin;
        float radius;
        float scale;

        /// <summary>
        /// A boolean indicating if this ball is colliding with another
        /// </summary>
        public bool Colliding { get; set; }

        /// <summary>
        /// A vector to the center of the ball
        /// </summary>
        public Vector2 Center { get; set; }

        /// <summary>
        /// A vector representing the velocity of the ball
        /// </summary>
        public Vector2 Velocity { get; set; }

        /// <summary>
        /// The mass of the ball (also its radius)
        /// </summary>
        public float Mass {
            get => radius;
            set
            {
                radius = value;
                scale = radius / 32;
                origin = new Vector2(32, 32);
            }
        }

        /// <summary>
        /// Loads the ball's texture
        /// </summary>
        /// <param name="contentManager">The content manager to use</param>
        public void LoadContent(ContentManager contentManager)
        {
            texture = contentManager.Load<Texture2D>("ball");
        }

        /// <summary>
        /// Updates the ball
        /// </summary>
        /// <param name="gameTime">An object representing time in the game</param>
        public void Update(GameTime gameTime)
        {
            // Move the balls
            Center += Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Bounce balls off the edge of the screen
            if (Center.X < radius || Center.X > Constants.GAME_WIDTH - radius) Velocity *= -Vector2.UnitX;
            if (Center.Y < radius || Center.Y > Constants.GAME_HEIGHT - radius) Velocity *= -Vector2.UnitY;

            // Clear the colliding flag 
            Colliding = false;
        }

        /// <summary>
        /// Draws the ball using the provided spritebatch
        /// </summary>
        /// <param name="gameTime">an object representing time in the game</param>
        /// <param name="spriteBatch">The spritebatch to render with</param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            // Use Green for visual collision indication
            Color color = (Colliding) ? Color.Green : Color.White;
            spriteBatch.Draw(texture, Center, null, color, 0, origin, scale, SpriteEffects.None, 0);
        }

        /// <summary>
        /// Tests if this ball is colliding with another
        /// </summary>
        /// <param name="ball">The ball to test against</param>
        /// <returns>True on collision, false otherwise</returns>
        public bool CollidesWith(BallSprite ball)
        {
            return this.Mass + ball.Mass >= Vector2.Distance(Center, ball.Center);
        }
    }
}
