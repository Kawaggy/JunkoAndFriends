using JunkoAndFriends.Items.AmeliaVanity;
using JunkoAndFriends.Items.FlandreVanity;
using JunkoAndFriends.Items.GuraGawrVanity;
using JunkoAndFriends.Items.JunkoVanity;
using JunkoAndFriends.Items.MoriVanity;
using JunkoAndFriends.Items.RemiliaVanity;
using JunkoAndFriends.Items.UsadaPekoraVanity;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JunkoAndFriends
{
    public class JunkoAndFriendsGlobalNPC : GlobalNPC
    {
        private const float vanityDrop = 0.01f;
        public override void SetupShop(int type, Chest shop, ref int nextSlot)
        {
            if (type == NPCID.Clothier)
            {
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<JunkoHead>());
                ++nextSlot;
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<JunkoBody>());
                ++nextSlot;

                shop.item[nextSlot].SetDefaults(ModContent.ItemType<FlandreHead>());
                ++nextSlot;
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<FlandreBody>());
                ++nextSlot;
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<FlandreLeg>());
                ++nextSlot;

                shop.item[nextSlot].SetDefaults(ModContent.ItemType<RemiliaHead>());
                ++nextSlot;
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<RemiliaBody>());
                ++nextSlot;
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<RemiliaLeg>());
                ++nextSlot;

                if (NPC.killCount[Item.NPCtoBanner(NPCID.Bunny)] >= 150)
                {
                    shop.item[nextSlot].SetDefaults(ModContent.ItemType<PekoraHead>());
                    ++nextSlot;
                    shop.item[nextSlot].SetDefaults(ModContent.ItemType<PekoraBody>());
                    ++nextSlot;
                    shop.item[nextSlot].SetDefaults(ModContent.ItemType<PekoraLeg>());
                    ++nextSlot;
                }

                shop.item[nextSlot].SetDefaults(ModContent.ItemType<AmeliaHead>());
                ++nextSlot;
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<AmeliaBody>());
                ++nextSlot;

                if (JunkoAndFriendsWorld.eclipsePassed)
                {
                    shop.item[nextSlot].SetDefaults(ModContent.ItemType<MoriHead>());
                    ++nextSlot;
                    shop.item[nextSlot].SetDefaults(ModContent.ItemType<MoriBody>());
                    ++nextSlot;
                }
            }

            if (type == NPCID.Pirate)
            {
                if (NPC.downedMoonlord)
                {
                    shop.item[nextSlot].SetDefaults(ModContent.ItemType<GuraGawrHeadHair>());
                    ++nextSlot;
                    shop.item[nextSlot].SetDefaults(ModContent.ItemType<GuraGawrHeadHoodie>());
                    ++nextSlot;
                    shop.item[nextSlot].SetDefaults(ModContent.ItemType<GuraGawrBody>());
                    ++nextSlot;
                    shop.item[nextSlot].SetDefaults(ModContent.ItemType<GuraGawrLeg>());
                    ++nextSlot;
                }
            }
        }

        public override void NPCLoot(NPC npc)
        {
            if (npc.type == NPCID.Shark)
            {
                float chance = NPC.downedPlantBoss ? 10f : 1f;
                bool flag = Main.rand.NextBool();
                if (Main.rand.Next(9927) == 0)
                {
                    if (flag)
                        Item.NewItem(npc.Hitbox, ModContent.ItemType<GuraGawrHeadHair>());
                    else
                        Item.NewItem(npc.Hitbox, ModContent.ItemType<GuraGawrHeadHoodie>());
                    Item.NewItem(npc.Hitbox, ModContent.ItemType<GuraGawrBody>());
                    Item.NewItem(npc.Hitbox, ModContent.ItemType<GuraGawrLeg>());
                }
                else
                {
                    if (Main.rand.NextFloat() < 0.009f * chance && flag)
                    {
                        Item.NewItem(npc.Hitbox, ModContent.ItemType<GuraGawrHeadHair>());
                    }
                    if (Main.rand.NextFloat() < 0.009f * chance && !flag)
                    {
                        Item.NewItem(npc.Hitbox, ModContent.ItemType<GuraGawrHeadHoodie>());
                    }
                    if (Main.rand.NextFloat() < 0.002f * chance)
                    {
                        Item.NewItem(npc.Hitbox, ModContent.ItemType<GuraGawrBody>());
                    }
                    if (Main.rand.NextFloat() < 0.007f * chance)
                    {
                        Item.NewItem(npc.Hitbox, ModContent.ItemType<GuraGawrLeg>());
                    }
                }
            }

            if (npc.type == NPCID.Reaper)
            {
                if (Main.rand.NextFloat() < vanityDrop)
                {
                    switch(Main.rand.Next(2))
                    {
                        case 0:
                            Item.NewItem(npc.Hitbox, ModContent.ItemType<MoriHead>());
                            break;
                        case 1:
                            Item.NewItem(npc.Hitbox, ModContent.ItemType<MoriBody>());
                            break;
                    }
                }
            }

            if (npc.type == NPCID.Bunny)
            {
                if (NPC.killCount[Item.NPCtoBanner(NPCID.Bunny)] % 100 == 0)
                {
                    switch (Main.rand.Next(3))
                    {
                        case 0:
                            Item.NewItem(npc.Hitbox, ModContent.ItemType<PekoraHead>());
                            break;
                        case 1:
                            Item.NewItem(npc.Hitbox, ModContent.ItemType<PekoraBody>());
                            break;
                        case 2:
                            Item.NewItem(npc.Hitbox, ModContent.ItemType<PekoraLeg>());
                            break;
                    }
                }
                else if (Main.rand.NextFloat() < vanityDrop)
                {
                    switch (Main.rand.Next(3))
                    {
                        case 0:
                            Item.NewItem(npc.Hitbox, ModContent.ItemType<PekoraHead>());
                            break;
                        case 1:
                            Item.NewItem(npc.Hitbox, ModContent.ItemType<PekoraBody>());
                            break;
                        case 2:
                            Item.NewItem(npc.Hitbox, ModContent.ItemType<PekoraLeg>());
                            break;
                    }
                }
            }
        }
    }
}
