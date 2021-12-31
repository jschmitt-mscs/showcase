using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Project2
{
    class Tile
    {
        public int tileDim { get; set; }
        public Rectangle drawRect { get; set; }
        public Texture2D _texture { get; set; }
        public Vector2 _coordinates { get; set; }
        public Vector2 _globalCoords { get; set; }
        public Camera camera { get; set; }
        public Color tileColor { get; set; }
        public MouseState mouseState { get; set; }
        public Point mousePoint { get; set; }
        public float noiseValue { get; set; }
        public Boolean isHovered { get; set; }
        private SpriteBatch spriteBatch;
        private Texture2D pixel { get; set; }
        
        public Tile(Vector2 _coordinates, Vector2 _globalCoords, Color color, Texture2D pixel, Camera camera, GraphicsDevice graphicsDevice, SpriteBatch spriteBatch)
        {
            this.tileDim = 32;
            this.spriteBatch = spriteBatch;
            this._coordinates = _coordinates;
            this._globalCoords = _globalCoords;
            this.noiseValue = noiseValue;
            this.pixel = pixel;
            this.tileColor = color;
            this.camera = camera;
            this.drawRect = new Rectangle((int)_coordinates.X,(int)_coordinates.Y, tileDim, tileDim);
            _texture = pixel;
            
        }

        public void Update()
        {
            mouseState = Mouse.GetState();
            mousePoint = new Point(mouseState.X, mouseState.Y);

            if (drawRect.Contains(mousePoint))
            {
                isHovered = true;
            }
        }
        public void Draw()
        {
            Rectangle visibleArea = camera.VisibleArea;

            if (_coordinates.X >= visibleArea.X - 500  
                && _coordinates.X <= visibleArea.X + visibleArea.Width + 500
                && _coordinates.Y >= visibleArea.Y - 500
                && _coordinates.Y <= visibleArea.Y + visibleArea.Height + 500)
            {
                spriteBatch.Draw(_texture, drawRect, tileColor);
            }

            if(isHovered)
            {
                this._texture.CreateBorder(4, Color.White);
                spriteBatch.Draw(_texture, drawRect, tileColor);
            }
            
        }
    }
}
