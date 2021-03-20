using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JunkoAndFriends.Items.AmeliaVanity
{
	[AutoloadEquip(EquipType.Body)]
	public class AmeliaBody : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Amelia Watson's Clothes");
		}

		public override void SetDefaults()
		{
			item.width = 40;
			item.height = 38;
			item.rare = ItemRarityID.Green;
			item.vanity = true;
			item.value = Item.buyPrice(gold: 1);
		}

		public override void SetMatch(bool male, ref int equipSlot, ref bool robes)
		{
			robes = true;
			equipSlot = mod.GetEquipSlot("AmeliaLeg_Legs", EquipType.Legs);
		}
	}
}
