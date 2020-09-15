using Terraria.ID;
using Terraria.ModLoader;

namespace JunkoAndFriends.Items
{
    public class WaveShaderDye : ModItem
    {
		public override void SetDefaults()
		{
			byte dye = item.dye;
			item.CloneDefaults(ItemID.GelDye);
			item.dye = dye;
		}
	}
}
