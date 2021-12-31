using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Collections;
using SimplexNoise;

namespace Project2
{
    class World
    {
        public Dictionary<Vector2, Chunk> chunkMap { get; set; }
        public Dictionary<Vector2, Tile> visibleTiles { get; set; }

        public int chunkSize { get; set; }
        public int seed { get; set; }
        public Player player { get; set; }
        public Camera camera { get; set; }
        public Tile mouseOver { get; set; }
        public Texture2D pixel { get; set; }
        public Vector2 playerPosition { get; set; }
        public MouseState mouseState { get; set; }
        public Point mousePoint { get; set; }

        public float noiseValue { get; set; }

        private SpriteBatch spriteBatch;
        private GraphicsDevice graphicsDevice;
        private GameContent gameContent;
        private SpriteFont spriteFont;


        public World(int seed, Player player, Camera camera, GameContent gameContent, GraphicsDevice graphicsDevice, SpriteBatch spriteBatch)
        {
            this.player = player;
            this.chunkMap = new Dictionary<Vector2, Chunk>();
            this.visibleTiles = new Dictionary<Vector2, Tile>();
            this.pixel = new Texture2D(graphicsDevice, 1, 1);
            pixel.SetData(new[] { Color.White });
            this.camera = camera;
            this.spriteBatch = spriteBatch;
            this.graphicsDevice = graphicsDevice;
            this.seed = seed;
            Noise.Seed = seed;
            spriteFont = gameContent.labelFont;

            playerPosition = new Vector2(player.X / 32, player.Y / 32);
            generateChunk();

        }

        public void Update()
        {
            Rectangle visibleArea = camera.VisibleArea;
            foreach (var chunk in chunkMap)
            {
                foreach (var tile in chunk.Value.tileMap)
                {
                    Vector2 drawLocation = tile.Value._coordinates;

                    if (drawLocation.X >= visibleArea.X - 300 
                        && drawLocation.X <= visibleArea.X + visibleArea.Width + 300
                        && drawLocation.Y >= visibleArea.Y - 300 
                        && drawLocation.Y <= visibleArea.Y + visibleArea.Height + 300)
                    {
                        if (!visibleTiles.ContainsKey(tile.Value._globalCoords))
                        {
                            visibleTiles.Add(tile.Value._globalCoords, tile.Value);
                        }
                    }
                }
            }
        }

        public void Draw()
        {
            foreach (var item in chunkMap)
            {
                item.Value.Draw();
            }
        }

        public void generateChunk()
        {
            playerPosition = new Vector2(player.X / 32, player.Y /32);
            Vector2 playerChunkCoords = getChunkCoords(playerPosition);
            Vector2 oChunkCoords;
            for (int i = (int)playerChunkCoords.X - 3; i < playerChunkCoords.X + 3; i++)
            {
                for (int j = (int)playerChunkCoords.Y - 3; j < playerChunkCoords.Y + 3; j++)
                {
                    oChunkCoords = new Vector2(i, j);
                    if (!chunkMap.ContainsKey(oChunkCoords))
                    {
                        chunkMap.Add(oChunkCoords, new Chunk((int)oChunkCoords.X, (int)oChunkCoords.Y, 32, pixel, camera, graphicsDevice, spriteBatch));

                        Console.WriteLine(oChunkCoords);
                    }
                }
            }
        }

        public Vector2 getChunkCoords(Vector2 playerCoords)
        {
            Vector2 chunkCoords = Vector2.Zero;
            Console.WriteLine(playerCoords);
            if (playerCoords.X < 0 && playerCoords.Y < 0)
            {
                chunkCoords.X = (float)Math.Ceiling(((playerCoords.X) / 32));
                chunkCoords.Y = (float)Math.Ceiling(((playerCoords.Y) / 32));
            }
            else if (playerCoords.X < 0 && playerCoords.Y >= 0)
            {
                chunkCoords.X = (float)Math.Ceiling(((playerCoords.X) / 32));
                chunkCoords.Y = (float)Math.Floor(((playerCoords.Y) / 32));
            }
            else if (playerCoords.X >= 0 && playerCoords.Y < 0)
            {
                chunkCoords.X = (float)Math.Floor(((playerCoords.X) / 32));
                chunkCoords.Y = (float)Math.Ceiling(((playerCoords.Y) / 32));
            }
            else
            {
                chunkCoords.X = (float)Math.Floor(((playerCoords.X) / 32));
                chunkCoords.Y = (float)Math.Floor(((playerCoords.Y) / 32));
            }

            //Console.WriteLine(chunkCoords);
            return chunkCoords;
        }

        public Point getChunkCoords(Point playerCoords)
        {
            Point chunkCoords = new Point();

            if (playerCoords.X < 0 && playerCoords.Y < 0)
            {
                chunkCoords.X = (int)Math.Ceiling((double)((playerCoords.X - 32) / 32));
                chunkCoords.Y = (int)Math.Ceiling((double)((playerCoords.Y - 32) / 32));
            }
            else if (playerCoords.X < 0 && playerCoords.Y >= 0)
            {
                chunkCoords.X = (int)Math.Ceiling((double)((playerCoords.X - 32) / 32));
                chunkCoords.Y = (int)Math.Floor((double)((playerCoords.Y) / 32));
            }
            else if (playerCoords.X >= 0 && playerCoords.Y < 0)
            {
                chunkCoords.X = (int)Math.Floor((double)((playerCoords.X) / 32));
                chunkCoords.Y = (int)Math.Ceiling((double)((playerCoords.Y - 32) / 32));

            }
            else
            {
                chunkCoords.X = (int)Math.Floor((double)((playerCoords.X) / 32));
                chunkCoords.Y = (int)Math.Floor((double)((playerCoords.Y) / 32));
            }

            return chunkCoords;
        }
    }
}
