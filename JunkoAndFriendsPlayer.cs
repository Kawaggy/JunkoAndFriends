using JunkoAndFriends.Items;
using JunkoAndFriends.Items.FlandreVanity;
using JunkoAndFriends.Items.JunkoVanity;
using JunkoAndFriends.Items.RemiliaVanity;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace JunkoAndFriends
{
    public class JunkoAndFriendsPlayer : ModPlayer
    {
        private int remiWingFrameCounter = 0;
        private int remiWingFrame = 0;
        public override void PostUpdateMiscEffects()
        {
            bool hasRemiWings = false;
            for (int i = 3; i < 8 + player.extraAccessorySlots; i++)
            {
                Item item = player.armor[i];
                if (item.type == ModContent.ItemType<RemiliaWings>())
                    hasRemiWings = true;
            }

            for (int i = 13; i < 18 + player.extraAccessorySlots; i++)
            {
                Item item = player.armor[i];
                if (item.type == ModContent.ItemType<RemiliaWings>())
                    hasRemiWings = true;
            }
            if (hasRemiWings)
                RemiliaWingsLogic();
        }

        private void RemiliaWingsLogic()
        {
            int remiWingFrames = 7;
            bool flying = false;
            if (player.wingsLogic > 0 && player.controlJump && player.wingTime > 0f && !player.jumpAgainCloud && player.jump == 0 && player.velocity.Y != 0f)
                flying = true;
            if (flying || player.jump > 0)
            {
                remiWingFrameCounter++;
                int speed = 4;
                if (remiWingFrameCounter >= speed * remiWingFrames)
                    remiWingFrameCounter = 0;
                remiWingFrame = remiWingFrameCounter / speed;
            }
            else if (player.velocity.Y != 0f)
            {
                if (player.controlJump)
                {
                    remiWingFrameCounter++;
                    int speed = 3;
                    if (remiWingFrameCounter >= speed * remiWingFrames)
                        remiWingFrameCounter = 0;
                    remiWingFrame = remiWingFrameCounter / speed;
                }
                else
                {
                    remiWingFrameCounter++;
                    int speed = 6;
                    if (remiWingFrameCounter >= speed * remiWingFrames)
                        remiWingFrameCounter = 0;
                    remiWingFrame = remiWingFrameCounter / speed;
                }
            }
            else
            {
                remiWingFrame = 0;
            }

            int i = (int)(player.position.X + (player.width / 2)) / 16;
            int j = (int)(player.position.Y + (player.height)) / 16;
            bool isOnGround = false;
            if (WorldGen.SolidTile(i, j + 1) || Main.tile[i, j + 1].type == TileID.Platforms)
            {
                isOnGround = true;
            }

            if (isOnGround)
                remiWingFrame = 0;

            if (remiWingFrame >= remiWingFrames)
                remiWingFrame = 0;
        }

        public override void ModifyDrawLayers(List<PlayerLayer> layers)
        {
            JunkoAura.visible = true;
            layers.Insert(0, JunkoAura);
            RemiWings_Layer.visible = true;
            layers.Insert(1, RemiWings_Layer);
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
            if (drawInfo.shadow != 0 || drawInfo.drawPlayer.dead)
                return;

            Player drawPlayer = drawInfo.drawPlayer;
            Mod mod = ModLoader.GetMod("JunkoAndFriends");

            if (drawPlayer.armor[10].type != ModContent.ItemType<JunkoHead>() || drawPlayer.armor[11].type != ModContent.ItemType<JunkoBody>())
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

            Lighting.AddLight(drawPlayer.position, new Vector3(0.870588235f, 0.396078431f, 0.611764706f));
        });

        public static readonly PlayerLayer RemiWings_Layer = new PlayerLayer("JunkoAndFriends", "RemiWings_Layer", PlayerLayer.MiscEffectsBack, delegate (PlayerDrawInfo drawInfo)
        {
            if (drawInfo.shadow != 0 || drawInfo.drawPlayer.dead)
                return;

            Player drawPlayer = drawInfo.drawPlayer;
            Mod mod = ModLoader.GetMod("JunkoAndFriends");

            bool hasRemiWings = false;
            int wingPosition = -1;
            for (int i = 3; i < 8 + drawPlayer.extraAccessorySlots; i++)
            {
                Item item = drawPlayer.armor[i];
                if (item.type == ModContent.ItemType<RemiliaWings>())
                {
                    hasRemiWings = true;
                    wingPosition = i;
                }
            }
            for (int i = 13; i < 18 + drawPlayer.extraAccessorySlots; i++)
            {
                Item item = drawPlayer.armor[i];
                if (item.type == ModContent.ItemType<RemiliaWings>())
                {
                    hasRemiWings = true;
                    wingPosition = i;
                }
            }

            if (!hasRemiWings)
                return;

            bool hasOtherWings = false;
            for (int i = 3; i < 8 + drawPlayer.extraAccessorySlots; i++)
            {
                Item item = drawPlayer.armor[i];
                if (item.wingSlot > 0 && item.type != ModContent.ItemType<RemiliaWings>() && i > wingPosition)
                    hasOtherWings = true;
            }
            for (int i = 13; i < 18 + drawPlayer.extraAccessorySlots; i++)
            {
                Item item = drawPlayer.armor[i];
                if (item.wingSlot > 0 && item.type != ModContent.ItemType<RemiliaWings>() && i > wingPosition)
                    hasOtherWings = true;
            }

            if (hasOtherWings)
                return;

            Texture2D texture1 = mod.GetTexture("ExtraTextures/RemiliaWings_L");
            Texture2D texture2 = mod.GetTexture("ExtraTextures/RemiliaWings_R");
            SpriteEffects spriteEffects = drawInfo.spriteEffects;
            float drawX1 = (int)drawInfo.position.X + drawPlayer.width + 14;
            float drawY1 = (int)drawInfo.position.Y + drawPlayer.height - 5;

            float drawX2 = (int)drawInfo.position.X - 8;
            float drawY2 = (int)drawInfo.position.Y + drawPlayer.height - 10;

            if (spriteEffects == SpriteEffects.FlipHorizontally)
            {
                drawX1 = (int)drawInfo.position.X + 8;
                drawY1 = (int)drawInfo.position.Y + drawPlayer.height - 5;

                drawX2 = (int)drawInfo.position.X + drawPlayer.width + 14 + 8;
                drawY2 = (int)drawInfo.position.Y + drawPlayer.height - 10;
            }


            Vector2 origin = drawInfo.bodyOrigin;
            Vector2 position1 = new Vector2(drawX1, drawY1) + drawPlayer.bodyPosition - Main.screenPosition;
            Vector2 position2 = new Vector2(drawX2, drawY2) + drawPlayer.bodyPosition - Main.screenPosition;

            float alpha = (255 - drawPlayer.immuneAlpha) / 255f;
            Color color = Lighting.GetColor((int)((drawInfo.position.X + drawPlayer.width / 2f) / 16f), (int)((drawInfo.position.Y + drawPlayer.height / 2f) / 16f));

            Rectangle frame1 = new Rectangle(0, drawPlayer.Friends().remiWingFrame * (texture2.Height / 7), texture2.Width, texture2.Height / 7);
            Rectangle frame2 = new Rectangle(0, drawPlayer.Friends().remiWingFrame * (texture1.Height / 7), texture1.Width, texture1.Height / 7);
            float rotation = drawPlayer.bodyRotation;


            DrawData drawDataR = new DrawData(texture2, position1, frame1, color * alpha, rotation, origin, 1f, spriteEffects, 0)
            {
                shader = drawInfo.wingShader
            };

            DrawData drawDataL = new DrawData(texture1, position2, frame2, color * alpha, rotation, origin, 1f, spriteEffects, 0)
            {
                shader = drawInfo.wingShader
            };

            Main.playerDrawData.Add(drawDataR);
            Main.playerDrawData.Add(drawDataL);
        });
    }
}
