using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameboyEmulator
{
	class EmulatorDisplay : Microsoft.Xna.Framework.Game
	{
		private GraphicsDeviceManager graphics;
		private SpriteBatch spriteBatch;
		private Vector2 baseScreenSize = new Vector2(800, 480);

		private GamePadState gamePadState;
		private KeyboardState keyboardState;

		public EmulatorDisplay()
		{
			graphics = new GraphicsDeviceManager(this);
		}

		Texture2D dummyTexture;
		Rectangle dummyRectangle;
		protected override void LoadContent()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch(GraphicsDevice);

			dummyTexture = new Texture2D(GraphicsDevice, 1, 1);
			dummyTexture.SetData(new Color[] { Color.White });
			dummyRectangle = new Rectangle(0,0,50,50);
		}
		protected override void Update(GameTime gameTime)
		{

			base.Update(gameTime);
		}
		private void HandleInput(GameTime gameTime)
		{

		}

		//draw primitives https://stackoverflow.com/questions/2792694/draw-rectangle-with-xna
		protected override void Draw(GameTime gameTime)
		{
			graphics.GraphicsDevice.Clear(Color.CornflowerBlue);

			spriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, null, null);

			spriteBatch.Draw(dummyTexture, dummyRectangle, Color.Red);

			spriteBatch.End();
			base.Draw(gameTime);
		}
	}
}
