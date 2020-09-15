using JunkoAndFriends.Items.FlandreVanity;
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
        }
    }
}
