using JunkoAndFriends.Items;
using JunkoAndFriends.Items.FlandreVanity;
using JunkoAndFriends.Items.JunkoVanity;
using JunkoAndFriends.Items.RemiliaVanity;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace JunkoAndFriends
{
    public class JunkoAndFriendsPlayer : ModPlayer
    {
        public override void ModifyDrawLayers(List<PlayerLayer> layers)
        {
            JunkoAura.visible = true;
            layers.Insert(0, JunkoAura);
        }

        public override void ModifyDrawInfo(ref PlayerDrawInfo drawInfo)
        {
            if (drawInfo.shadow != 0)
                return;

            Player drawPlayer = drawInfo.drawPlayer;
            if (drawPlayer.armor[12].type == ModContent.ItemType<FlandreLeg>() ||
                drawPlayer.armor[12].type == ModContent.ItemType<RemiliaLeg>())
            {
                TurnLegTransparent(ref drawInfo);
            }
        }

        private void TurnFullyTransparent(ref PlayerDrawInfo drawInfo)
        {
            drawInfo.armGlowMaskColor = Color.Transparent;
            drawInfo.bodyColor = Color.Transparent;
            drawInfo.bodyGlowMaskColor = Color.Transparent;
            drawInfo.eyeColor = Color.Transparent;
            drawInfo.eyeWhiteColor = Color.Transparent;
            drawInfo.faceColor = Color.Transparent;
            drawInfo.hairColor = Color.Transparent;
            drawInfo.headGlowMaskColor = Color.Transparent;
            drawInfo.legColor = Color.Transparent;
            drawInfo.legGlowMaskColor = Color.Transparent;
            drawInfo.lowerArmorColor = Color.Transparent;
            drawInfo.middleArmorColor = Color.Transparent;
            drawInfo.pantsColor = Color.Transparent;
            drawInfo.shirtColor = Color.Transparent;
            drawInfo.shoeColor = Color.Transparent;
            drawInfo.underShirtColor = Color.Transparent;
            drawInfo.upperArmorColor = Color.Transparent;
        }

        private void TurnLegTransparent(ref PlayerDrawInfo drawInfo)
        {
            drawInfo.legColor = Color.Transparent;
            drawInfo.legGlowMaskColor = Color.Transparent;
            drawInfo.pantsColor = Color.Transparent;
        }

        public static readonly PlayerLayer JunkoAura = new PlayerLayer("JunkoAndFriends", "JunkoAura", PlayerLayer.MiscEffectsBack, delegate (PlayerDrawInfo drawInfo)
        {
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.ZoomMatrix);

            if (drawInfo.shadow != 0 || drawInfo.drawPlayer.dead)
                return;

            Player drawPlayer = drawInfo.drawPlayer;
            Mod mod = ModLoader.GetMod("JunkoAndFriends");

            if (drawPlayer.armor[10].type != ModContent.ItemType<JunkoHead>() || drawPlayer.armor[11].type != ModContent.ItemType<JunkoBody>() || drawPlayer.armor[12].type != ModContent.ItemType<JunkoLeg>())
                return;

            Texture2D texture = mod.GetTexture("ExtraTextures/JunkoAura");
            float drawX = (int)drawInfo.position.X + drawPlayer.width / 2;
            float drawY = (int)drawInfo.position.Y + drawPlayer.height / 2;

            Vector2 origin = drawInfo.bodyOrigin;

            Vector2 position = new Vector2(drawX, drawY) + drawPlayer.bodyPosition - Main.screenPosition;

            Color color = Color.White;

            float rotation = drawPlayer.bodyRotation;

            SpriteEffects spriteEffects = drawInfo.spriteEffects;

            DrawData drawData = new DrawData(texture, position, null, color, rotation, new Vector2(texture.Width / 2f, texture.Height / 2f), 1f, spriteEffects, 0);

            int shaderID = GameShaders.Armor.GetShaderIdFromItemId(ModContent.ItemType<WaveShaderDye>());

            drawData.shader = shaderID;

            Main.playerDrawData.Add(drawData);
        });
    }
}
