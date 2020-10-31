using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JunkoAndFriends.Items.MoriVanity
{
    [AutoloadEquip(EquipType.Head)]
    public class MoriHead : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mori Calliope's Crown");
        }

        public override void SetDefaults()
        {
            item.width = 50;
            item.height = 30;
            item.rare = ItemRarityID.Green;
            item.vanity = true;
            item.value = Item.buyPrice(gold: 1);
        }
    }
}
