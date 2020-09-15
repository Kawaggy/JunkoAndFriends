using JunkoAndFriends.Items;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace JunkoAndFriends
{
	public class JunkoAndFriends : Mod
	{
        internal static Mod junkoVanity;
        internal static JunkoAndFriends Instance;

        public override void Load()
        {
            Instance = this;
            junkoVanity = ModLoader.GetMod("JunkoVanity");
            if (!Main.dedServ)
            {
                AddEquipTexture(null, EquipType.Legs, "JunkoVanity_Legs", "JunkoAndFriends/Items/JunkoVanity/JunkoLeg_Legs");
                GameShaders.Armor.BindShader(ModContent.ItemType<WaveShaderDye>(), new ArmorShaderData(new Ref<Effect>(GetEffect("Effects/WaveShader")), "WaveShaderPass"));
            }
        }

        public override void Unload()
        {
            Instance = null;
            junkoVanity = null;
        }
    }
}