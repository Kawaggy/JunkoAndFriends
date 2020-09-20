using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JunkoAndFriends.Items.GuraGawrVanity
{
    [AutoloadEquip(EquipType.Legs)]
    public class GuraGawrLeg : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gura Gawr's Boots");
        }

        public override void SetDefaults()
        {
            item.width = 68;
            item.height = 32;
            item.rare = ItemRarityID.Green;
            item.vanity = true;
            item.value = Item.buyPrice(gold: 1);
        }
    }
}
