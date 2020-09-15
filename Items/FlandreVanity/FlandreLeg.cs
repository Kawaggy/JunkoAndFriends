using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JunkoAndFriends.Items.FlandreVanity
{
	[AutoloadEquip(EquipType.Legs)]
    public class FlandreLeg : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Flandre's Shoes");
		}

		public override void SetDefaults()
		{
			item.width = 36;
			item.height = 24;
			item.rare = ItemRarityID.Green;
			item.vanity = true;
			item.value = Item.buyPrice(gold: 1);
		}
	}
}
