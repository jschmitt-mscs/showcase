using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Project2
{
    public static class Utilities
    {
        public static void CreateBorder(this Texture2D texture, int borderWidth, Color borderColor)
        {
            Color[] colors = new Color[texture.Width * texture.Height];
            for(int i = 0; i < texture.Width; i++)
            {
                for (int j = 0; j < texture.Height; j++)
                {
                    bool colored = false;
                    for(int k = 0; k < borderWidth; k++)
                    {
                        if( i == k || j == k || i == texture.Width - 1 - k || j == texture.Height -1 - 1)
                        {
                            colors[i + j * texture.Width] = borderColor;
                            colored = true;
                        }
                    }
                    if(colored == false)
                    {
                        colors[i + j * texture.Width] = Color.Transparent;
                    }
                }
                texture.SetData(colors);
            }
        }
    }
}
