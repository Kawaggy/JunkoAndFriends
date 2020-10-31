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
			DisplayName.SetDefault("Junko's Dress");
		}

		public override void SetDefaults()
		{
			item.width = 42;
			item.height = 42;
			item.rare = ItemRarityID.Green;
			item.vanity = true;
			item.value = Item.buyPrice(gold: 1);
		}

        public override void SetMatch(bool male, ref int equipSlot, ref bool robes)
        {
			robes = true;
			equipSlot = mod.GetEquipSlot("Junko_Legs", EquipType.Legs);
        }
    }
}
