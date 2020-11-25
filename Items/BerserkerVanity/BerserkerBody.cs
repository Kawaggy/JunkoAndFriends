using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JunkoAndFriends.Items.BerserkerVanity
{
	[AutoloadEquip(EquipType.Body)]
	public class BerserkerBody : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Berserker Body Armor");
		}

		public override void SetDefaults()
		{
			item.width = 46;
			item.height = 50;
			item.rare = ItemRarityID.Green;
			item.vanity = true;
			item.value = Item.buyPrice(gold: 1);
		}
	}
}
