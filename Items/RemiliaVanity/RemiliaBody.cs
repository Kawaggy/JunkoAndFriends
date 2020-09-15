using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JunkoAndFriends.Items.RemiliaVanity
{
    [AutoloadEquip(EquipType.Body)]
    public class RemiliaBody : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Remilia's Dress");
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
