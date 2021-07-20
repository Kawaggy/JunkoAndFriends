using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace JunkoAndFriends
{
    public static class JunkoAndFriendsRenderTargets
    {
        public static void Draw()
        {
            Junko.Draw();
        }

        public static void Initialize()
        {
            Junko.Initialize();
        }

        public static void Unload()
        {
            Junko.Unload();
        }

        public static class Junko
        {
            public static RenderTarget2D junkoAura;
            public static Texture2D texture;

            public static void Initialize()
            {
                texture = ModContent.GetTexture("JunkoAndFriends/ExtraTextures/JunkoAura");
                junkoAura = new RenderTarget2D(Main.graphics.GraphicsDevice, texture.Width, texture.Height, false, Main.graphics.GraphicsDevice.PresentationParameters.BackBufferFormat, Main.graphics.GraphicsDevice.PresentationParameters.DepthStencilFormat);
            }

            public static void Draw()
            {
                Main.graphics.GraphicsDevice.Clear(Color.Transparent);
                Main.graphics.GraphicsDevice.SetRenderTarget(junkoAura);
                Main.graphics.GraphicsDevice.Clear(Color.Transparent);
                Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise);
                if (texture != null && !texture.IsDisposed)
                {
                    DrawData data = new DrawData(texture, Vector2.Zero, texture.Frame(), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0);
                    MiscShaderData shaderData = GameShaders.Misc["WaveShader"];
                    shaderData.Apply(data);
                    Main.spriteBatch.Draw(texture, Vector2.Zero, texture.Frame(), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0);
                    Main.pixelShader.CurrentTechnique.Passes[0].Apply();
                }
                Main.spriteBatch.End();
                Main.graphics.GraphicsDevice.SetRenderTarget(null);
            }

            public static void Unload()
            {
                texture.Dispose();
                texture = null;
                junkoAura.Dispose();
                junkoAura = null;
            }
        }
    }
}
