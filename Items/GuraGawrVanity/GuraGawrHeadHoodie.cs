using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JunkoAndFriends.Items.GuraGawrVanity
{
    [AutoloadEquip(EquipType.Head)]
    public class GuraGawrHeadHoodie : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gura Gawr's Hoodie");
        }

        public override void SetDefaults()
        {
            item.width = 50;
            item.height = 38;
            item.rare = ItemRarityID.Green;
            item.vanity = true;
            item.value = Item.buyPrice(gold: 1);
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            TooltipLine line = new TooltipLine(mod, "GuraA", 
                $"Press the {JunkoAndFriends.SpecialEffectKey.HotkeyString()} key to A");
            tooltips.Add(line);
        }
    }
}
