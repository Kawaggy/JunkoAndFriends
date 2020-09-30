﻿using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JunkoAndFriends.Items.GuraGawrVanity
{
    [AutoloadEquip(EquipType.Head)]
    public class GuraGawrHeadHair : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gura Gawr's Wig");
        }

        public override void SetDefaults()
        {
            item.width = 42;
            item.height = 38;
            item.rare = ItemRarityID.Green;
            item.vanity = true;
            item.value = Item.buyPrice(gold: 1);
        }
    }
}