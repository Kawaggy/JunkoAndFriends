using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;

namespace JunkoAndFriends
{
    public partial class JunkoAndFriendsPlayer : ModPlayer
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
    }
}
