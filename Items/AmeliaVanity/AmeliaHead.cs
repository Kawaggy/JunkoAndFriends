using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JunkoAndFriends.Items.AmeliaVanity
{
	[AutoloadEquip(EquipType.Head)]
	public class AmeliaHead : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Amelia Watson's Cap");
		}

		public override void SetDefaults()
		{
			item.width = 34;
			item.height = 32;
			item.rare = ItemRarityID.Green;
			item.vanity = true;
			item.value = Item.buyPrice(gold: 1);
		}
	}
}
