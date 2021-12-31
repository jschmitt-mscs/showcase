using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project2
{
    class Player
    {
        public int X;
        public int Y;
        public Texture2D _texture { get; set; }
        private SpriteBatch spriteBatch;


        //player stats
        public int velocity { get; set; }
        public Vector2 currentChunkCoords { get; set; }
        public Vector2 localChunkCoords { get; set; }

        public Player(GraphicsDevice graphicsDevice, SpriteBatch spriteBatch)
        {
            _texture = new Texture2D(graphicsDevice, 1, 1);
            _texture.SetData(new Color[] { Color.White });

            this.spriteBatch = spriteBatch;
            this.velocity = 5;

        }

        public void Draw()
        {
            spriteBatch.Draw(_texture, new Rectangle(X, Y, 30, 30),Color.White);
        }

        public void MoveUp()
        {
            Y = Y - velocity;
        }

        public void MoveDown()
        {
            Y = Y + velocity;
        }

        public void MoveLeft()
        {
            X = X - velocity;
        }
        public void MoveRight()
        {
            X = X + velocity;
        }

    }
}
