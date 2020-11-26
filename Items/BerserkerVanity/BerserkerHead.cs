using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JunkoAndFriends.Items.BerserkerVanity
{
	[AutoloadEquip(EquipType.Head)]
	public class BerserkerHead : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Berserker Helmet");
		}

		public override void SetDefaults()
		{
			item.width = 28;
			item.height = 28;
			item.rare = ItemRarityID.Green;
			item.vanity = true;
			item.value = Item.buyPrice(gold: 1);
		}

		public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            TooltipLine line = new TooltipLine(mod, "BerserkerTransform", 
				$"Press the {JunkoAndFriends.SpecialEffectKey.UsedKeys()} key to go into Berserker Mode");
            tooltips.Add(line);
        }
    }
}
