using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JunkoAndFriends.Items.BerserkerVanity
{
	[AutoloadEquip(EquipType.Legs)]
    public class BerserkerLeg : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Berserker Leggings");
		}

		public override void SetDefaults()
		{
			item.width = 36;
			item.height = 44;
			item.rare = ItemRarityID.Green;
			item.vanity = true;
			item.value = Item.sellPrice(gold: 2);
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.HallowedBar, 3);
			recipe.AddIngredient(ItemID.SoulofFright, 1);
			recipe.AddTile(TileID.DemonAltar);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
