using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JunkoAndFriends.Items.JunkoVanity
{
    [AutoloadEquip(EquipType.Head)]
    public class JunkoHead : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Junko's Head");
		}

		public override void SetDefaults()
		{
			item.width = 30;
			item.height = 42;
			item.rare = ItemRarityID.Green;
			item.vanity = true;
			item.value = Item.buyPrice(gold: 1);
		}
	}
}
