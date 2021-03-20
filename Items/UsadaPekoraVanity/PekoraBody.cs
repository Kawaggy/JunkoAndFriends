using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JunkoAndFriends.Items.UsadaPekoraVanity
{
	[AutoloadEquip(EquipType.Body)]
	public class PekoraBody : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Usada Pekora's Dress");
		}

		public override void SetDefaults()
		{
			item.width = 42;
			item.height = 36;
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
