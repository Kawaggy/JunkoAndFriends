using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JunkoAndFriends.Items.FlandreVanity
{
	[AutoloadEquip(EquipType.Head)]
    public class FlandreHead : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Flandre's Cap");
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
