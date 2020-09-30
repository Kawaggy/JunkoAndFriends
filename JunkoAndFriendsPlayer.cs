using JunkoAndFriends.Items;
using JunkoAndFriends.Items.FlandreVanity;
using JunkoAndFriends.Items.GuraGawrVanity;
using JunkoAndFriends.Items.JunkoVanity;
using JunkoAndFriends.Items.RemiliaVanity;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace JunkoAndFriends
{
    public class JunkoAndFriendsPlayer : ModPlayer
    {
        private int remiWingFrameCounter = 0;
        private int remiWingFrame = 0;

        private bool guraGawrDoA = false;
        private int guraGawrACounter = 0;
        private int guraGawrAFrame = 0;
        private bool guraGawrDoTail = false;
        private int guraGawrTailCounter = 0;
        private int guraGawrTailFrame = 0;
        private float guraGawrTridentRotation = 0f;

        public override void Initialize()
        {
            remiWingFrameCounter = 0;
            remiWingFrame = 0;

            guraGawrDoA = false;
            guraGawrACounter = 0;
            guraGawrAFrame = 0;
            guraGawrDoTail = false;
            guraGawrTailCounter = 0;
            guraGawrTailFrame = 0;
        }

        public override void PostUpdateMiscEffects()
        {
            guraGawrTridentRotation = -(player.velocity.X * 0.05f);
            guraGawrTridentRotation += player.velocity.Y * 0.05f;

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

            if (player.armor[10].type == ModContent.ItemType<GuraGawrHeadHair>() || player.armor[10].type == ModContent.ItemType<GuraGawrHeadHoodie>())
                GuraGawrALogic();

            if (player.armor[11].type == ModContent.ItemType<GuraGawrBody>() && player.armor[12].type == ModContent.ItemType<GuraGawrLeg>())
                GuraGawrTailLogic();
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

        private void GuraGawrALogic()
        {
            int guraGawrAFrames = 7;
            if (Main.rand.Next(60000) == 0)
                guraGawrDoA = true;

            if (guraGawrDoA)
            {
                guraGawrACounter++;
                int speed = 30;
                if (guraGawrACounter >= speed * guraGawrAFrames)
                {
                    guraGawrACounter = 0;
                    guraGawrDoA = false;
                }

                if (guraGawrACounter == speed * 5)
                {
                    CombatText.NewText(player.Hitbox, Color.White, "A", true);
                }

                guraGawrAFrame = guraGawrACounter / speed;
            }
            else
                guraGawrAFrame = 0;
        }

        private void GuraGawrTailLogic()
        {
            int guraGawrTailFrames = 4;
            if (Main.rand.Next(200) == 0)
                guraGawrDoTail = true;

            if (guraGawrDoTail)
            {
                guraGawrTailCounter++;
                int speed = 30;
                if (guraGawrTailCounter >= speed * guraGawrTailFrames)
                {
                    guraGawrTailCounter = 0;
                    guraGawrDoTail = false;
                }

                guraGawrTailFrame = guraGawrTailCounter / speed;
            }
            else
                guraGawrTailFrame = 0;
        }

        public override void ModifyDrawLayers(List<PlayerLayer> layers)
        {
            int headLayer = layers.FindIndex(l => l == PlayerLayer.Head);

            if (headLayer > -1)
            {
                layers.Insert(headLayer + 1, GuraGawrA);
            }

            JunkoAura.visible = true;
            layers.Insert(0, JunkoAura);
            RemiWings_Layer.visible = true;
            layers.Insert(1, RemiWings_Layer);
            GuraGawrTail.visible = true;
            layers.Insert(0, GuraGawrTail);
            GuraGawrTrident.visible = true;
            layers.Insert(0, GuraGawrTrident);
        }

        public override void ModifyDrawInfo(ref PlayerDrawInfo drawInfo)
        {
            if (drawInfo.shadow != 0)
                return;

            Player drawPlayer = drawInfo.drawPlayer;

            if (drawPlayer.armor[12].type == ModContent.ItemType<FlandreLeg>() ||
                drawPlayer.armor[12].type == ModContent.ItemType<RemiliaLeg>() ||
                drawPlayer.armor[12].type == ModContent.ItemType<GuraGawrLeg>())
            {
                TurnLegTransparent(ref drawInfo);
            }

            if (drawPlayer.armor[11].type == ModContent.ItemType<GuraGawrBody>())
            {
                TurnBodyTransparent(ref drawInfo);
            }

            if (drawPlayer.armor[10].type == ModContent.ItemType<GuraGawrHeadHair>() || drawPlayer.armor[10].type == ModContent.ItemType<GuraGawrHeadHoodie>())
            {
                TurnHeadTransparent(ref drawInfo);
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

        private void TurnHeadTransparent(ref PlayerDrawInfo drawInfo)
        {
            drawInfo.eyeColor = Color.Transparent;
            drawInfo.eyeWhiteColor = Color.Transparent;
            drawInfo.faceColor = Color.Transparent;
            drawInfo.hairColor = Color.Transparent;
            drawInfo.headGlowMaskColor = Color.Transparent;
        }

        private void TurnBodyTransparent(ref PlayerDrawInfo drawInfo)
        {
            drawInfo.armGlowMaskColor = Color.Transparent;
            drawInfo.bodyColor = Color.Transparent;
            drawInfo.bodyGlowMaskColor = Color.Transparent;
            drawInfo.shirtColor = Color.Transparent;
            drawInfo.underShirtColor = Color.Transparent;
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
            int slot = 0;
            for (int i = 3; i < 8 + drawPlayer.extraAccessorySlots; i++)
            {
                Item item = drawPlayer.armor[i];
                if (item.type == ModContent.ItemType<RemiliaWings>())
                {
                    slot = i;
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

            if (drawPlayer.hideVisual[slot] && drawPlayer.Friends().remiWingFrame == 0 && drawPlayer.velocity.Y == 0)
                return;

            if (hasOtherWings)
                return;

            Texture2D texture1 = mod.GetTexture("ExtraTextures/RemiliaWings_L");
            Texture2D texture2 = mod.GetTexture("ExtraTextures/RemiliaWings_R");
            SpriteEffects spriteEffects = drawInfo.spriteEffects;
            float drawX1 = (int)drawInfo.position.X + drawPlayer.width + 14;
            float drawY1 = (int)drawInfo.position.Y + drawPlayer.height - 5;

            float drawX2 = (int)drawInfo.position.X - 8;
            float drawY2 = (int)drawInfo.position.Y + drawPlayer.height - 10;

            if (spriteEffects == SpriteEffects.FlipHorizontally || (int)spriteEffects == 3)
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

        public static readonly PlayerLayer GuraGawrA = new PlayerLayer("JunkoAndFriends", "GuraGawrA", PlayerLayer.Head, delegate (PlayerDrawInfo drawInfo)
        {
            if (drawInfo.shadow != 0 || drawInfo.drawPlayer.dead)
                return;

            Player drawPlayer = drawInfo.drawPlayer;
            Mod mod = ModLoader.GetMod("JunkoAndFriends");

            if (drawPlayer.armor[10].type != ModContent.ItemType<GuraGawrHeadHair>() && drawPlayer.armor[10].type != ModContent.ItemType<GuraGawrHeadHoodie>())
                return;

            Texture2D faceTexture = mod.GetTexture("ExtraTextures/GuraGawrAFace");

            float drawFaceX = (int)drawInfo.position.X + drawPlayer.width / 2 + 4;
            float drawFaceY = (int)drawInfo.position.Y + drawPlayer.height + 3;

            SpriteEffects spriteEffects = drawInfo.spriteEffects;

            switch((int)spriteEffects)
            {
                case 1:
                case 3:
                    drawFaceX -= 8;
                    break;
            }

            bool flag = ((int)spriteEffects == 0 || (int)spriteEffects == 1);
            if (JunkoAndFriends.guraGawrDownFrames.Contains(drawPlayer.bodyFrame.Y) && flag)
            {
                drawFaceY += 2;
            }
            else if (JunkoAndFriends.guraGawrDownFrames.Contains(drawPlayer.bodyFrame.Y))
            {
                drawFaceY -= 2;
            }

            if ((int)spriteEffects == 2 || (int)spriteEffects == 3)
            {
                drawFaceY += 6;
            }
            Vector2 origin = drawInfo.headOrigin;

            Vector2 facePosition = new Vector2(drawFaceX, drawFaceY) + drawPlayer.headPosition - Main.screenPosition;

            float alpha = (255 - drawPlayer.immuneAlpha) / 255f;
            Color color = Lighting.GetColor((int)((drawInfo.position.X + drawPlayer.width / 2f) / 16f), (int)((drawInfo.position.Y + drawPlayer.height / 2f) / 16f));

            float faceRotation = drawPlayer.headRotation;

            Rectangle faceFrame = new Rectangle(0, drawPlayer.Friends().guraGawrAFrame * (faceTexture.Height / 7), faceTexture.Width, faceTexture.Height / 7);

            DrawData faceDrawData = new DrawData(faceTexture, facePosition, faceFrame, color * alpha * (drawPlayer.Friends().guraGawrDoA ? 1f : 0f), faceRotation, new Vector2(faceTexture.Width / 2f, faceTexture.Height / 2f), 1f, spriteEffects, 0)
            {
                shader = drawInfo.headArmorShader
            };

            Main.playerDrawData.Add(faceDrawData);
        });

        public static readonly PlayerLayer GuraGawrTail = new PlayerLayer("JunkoAndFriends", "GuraGawrTail", PlayerLayer.MiscEffectsBack, delegate (PlayerDrawInfo drawInfo)
        {
            if (drawInfo.shadow != 0 || drawInfo.drawPlayer.dead)
                return;

            Player drawPlayer = drawInfo.drawPlayer;
            Mod mod = ModLoader.GetMod("JunkoAndFriends");

            if (drawPlayer.armor[11].type != ModContent.ItemType<GuraGawrBody>() || drawPlayer.armor[12].type != ModContent.ItemType<GuraGawrLeg>())
                return;

            Texture2D tailTexture = mod.GetTexture("ExtraTextures/GuraGawrTail");

            float drawX = (int)drawInfo.position.X + 2;
            float drawY = (int)drawInfo.position.Y + drawPlayer.height + 12;

            SpriteEffects spriteEffects = drawInfo.spriteEffects;

            if (spriteEffects == SpriteEffects.FlipHorizontally && spriteEffects != SpriteEffects.FlipVertically)
            {
                drawX = (int)drawInfo.position.X + drawPlayer.width + 14;
            }
            if (spriteEffects == SpriteEffects.FlipVertically)
            {
                drawX = (int)drawInfo.position.X + 2;
                drawY = (int)drawInfo.position.Y + 20;
            }
            if ((int)spriteEffects == 3)
            {
                drawY = (int)drawInfo.position.Y + 20;
                drawX = (int)drawInfo.position.X + drawPlayer.width + 14;
            }

            Vector2 origin = drawInfo.bodyOrigin;

            Vector2 tailPosition = new Vector2(drawX, drawY) + drawPlayer.bodyPosition - Main.screenPosition;

            float alpha = (255 - drawPlayer.immuneAlpha) / 255f;
            Color color = Lighting.GetColor((int)((drawInfo.position.X + drawPlayer.width / 2f) / 16f), (int)((drawInfo.position.Y + drawPlayer.height / 2f) / 16f));

            float tailRotation = drawPlayer.bodyRotation;

            Rectangle tailFrame = new Rectangle(0, drawPlayer.Friends().guraGawrTailFrame * (tailTexture.Height / 4), tailTexture.Width, tailTexture.Height / 4);

            DrawData tailDrawData = new DrawData(tailTexture, tailPosition, tailFrame, color * alpha, tailRotation, origin, 1f, spriteEffects, 0)
            {
                shader = drawInfo.bodyArmorShader
            };

            Main.playerDrawData.Add(tailDrawData);
        });

        public static readonly PlayerLayer GuraGawrTrident = new PlayerLayer("JunkoAndFriends", "GuraGawrTrident", PlayerLayer.MiscEffectsBack, delegate (PlayerDrawInfo drawInfo)
        {
            if (drawInfo.shadow != 0 || drawInfo.drawPlayer.dead)
                return;

            Player drawPlayer = drawInfo.drawPlayer;
            Mod mod = ModLoader.GetMod("JunkoAndFriends");

            if (drawPlayer.armor[11].type != ModContent.ItemType<GuraGawrBody>() || drawPlayer.armor[12].type != ModContent.ItemType<GuraGawrLeg>() || (drawPlayer.armor[10].type != ModContent.ItemType<GuraGawrHeadHair>() && drawPlayer.armor[10].type != ModContent.ItemType<GuraGawrHeadHoodie>()))
                return;

            Texture2D tridentTexture = mod.GetTexture("ExtraTextures/GuraGawrTrident");

            float drawX = drawInfo.position.X + drawPlayer.width / 2f;
            float drawY = drawInfo.position.Y + drawPlayer.height / 2f;

            SpriteEffects spriteEffects = drawInfo.spriteEffects;

            if ((int)spriteEffects == 0 || (int)spriteEffects == 2)
                drawX += 6;
            else
                drawX -= 6;

            if ((int)spriteEffects == 2 || (int)spriteEffects == 3)
                drawY -= 6;

            Vector2 origin = drawInfo.bodyOrigin;

            Vector2 tridentPosition = new Vector2(drawX, drawY) + drawPlayer.bodyPosition - Main.screenPosition;

            float alpha = (255 - drawPlayer.immuneAlpha) / 255f;
            Color color = Lighting.GetColor((int)((drawInfo.position.X + drawPlayer.width / 2f) / 16f), (int)((drawInfo.position.Y + drawPlayer.height / 2f) / 16f));

            float rotation = 0f;
            if ((int)spriteEffects == 0 || (int)spriteEffects == 3)
                rotation = 1f;
            else
                rotation = -1f;

            float tridentRotation = drawPlayer.bodyRotation + rotation + drawPlayer.Friends().guraGawrTridentRotation;

            DrawData tridentDrawData = new DrawData(tridentTexture, tridentPosition, null, color * alpha, tridentRotation, new Vector2(tridentTexture.Width / 2f, tridentTexture.Height / 2f), 1f, spriteEffects, 0)
            {
                shader = drawInfo.bodyArmorShader
            };

            Main.playerDrawData.Add(tridentDrawData);
        });
    }
}
