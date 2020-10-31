using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JunkoAndFriends.Items.MoriVanity
{
	[AutoloadEquip(EquipType.Body)]
	public class MoriBody : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Mori Calliope's Dress");
		}

		public override void SetDefaults()
		{
			item.width = 54;
			item.height = 52;
			item.rare = ItemRarityID.Green;
			item.vanity = true;
			item.value = Item.buyPrice(gold: 1);
		}

		public override void SetMatch(bool male, ref int equipSlot, ref bool robes)
		{
			robes = true;
			equipSlot = mod.GetEquipSlot("MoriLeg_Legs", EquipType.Legs);
		}
	}
}
