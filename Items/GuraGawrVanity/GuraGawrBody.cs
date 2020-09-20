using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JunkoAndFriends.Items.GuraGawrVanity
{
    [AutoloadEquip(EquipType.Body)]
    public class GuraGawrBody : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Gura Gawr's Jacket");
		}

		public override void SetDefaults()
		{
			item.width = 44;
			item.height = 48;
			item.rare = ItemRarityID.Green;
			item.vanity = true;
			item.value = Item.buyPrice(gold: 1);
		}
	}
}
