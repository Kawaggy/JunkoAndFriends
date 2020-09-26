using JunkoAndFriends.Items.FlandreVanity;
using JunkoAndFriends.Items.GuraGawrVanity;
using JunkoAndFriends.Items.JunkoVanity;
using JunkoAndFriends.Items.RemiliaVanity;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JunkoAndFriends
{
    public class JunkoAndFriendsGlobalNPC : GlobalNPC
    {
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
        }
    }
}
