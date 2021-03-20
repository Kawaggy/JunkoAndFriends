using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JunkoAndFriends.Items.UsadaPekoraVanity
{
	[AutoloadEquip(EquipType.Legs)]
	public class PekoraLeg : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Usada Pekora's Shoes");
		}

		public override void SetDefaults()
		{
			item.width = 36;
			item.height = 24;
			item.rare = ItemRarityID.Green;
			item.vanity = true;
			item.value = Item.buyPrice(gold: 1);
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.BunnyBanner, 1);
			recipe.AddTile(TileID.Solidifier);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
