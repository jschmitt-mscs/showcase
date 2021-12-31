using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Project2
{
    class GameContent
    {
        public SpriteFont labelFont { get; set; }
        public GameContent(ContentManager Content)
        {
            labelFont = Content.Load<SpriteFont>("Arial20");
        }
    }
}
