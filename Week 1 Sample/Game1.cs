using Engine.Engines;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Sprites;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Week_1_Sample
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Texture2D _texture;
        private Song _song;
        private SimpleSprite _sprite1;
        private SoundEffect _soundeffect;
        private SoundEffectInstance _soundPlayer;
        List<SimpleSprite> _lips = new List<SimpleSprite>();
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            new InputEngine(this);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _texture = Content.Load<Texture2D>(@"Week 4 Lab Assets-2122/backgroundImage");
            _song = Content.Load<Song>(@"Week 4 Lab Assets-2122/Opening Music Track");
            // TODO: use this.Content to load your game content here
            //MediaPlayer.Play(_song);
            _sprite1 = new SimpleSprite(
                Content.Load<Texture2D>(@"Week 4 Lab Assets-2122/body"),
                GraphicsDevice.Viewport.Bounds.Center.ToVector2()
                );
            _soundeffect = Content.Load<SoundEffect>(@"Week 4 Lab Assets-2122/Collected");
            _soundPlayer = _soundeffect.CreateInstance();

            Random r = new Random();
            Vector2[] directions = new Vector2[] {Vector2.One, -Vector2.One,
                                                    Vector2.UnitX, Vector2.UnitY,};
            for (int i = 0; i < 4; i++)
            {
                _lips.Add(new SimpleSprite(
                Content.Load<Texture2D>(@"Week 4 Lab Assets-2122/lips"),
                GraphicsDevice.Viewport.Bounds.Center.ToVector2() * r.NextSingle() *
                directions[r.Next(0,directions.Length)]
                ));
            }
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            //if (Keyboard.GetState().IsKeyDown(Keys.A))
            //    _sprite1.Move(new Vector2(-1,0));
            if (InputEngine.IsKeyHeld(Keys.A))
                _sprite1.Move(new Vector2(-1, 0));
            if (_lips.Count > 0 && InputEngine.IsKeyPressed(Keys.X))
                _lips.Remove(_lips.First());

            // TODO: Add your update logic here
            //MouseState ms = Mouse.GetState();
            if (_sprite1.BoundingRect.Contains(InputEngine.MousePosition))
            {
                _sprite1.CurrentColor = _sprite1.tint * _sprite1.Transparency;
                //if (ms.LeftButton == ButtonState.Pressed)
                //{
                //    if (_soundPlayer.State != SoundState.Playing)
                //        _soundPlayer.Play();

                //}

                if (InputEngine.IsMouseLeftClick())
                {
                    if (_soundPlayer.State != SoundState.Playing)
                        _soundPlayer.Play();
                }

            }
            else
            {
                _sprite1.CurrentColor = Color.White;
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
            _spriteBatch.Draw(_texture,GraphicsDevice.Viewport.Bounds,Color.White);
            _sprite1.draw(_spriteBatch);
            foreach (var item in _lips)
                item.draw(_spriteBatch);
            _spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}