using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JunkoAndFriends.Items.UsadaPekoraVanity
{
	[AutoloadEquip(EquipType.Head)]
	public class PekoraHead : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Usada Pekora Wig");
		}

		public override void SetDefaults()
		{
			item.width = 53;
			item.height = 60;
			item.rare = ItemRarityID.Green;
			item.vanity = true;
			item.value = Item.buyPrice(gold: 1);
		}

		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			TooltipLine line = new TooltipLine(mod, "PekoraSmoll",
				$"Press the {JunkoAndFriends.SpecialEffectKey.UsedKeys()} key to become smoll");
			tooltips.Add(line);
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
