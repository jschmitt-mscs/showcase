using System;
using System.Collections.Generic;
using System.Text;
using SimplexNoise;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Project2
{
    class Chunk
    {
        public int GlobalX { get; set; }
        public int GlobalY { get; set; }
        public int chunkSize { get; set; }
        

        public int tileDim = 32;
        public static int t1 = 20;
        public static int t2 = 100;
        public Dictionary<Vector2, Tile> tileMap { get; set; }
        public Camera camera { get; set; }

        private Texture2D pixel;

        private SpriteBatch spriteBatch;

        public Chunk(int globalX, int globalY, int chunkSize, Texture2D pixel, Camera camera, GraphicsDevice graphicsDevice, SpriteBatch spriteBatch)
        {
            this.GlobalX = globalX;
            this.GlobalY = globalY;
            this.chunkSize = chunkSize;
            this.camera = camera;
            this.spriteBatch = spriteBatch;
            this.tileMap = new Dictionary<Vector2, Tile>();
            this.pixel = pixel;


            Vector2 noisePosition = new Vector2();
            Vector2 tileSize = new Vector2(tileDim, tileDim);
            if (globalX >= 0 && globalY >= 0)
            {
                for (int i = 0; i < chunkSize; i++)
                {
                    for (int j = 0; j < chunkSize; j++)
                    {

                        noisePosition.X = globalX * chunkSize + i;
                        noisePosition.Y = globalY * chunkSize + j;
                        float noiseValue = Noise.Generate(noisePosition.X / 100, noisePosition.Y / 100) * 256;
                        Color tileColor = tileType(noiseValue);
                        Vector2 drawLocation = new Vector2(globalX * chunkSize * tileDim + tileSize.X * i, globalY * chunkSize * tileDim + tileSize.Y * j);
                        Vector2 globalTileCoords = new Vector2(globalX * chunkSize + i, globalY * chunkSize + j);

                        //tileMap[i, j] = new Tile(spriteCoords, tileSize, drawLocation, noiseValue, camera, spriteBatch, gameContent);
                        Tile newTile = new Tile(drawLocation, globalTileCoords, tileColor, pixel, camera, graphicsDevice, spriteBatch);
                        tileMap.Add(new Vector2(i, j), newTile);

                    }
                }
            }
            else if (globalX < 0 && globalY >= 0)
            {
                
            }
            else if (globalX >= 0 && globalY < 0)
            {

            }
            else if (globalX < 0 && globalY < 0)
            {

            }
        }

        public void Draw()
        {
            Rectangle visibleArea = camera.VisibleArea;
            for (int i = 0; i < chunkSize; i++)
            {
                for (int j = 0; j < chunkSize; j++)
                {

                    if (tileMap.ContainsKey(new Vector2(i, j)))
                    {

                        tileMap[new Vector2(i, j)].Draw();
                    }

                }
            }
        }

        private Color tileType(float noiseValue)
        {
            Color A;
            Color color1 = new Color(102, 141, 61);
            Color color2 = new Color(143, 128, 61);
            Color color3 = new Color(61, 103, 143);

            if (noiseValue >= t2)
            {
                A = color1;
                return A;
            } else if ( noiseValue < t2 && noiseValue >= t1) 
            {
                A = color2;
                return A;
            } else
            {
                A = color3;
                return A;
            }
        }

        private float betterGen(Vector2 position, int octaves, float persistance, float lacunarity, float exp)
        {
            float totalAmplitude = 0;
            float frequency = 1;
            float amplitude = 1;
            float total = 0;
            float result = 0;
            for(int i =0; i < octaves; i++)
            {
                total += Noise.Generate(position.X * frequency, position.Y * frequency) * amplitude;
                totalAmplitude += amplitude;
                amplitude *= persistance;
                frequency *= lacunarity;
            }
            total = (float)Math.Pow((double)total, (double)exp);
            result = total / totalAmplitude;
            return result;
        }
    }
}
