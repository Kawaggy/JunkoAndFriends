using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JunkoAndFriends.Items.RemiliaVanity
{
	[AutoloadEquip(EquipType.Head)]
    public class RemiliaHead : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Remilia's Cap");
		}

		public override void SetDefaults()
		{
			item.width = 28;
			item.height = 18;
			item.rare = ItemRarityID.Green;
			item.vanity = true;
			item.value = Item.buyPrice(gold: 1);
		}
	}
}
