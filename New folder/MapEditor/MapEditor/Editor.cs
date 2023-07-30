using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapEditor
{
    internal class Editor
    {
        public List<Sprite> NewPaths;
        bool hasAdded = false;
        public Editor()
        {
            NewPaths = new List<Sprite>();
        }
        public void Update(Sprite Path,GraphicsDevice graphics, Sprite[] Grass)
        {
            var mouseState = Mouse.GetState();
            var mousePos = new Point(mouseState.X, mouseState.Y);
            if (mousePos.X < 0)
            {
                Mouse.SetPosition(0, mousePos.Y);
            }
            if (mousePos.X > graphics.Viewport.Width)
            {
                Mouse.SetPosition(graphics.Viewport.Width, mousePos.Y);
            }
            if (mousePos.Y < 0)
            {
                Mouse.SetPosition(mousePos.X, 0);
            }
            if (mousePos.Y > graphics.Viewport.Height)
            {
                Mouse.SetPosition(mousePos.X, graphics.Viewport.Height);
            }
            mousePos = new Point(mouseState.X, mouseState.Y);

            if (Keyboard.GetState().IsKeyDown(Keys.E))
            {
                foreach(var path in NewPaths)
                {
                    if(path.HitBox.Value.Contains(mousePos))
                    {
                        NewPaths.Remove(path);
                        break;
                    }
                }
            }
            else
            {   
                if (mouseState.LeftButton == ButtonState.Pressed && (Path.HitBox.Value.Contains(mousePos) || hasAdded))
                {
                    if (hasAdded == false)
                    {
                        NewPaths.Add(new Sprite(Color.White, new Vector2(mousePos.X, mousePos.Y), Path.Image, 0, Vector2.Zero, Vector2.One));
                        hasAdded = true;
                    }
                    else if (hasAdded)
                    {
                        NewPaths[NewPaths.Count - 1].Position = new Vector2(mousePos.X, mousePos.Y);
                    }
                }
                else if (mouseState.LeftButton == ButtonState.Released && hasAdded)
                {
                    if(NewPaths.Count != 0)
                    {
                        foreach (var item in NewPaths)
                        {
                            foreach (var actual in NewPaths)
                            {
                                if (item == actual) continue;
                                if (item.HitBox.Value.Contains(actual.HitBox.Value))
                                {
                                    NewPaths.Remove(actual);
                                    break;
                                }
                            }
                        }
                    }
                    
                    foreach (var grassSquare in Grass)
                    {
                        if (grassSquare.HitBox.Value.Contains(NewPaths[NewPaths.Count - 1].Position))
                        {
                            NewPaths[NewPaths.Count - 1].Position = grassSquare.Position;
                        }
                    }
                    hasAdded = false;
                }
            }

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach(var item in NewPaths)
            {
                item.Draw(spriteBatch);
            }
        }
    }
}
