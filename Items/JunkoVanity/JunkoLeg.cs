using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JunkoAndFriends.Items.JunkoVanity
{
    public class JunkoLeg :  ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Compensation");
            Tooltip.SetDefault("Sell to any town NPC for compensation");
        }

        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 26;
            item.value = Item.sellPrice(gold: 4);
            item.rare = ItemRarityID.Gray;
        }
    }
}
