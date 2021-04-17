using JunkoAndFriends.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace JunkoAndFriends
{
    public class JunkoAndFriendsPlayer : ModPlayer
    {
        public int remiWingFrameCounter = 0;
        public int remiWingFrame = 0;

        public bool guraGawrDoA = false;
        public int guraGawrACounter = 0;
        public int guraGawrAFrame = 0;
        public bool guraGawrDoTail = false;
        public int guraGawrTailCounter = 0;
        public int guraGawrTailFrame = 0;
        public float guraGawrTridentRotation = 0f;

        public bool vanitySpecialEffect = false;
        public int vanitySpecialEffectCooldown = 0;

        public bool berserkerIsBerserk = false;
        public bool berserkerDoTransformation = false;
        public int berserkerHelmetFrame = 0;
        public int berserkerHelmetCounter = 0;
        public float berserkerCapeRotation = 0f;

        public bool pekoraSmoll = false;

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

            vanitySpecialEffect = false;
            vanitySpecialEffectCooldown = 0;

            berserkerIsBerserk = false;
            berserkerDoTransformation = false;
            berserkerHelmetFrame = 0;
            berserkerHelmetCounter = 0;
            berserkerCapeRotation = 0f;

            pekoraSmoll = false;
        }

        public override void PostUpdate()
        {
            if (player.direction == 1)
            {
                berserkerCapeRotation = (MathHelper.Clamp(player.velocity.Y, 0f, 120f) * 0.05f);
                guraGawrTridentRotation = -(MathHelper.Clamp(player.velocity.Y, -10f, 400f) * 0.08f);
            }
            else
            {
                berserkerCapeRotation = -(MathHelper.Clamp(player.velocity.Y, 0f, 120f) * 0.05f);
                guraGawrTridentRotation = (MathHelper.Clamp(player.velocity.Y, -10f, 400f) * 0.08f);
            }

            berserkerCapeRotation += player.velocity.X * 0.05f;
            guraGawrTridentRotation -= player.velocity.X * 0.1f;

            if (player.wings == mod.GetEquipSlot("RemiliaWings", EquipType.Wings))
                RemiliaWingsLogic();

            if (player.head == mod.GetEquipSlot("GuraGawrHeadHair", EquipType.Head) ||
                player.head == mod.GetEquipSlot("GuraGawrHeadHoodie", EquipType.Head))
                GuraGawrALogic();

            if (player.body == mod.GetEquipSlot("GuraGawrBody", EquipType.Body) &&
                player.legs == mod.GetEquipSlot("GuraGawrLeg", EquipType.Legs))
                GuraGawrTailLogic();

            if (player.head == mod.GetEquipSlot("BerserkerHead", EquipType.Head))
                BerserkerHelmerTransformationLogic();

            if (player.head == mod.GetEquipSlot("PekoraHead", EquipType.Head))
                if (vanitySpecialEffect)
                    pekoraSmoll = !pekoraSmoll;


            vanitySpecialEffect = false;
            if (vanitySpecialEffectCooldown > 0)
                vanitySpecialEffectCooldown--;
        }

        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            if(JunkoAndFriends.SpecialEffectKey.JustPressed && vanitySpecialEffectCooldown <= 0)
            {
                vanitySpecialEffect = true;
                vanitySpecialEffectCooldown = 7 * 60;
            }
        }

        public override void clientClone(ModPlayer clientClone)
        {
            JunkoAndFriendsPlayer clone = clientClone as JunkoAndFriendsPlayer;

            clone.vanitySpecialEffect = vanitySpecialEffect;
            clone.berserkerIsBerserk = berserkerIsBerserk;
            clone.berserkerDoTransformation = berserkerDoTransformation;
            clone.guraGawrDoA = guraGawrDoA;
            clone.pekoraSmoll = pekoraSmoll;
            clone.berserkerHelmetFrame = berserkerHelmetFrame;
        }

        public override void SyncPlayer(int toWho, int fromWho, bool newPlayer)
        {
            ModPacket packet = mod.GetPacket();
            packet.Write((byte)MessageType.SyncPlayer);
            packet.Write((byte)player.whoAmI);
            packet.Write(vanitySpecialEffect);
            packet.Write(berserkerIsBerserk);
            packet.Write(berserkerDoTransformation);
            packet.Write(guraGawrDoA);
            packet.Write(pekoraSmoll);
            packet.Write((byte)berserkerHelmetFrame);
            packet.Send(toWho, fromWho);
        }

        public override void SendClientChanges(ModPlayer clientPlayer)
        {
            JunkoAndFriendsPlayer clone = clientPlayer as JunkoAndFriendsPlayer;
            if (clone.vanitySpecialEffect != vanitySpecialEffect)
            {
                var packet = mod.GetPacket();
                packet.Write((byte)MessageType.SyncVanitySpecialEffect);
                packet.Write((byte)player.whoAmI);
                packet.Write(vanitySpecialEffect);
                packet.Send();
            }

            if (clone.berserkerIsBerserk != berserkerIsBerserk)
            {
                var packet = mod.GetPacket();
                packet.Write((byte)MessageType.SyncBerserkerIsBerserk);
                packet.Write((byte)player.whoAmI);
                packet.Write(berserkerIsBerserk);
                packet.Send();
            }

            if (clone.berserkerDoTransformation != berserkerDoTransformation)
            {
                var packet = mod.GetPacket();
                packet.Write((byte)MessageType.SyncBerserkerDoTransformation);
                packet.Write((byte)player.whoAmI);
                packet.Write(berserkerDoTransformation);
                packet.Send();
            }

            if (clone.guraGawrDoA != guraGawrDoA)
            {
                var packet = mod.GetPacket();
                packet.Write((byte)MessageType.SyncGuraGawrDoA);
                packet.Write((byte)player.whoAmI);
                packet.Write(guraGawrDoA);
                packet.Send();
            }

            if (clone.pekoraSmoll != pekoraSmoll)
            {
                var packet = mod.GetPacket();
                packet.Write((byte)MessageType.SyncPekoraSmoll);
                packet.Write((byte)player.whoAmI);
                packet.Write(pekoraSmoll);
                packet.Send();
            }

            if (clone.berserkerHelmetFrame != berserkerHelmetFrame)
            {
                var packet = mod.GetPacket();
                packet.Write((byte)MessageType.SyncBerserkerHelmetFrame);
                packet.Write((byte)player.whoAmI);
                packet.Write((byte)berserkerHelmetFrame);
                packet.Send();
            }
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
            if (Main.rand.Next(60000) == 0 || vanitySpecialEffect)
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
                    Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/GuraGawrA"));
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

        private void BerserkerHelmerTransformationLogic()
        {
            int berserkerHelmetFrames = 8;
            if (vanitySpecialEffect)
                berserkerDoTransformation = true;

            if (berserkerDoTransformation)
            {
                berserkerHelmetCounter++;
                if (berserkerIsBerserk)
                {
                    if (berserkerHelmetCounter > 5)
                    {
                        berserkerHelmetCounter = 0;
                        berserkerHelmetFrame--;
                        if (berserkerHelmetFrame <= 0)
                        {
                            berserkerHelmetFrame = 0;
                            berserkerDoTransformation = false;
                            berserkerIsBerserk = false;
                        }
                    }
                }
                else if (!berserkerIsBerserk)
                {
                    if (berserkerHelmetCounter > 5)
                    {
                        berserkerHelmetCounter = 0;
                        berserkerHelmetFrame++;
                        if (berserkerHelmetFrame >= berserkerHelmetFrames - 1)
                        {
                            berserkerHelmetFrame = berserkerHelmetFrames - 1;
                            berserkerDoTransformation = false;
                            berserkerIsBerserk = true;
                        }
                    }
                }
            }
        }

        public override void ModifyDrawLayers(List<PlayerLayer> layers)
        {
            int headLayer = layers.FindIndex(l => l == PlayerLayer.Head);

            if (headLayer > -1)
            {
                layers.Insert(headLayer + 1, GuraGawrA);
                layers.Insert(headLayer + 1, BerserkerHelmetBerserk);
                layers.Insert(headLayer + 1, UsadaPekoraHead);
            }

            JunkoAura.visible = true;
            layers.Insert(0, JunkoAura);
            RemiWings_Layer.visible = true;
            layers.Insert(1, RemiWings_Layer);
            GuraGawrTail.visible = true;
            layers.Insert(0, GuraGawrTail);
            GuraGawrTrident.visible = true;
            layers.Insert(0, GuraGawrTrident);
            BerserkerCape.visible = true;
            layers.Insert(0, BerserkerCape);
        }

        public override void ModifyDrawHeadLayers(List<PlayerHeadLayer> layers)
        {
            int headLayer = layers.FindIndex(l => l == PlayerHeadLayer.Armor);

            if (headLayer > -1)
            {
                layers.Insert(headLayer + 1, UsadaPekoraHeadMap);
            }
        }

        public Color saveUpperArmorColor;
        public override void ModifyDrawInfo(ref PlayerDrawInfo drawInfo)
        {
            if (drawInfo.shadow != 0)
                return;

            Player drawPlayer = drawInfo.drawPlayer;

            if (drawPlayer.legs == mod.GetEquipSlot("FlandreLeg", EquipType.Legs) ||
                drawPlayer.legs == mod.GetEquipSlot("RemiliaLeg", EquipType.Legs) ||
                drawPlayer.legs == mod.GetEquipSlot("GuraGawrLeg", EquipType.Legs) ||
                drawPlayer.legs == mod.GetEquipSlot("BerserkerLeg", EquipType.Legs) ||
                drawPlayer.legs == mod.GetEquipSlot("PekoraLeg", EquipType.Legs))
            {
                TurnLegTransparent(ref drawInfo);
            }

            if (drawPlayer.body == mod.GetEquipSlot("GuraGawrBody", EquipType.Body) ||
                drawPlayer.body == mod.GetEquipSlot("PekoraBody", EquipType.Body))
            {
                TurnBodyTransparent(ref drawInfo);
            }

            if (drawPlayer.head == mod.GetEquipSlot("GuraGawrHeadHair", EquipType.Head) ||
                drawPlayer.head == mod.GetEquipSlot("GuraGawrHeadHoodie", EquipType.Head))
            {
                TurnHeadTransparent(ref drawInfo);
            }

            if (drawPlayer.head == mod.GetEquipSlot("PekoraHead", EquipType.Head))
            {
                TurnHeadTransparent(ref drawInfo);
                saveUpperArmorColor = drawInfo.upperArmorColor;
                drawInfo.upperArmorColor = Color.Transparent;
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

            if (drawPlayer.head != mod.GetEquipSlot("JunkoHead", EquipType.Head) || drawPlayer.body != mod.GetEquipSlot("JunkoBody", EquipType.Body))
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

            if (drawPlayer.wings != mod.GetEquipSlot("RemiliaWings", EquipType.Wings))
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

            Rectangle frame1 = new Rectangle(0, drawPlayer.Friends().remiWingFrame * (texture2.Height / 7), texture2.Width, texture2.Height / 7);
            Rectangle frame2 = new Rectangle(0, drawPlayer.Friends().remiWingFrame * (texture1.Height / 7), texture1.Width, texture1.Height / 7);
            float rotation = drawPlayer.bodyRotation;

            DrawData drawDataR = new DrawData(texture2, position1, frame1, drawInfo.middleArmorColor, rotation, origin, 1f, spriteEffects, 0)
            {
                shader = drawInfo.wingShader
            };

            DrawData drawDataL = new DrawData(texture1, position2, frame2, drawInfo.middleArmorColor, rotation, origin, 1f, spriteEffects, 0)
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

            if (drawPlayer.head != mod.GetEquipSlot("GuraGawrHeadHair", EquipType.Head) && drawPlayer.head != mod.GetEquipSlot("GuraGawrHeadHoodie", EquipType.Head))
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
            if (JunkoAndFriends.TerrarianUpFrames.Contains(drawPlayer.bodyFrame.Y) && flag)
            {
                drawFaceY -= 2;
            }
            else if (JunkoAndFriends.TerrarianUpFrames.Contains(drawPlayer.bodyFrame.Y))
            {
                drawFaceY += 2;
            }

            if ((int)spriteEffects == 2 || (int)spriteEffects == 3)
            {
                drawFaceY += 6;
            }
            Vector2 origin = drawInfo.headOrigin;

            Vector2 facePosition = new Vector2(drawFaceX, drawFaceY) + drawPlayer.headPosition - Main.screenPosition;

            float faceRotation = drawPlayer.headRotation;

            Rectangle faceFrame = new Rectangle(0, drawPlayer.Friends().guraGawrAFrame * (faceTexture.Height / 7), faceTexture.Width, faceTexture.Height / 7);

            DrawData faceDrawData = new DrawData(faceTexture, facePosition, faceFrame, drawInfo.upperArmorColor * (drawPlayer.Friends().guraGawrDoA ? 1f : 0f), faceRotation, new Vector2(faceTexture.Width / 2f, faceTexture.Height / 2f), 1f, spriteEffects, 0)
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

            if (drawPlayer.body != mod.GetEquipSlot("GuraGawrBody", EquipType.Body) || drawPlayer.legs != mod.GetEquipSlot("GuraGawrLeg", EquipType.Legs))
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

            float tailRotation = drawPlayer.bodyRotation;

            Rectangle tailFrame = new Rectangle(0, drawPlayer.Friends().guraGawrTailFrame * (tailTexture.Height / 4), tailTexture.Width, tailTexture.Height / 4);

            DrawData tailDrawData = new DrawData(tailTexture, tailPosition, tailFrame, drawInfo.middleArmorColor, tailRotation, origin, 1f, spriteEffects, 0)
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

            if (drawPlayer.body != mod.GetEquipSlot("GuraGawrBody", EquipType.Body) ||
            drawPlayer.legs != mod.GetEquipSlot("GuraGawrLeg", EquipType.Legs) ||
            (drawPlayer.head != mod.GetEquipSlot("GuraGawrHeadHair", EquipType.Head) && drawPlayer.head != mod.GetEquipSlot("GuraGawrHeadHoodie", EquipType.Head)))
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

            float rotation = 0f;
            if ((int)spriteEffects == 0 || (int)spriteEffects == 3)
                rotation = 1f;
            else
                rotation = -1f;

            float tridentRotation = drawPlayer.bodyRotation + rotation + drawPlayer.Friends().guraGawrTridentRotation;

            DrawData tridentDrawData = new DrawData(tridentTexture, tridentPosition, null, drawInfo.middleArmorColor, tridentRotation, new Vector2(tridentTexture.Width / 2f, tridentTexture.Height / 2f), 1f, spriteEffects, 0)
            {
                shader = drawInfo.bodyArmorShader
            };

            Main.playerDrawData.Add(tridentDrawData);
        });

        public static readonly PlayerLayer BerserkerHelmetBerserk = new PlayerLayer("JunkoAndFriends", "BerserkerHelmetBerserk", PlayerLayer.Head, delegate (PlayerDrawInfo drawInfo)
        {
            if (drawInfo.shadow != 0 || drawInfo.drawPlayer.dead)
                return;

            Player drawPlayer = drawInfo.drawPlayer;
            Mod mod = ModLoader.GetMod("JunkoAndFriends");

            if (drawPlayer.head != mod.GetEquipSlot("BerserkerHead", EquipType.Head))
                return;

            Texture2D helmetTexture = mod.GetTexture("ExtraTextures/BerserkerHelmet");
            Texture2D glowmaskTexture = mod.GetTexture("ExtraTextures/BerserkerHelmet_Glowmask");
            Texture2D scarfTexture = mod.GetTexture("ExtraTextures/BerserkerScarf");

            float drawHelmetX = (int)drawInfo.position.X + drawPlayer.width / 2 + 2;
            float drawHelmetY = (int)drawInfo.position.Y + drawPlayer.height + 48;

            float drawScarfX = (int)drawInfo.position.X + drawPlayer.width / 2;
            float drawScarfY = (int)drawInfo.position.Y + drawPlayer.height + 58;

            SpriteEffects spriteEffects = drawInfo.spriteEffects;
            
            switch ((int)spriteEffects)
            {
                case 1:
                case 3:
                    drawHelmetX -= 4;
                    drawScarfX += 4;
                    break;
            }

            bool flag = ((int)spriteEffects == 0 || (int)spriteEffects == 1);
            if (JunkoAndFriends.TerrarianUpFrames.Contains(drawPlayer.bodyFrame.Y) && flag)
            {
                drawHelmetY -= 2;
                drawScarfY -= 2;
            }
            else if (JunkoAndFriends.TerrarianUpFrames.Contains(drawPlayer.bodyFrame.Y))
            {
                drawHelmetY += 2;
                drawScarfY += 2;
            }

            if ((int)spriteEffects == 2 || (int)spriteEffects == 3)
            {
                drawHelmetY += 10;
            }
            Vector2 origin = drawInfo.headOrigin;

            Vector2 helmetPosition = new Vector2(drawHelmetX, drawHelmetY) + drawPlayer.headPosition - Main.screenPosition;
            Vector2 scarfPosition = new Vector2(drawScarfX, drawScarfY) + drawPlayer.headPosition - Main.screenPosition;

            float helmetRotation = drawPlayer.headRotation;

            Rectangle helmetFrame = new Rectangle(0, drawPlayer.Friends().berserkerHelmetFrame * (helmetTexture.Height / 8), helmetTexture.Width, helmetTexture.Height / 8);
            Rectangle scarfFrame = new Rectangle(0, 0, scarfTexture.Width, scarfTexture.Height);
            DrawData helmetDrawData = new DrawData(helmetTexture, helmetPosition, helmetFrame, drawInfo.upperArmorColor * (drawPlayer.Friends().berserkerHelmetFrame > 0 ? 1f : 0f), helmetRotation, new Vector2(helmetTexture.Width / 2f, helmetTexture.Height / 2f), 1f, spriteEffects, 0)
            {
                shader = drawInfo.headArmorShader
            };

            DrawData glowmaskDrawData = new DrawData(glowmaskTexture, helmetPosition, helmetFrame, Color.White * (drawPlayer.Friends().berserkerHelmetFrame > 0 ? 1f : 0f), helmetRotation, new Vector2(helmetTexture.Width / 2f, helmetTexture.Height / 2f), 1f, spriteEffects, 0)
            {
                shader = drawInfo.headArmorShader
            };

            DrawData scarfDrawData = new DrawData(scarfTexture, scarfPosition, scarfFrame, drawInfo.middleArmorColor * (drawPlayer.Friends().berserkerHelmetFrame == 0 ? 1f : 0f), helmetRotation, new Vector2(helmetTexture.Width / 2f, helmetTexture.Height / 2f), 1f, spriteEffects, 0)
            {
                shader = drawInfo.headArmorShader
            };

            Main.playerDrawData.Add(helmetDrawData);
            Main.playerDrawData.Add(glowmaskDrawData);
            Main.playerDrawData.Add(scarfDrawData);
        });

        public static readonly PlayerLayer BerserkerCape = new PlayerLayer("JunkoAndFriends", "BerserkerCape", PlayerLayer.MiscEffectsBack, delegate (PlayerDrawInfo drawInfo)
        {
            if (drawInfo.shadow != 0 || drawInfo.drawPlayer.dead)
                return;

            Player drawPlayer = drawInfo.drawPlayer;
            Mod mod = ModLoader.GetMod("JunkoAndFriends");

            if (drawPlayer.body != mod.GetEquipSlot("BerserkerBody", EquipType.Body))
                return;

            Texture2D capeTexture = mod.GetTexture("ExtraTextures/BerserkerCape");

            float drawX = drawInfo.position.X + drawPlayer.width / 2f;
            float drawY = drawInfo.position.Y + drawPlayer.height / 2f - 4;

            SpriteEffects spriteEffects = drawInfo.spriteEffects;
            
            if ((int)spriteEffects == 0 || (int)spriteEffects == 2)
                drawX -= 12;
            else
                drawX += 12;

            if ((int)spriteEffects == 2 || (int)spriteEffects == 3)
                drawY -= 6;

            if ((int)spriteEffects == 2)
                spriteEffects = (SpriteEffects)0;

            if ((int)spriteEffects == 3)
                spriteEffects = (SpriteEffects)1;

            Vector2 origin = drawInfo.bodyOrigin;

            Vector2 capePosition = new Vector2(drawX, drawY) + drawPlayer.bodyPosition - Main.screenPosition;

            float capeRotation = drawPlayer.bodyRotation + drawPlayer.Friends().berserkerCapeRotation;

            DrawData capeDrawData = new DrawData(capeTexture, capePosition, null, drawInfo.middleArmorColor, capeRotation, new Vector2(capeTexture.Width / 2f, 0f), 1f, spriteEffects, 0)
            {
                shader = drawInfo.bodyArmorShader
            };

            Main.playerDrawData.Add(capeDrawData);
        });

        public static readonly PlayerLayer UsadaPekoraHead = new PlayerLayer("JunkoAndFriends", "UsadaPekoraHead", PlayerLayer.Head, delegate (PlayerDrawInfo drawInfo)
        {
            if (drawInfo.shadow != 0 || drawInfo.drawPlayer.dead)
                return;

            Player drawPlayer = drawInfo.drawPlayer;
            Mod mod = ModLoader.GetMod("JunkoAndFriends");

            if (drawPlayer.head != mod.GetEquipSlot("PekoraHead", EquipType.Head))
                return;

            Texture2D headTexture = mod.GetTexture("ExtraTextures/UsadaPekoraHead");

            float drawHeadX = (int)drawInfo.position.X + drawPlayer.width / 2;
            float drawHeadY = (int)drawInfo.position.Y + drawPlayer.height - 34;
            if (drawPlayer.Friends().pekoraSmoll)
                drawHeadY += 18;

            SpriteEffects spriteEffects = drawInfo.spriteEffects;

            bool flag = ((int)spriteEffects == 0 || (int)spriteEffects == 1);
            if (JunkoAndFriends.TerrarianUpFrames.Contains(drawPlayer.bodyFrame.Y) && flag)
            {
                drawHeadY -= 2;
            }
            else if (JunkoAndFriends.TerrarianUpFrames.Contains(drawPlayer.bodyFrame.Y))
            {
                drawHeadY += 2;
            }

            if ((int)spriteEffects == 2 || (int)spriteEffects == 3)
            {
                drawHeadY += 20;
                if (drawPlayer.Friends().pekoraSmoll)
                    drawHeadY -= 18 * 2;
            }
            Vector2 origin = drawInfo.headOrigin;

            Vector2 headPosition = new Vector2(drawHeadX, drawHeadY) + drawPlayer.headPosition - Main.screenPosition;

            float headRotation = drawPlayer.headRotation;

            Rectangle headFrame = new Rectangle(0, 0, headTexture.Width, headTexture.Height);
            
            DrawData headDrawData = new DrawData(headTexture, headPosition, headFrame, drawPlayer.Friends().saveUpperArmorColor, headRotation, new Vector2(headTexture.Width / 2f, headTexture.Height / 2f), 1f, spriteEffects, 0)
            {
                shader = drawInfo.headArmorShader
            };

            Main.playerDrawData.Add(headDrawData);
        });

        public static readonly PlayerHeadLayer UsadaPekoraHeadMap = new PlayerHeadLayer("JunkoAndFriends", "UsadaPekoraHeadMap", delegate (PlayerHeadDrawInfo drawInfo)
        {
            Player drawPlayer = drawInfo.drawPlayer;
            Mod mod = ModLoader.GetMod("JunkoAndFriends");

            if (drawPlayer.head != mod.GetEquipSlot("PekoraHead", EquipType.Head))
                return;

            Texture2D texture = mod.GetTexture("ExtraTextures/UsadaPekoraHead");
            float drawX = (int)(drawPlayer.position.X - Main.screenPosition.X - drawPlayer.bodyFrame.Width / 2 +
                                 drawPlayer.width / 2);
            float drawY = (int)(drawPlayer.position.Y - Main.screenPosition.Y + drawPlayer.height -
                drawPlayer.bodyFrame.Height + 4);

            Vector2 position = new Vector2(drawX, drawY) + drawPlayer.headPosition + drawInfo.drawOrigin;

            Rectangle frame = texture.Frame();

            float rotation = drawPlayer.headRotation;

            Vector2 origin = drawInfo.drawOrigin;

            float scale = drawInfo.scale;

            SpriteEffects spriteEffects = drawInfo.spriteEffects;

            DrawData drawData = new DrawData(texture, position, frame, drawInfo.armorColor, rotation, origin, scale,
                spriteEffects, 0);

            GameShaders.Armor.Apply(drawInfo.armorShader, drawPlayer, drawData);

            drawData.Draw(Main.spriteBatch);

            Main.pixelShader.CurrentTechnique.Passes[0].Apply();
        });
    }
}
