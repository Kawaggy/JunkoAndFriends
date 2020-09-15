using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JunkoAndFriends.Items.FlandreVanity
{
	[AutoloadEquip(EquipType.Body)]
	public class FlandreBody : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Flandre's Dress");
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
