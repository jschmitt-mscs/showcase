using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
namespace Project2
{
    class Camera
    {
        public float Zoom { get; set; }
        public Vector2 Position { get; set; }
        public Rectangle Bounds { get; protected set; }
        public Rectangle VisibleArea { get; protected set; }
        public Player player { get; set; }
        public Boolean overlay { get; set; }
        public Matrix Transform { get; protected set; }
        private float currentMouseWheelValue, previousMouseWheelValue, zoom, previousZoom;

        private SpriteBatch spriteBatch;
        private SpriteFont arial20;


        public Camera(Viewport viewport, Player player, SpriteBatch spriteBatch)
        {
            Bounds = viewport.Bounds;
            this.spriteBatch = spriteBatch;
            this.player = player;
            this.overlay = true;
            Zoom = 1f;
            Position = new Vector2(player.X, player.Y);


        }

        private void UpdateVisibleArea()
        {
            var inverseViewMatrix = Matrix.Invert(Transform);
            var t1 = Vector2.Transform(Vector2.Zero, inverseViewMatrix);
            var tr = Vector2.Transform(new Vector2(Bounds.X, 0), inverseViewMatrix);
            var b1 = Vector2.Transform(new Vector2(0, Bounds.Y), inverseViewMatrix);
            var br = Vector2.Transform(new Vector2(Bounds.Width, Bounds.Height), inverseViewMatrix);

            var min = new Vector2(MathHelper.Min(t1.X, MathHelper.Min(tr.X, MathHelper.Min(b1.X, br.X))),
                MathHelper.Min(t1.Y, MathHelper.Min(tr.Y, MathHelper.Min(b1.Y, br.Y))));
            var max = new Vector2(MathHelper.Max(t1.X, MathHelper.Max(tr.X, MathHelper.Max(b1.X, br.X))),
                MathHelper.Max(t1.Y, MathHelper.Max(tr.Y, MathHelper.Max(b1.Y, br.Y))));
            VisibleArea = new Rectangle((int)min.X, (int)min.Y, (int)(max.X - min.X), (int)(max.Y - min.Y));
        }

        private void UpdateMatrix()
        {
            Transform = Matrix.CreateTranslation(new Vector3(-Position.X, -Position.Y, 0)) *
                Matrix.CreateScale(Zoom) *
                Matrix.CreateTranslation(new Vector3(Bounds.Width * 0.5f, Bounds.Height * 0.5f, 0));
            UpdateVisibleArea();
        }

        public void MoveCamera(Vector2 movePosition)
        {
            Vector2 newPosition = Position + movePosition;
            Position = newPosition;
        }
        public void AdjustZoom(float zoomAmount)
        {
            Zoom += zoomAmount;
            if (Zoom < 0.35f)
            {
                Zoom = 0.35f;
            }
            if (Zoom > 2f)
            {
                Zoom = 2f;
            }
        }

        public void UpdateCamera(Viewport bounds)
        {
            Bounds = bounds.Bounds;
            UpdateMatrix();
            Position = new Vector2(player.X, player.Y);



            Vector2 cameraMovement = Vector2.Zero;
            int moveSpeed;



            if (Zoom > .8f)
            {
                moveSpeed = 15;
            }
            else if (Zoom < .8f && Zoom >= .6f)
            {
                moveSpeed = 20;
            }
            else if (Zoom < .6f && Zoom > 0.35f)
            {
                moveSpeed = 25;
            }
            else if (Zoom <= .35f)
            {
                moveSpeed = 30;
            }
            else
            {
                moveSpeed = 10;
            }

            previousMouseWheelValue = currentMouseWheelValue;
            currentMouseWheelValue = Mouse.GetState().ScrollWheelValue;

            if (currentMouseWheelValue > previousMouseWheelValue)
            {
                AdjustZoom(.05f);
                //Console.WriteLine(moveSpeed);
            }
            if (currentMouseWheelValue < previousMouseWheelValue)
            {
                AdjustZoom(-.05f);
                //Console.WriteLine(moveSpeed);
            }

            previousZoom = zoom;
            zoom = Zoom;
            if (previousZoom != zoom)
            {
                //Console.WriteLine(zoom);
            }
            MoveCamera(cameraMovement);
        }
    }
}
