using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JunkoAndFriends.Items.JunkoVanity
{
    [AutoloadEquip(EquipType.Legs)]
    public class JunkoLeg : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Junko's Legs");
		}

		public override void SetDefaults()
		{
			item.width = 32;
			item.height = 12;
			item.rare = ItemRarityID.Green;
			item.vanity = true;
			item.value = Item.buyPrice(gold: 1);
		}
	}
}
