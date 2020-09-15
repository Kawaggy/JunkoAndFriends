using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JunkoAndFriends.Items.JunkoVanity
{
    [AutoloadEquip(EquipType.Body)]
    public class JunkoBody : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Junko's Body");
		}

		public override void SetDefaults()
		{
			item.width = 28;
			item.height = 20;
			item.rare = ItemRarityID.Green;
			item.vanity = true;
			item.value = Item.buyPrice(gold: 1);
		}
	}
}
