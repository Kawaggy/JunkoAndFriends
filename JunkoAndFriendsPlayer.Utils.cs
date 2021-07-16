using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace JunkoAndFriends
{
    public partial class JunkoAndFriendsPlayer : ModPlayer
    {
        public Color saveUpperArmorColor;

        internal void TurnFullyTransparent(ref PlayerDrawInfo drawInfo)
        {
            drawInfo.armGlowMaskColor = Color.Transparent;
            drawInfo.bodyColor = Color.Transparent;
            drawInfo.bodyGlowMaskColor = Color.Transparent;
            drawInfo.eyeColor = Color.Transparent;
            drawInfo.eyeWhiteColor = Color.Transparent;
            drawInfo.faceColor = Color.Transparent;
            drawInfo.hairColor = Color.Transparent;
            drawInfo.headGlowMaskColor = Color.Transparent;
            drawInfo.legColor = Color.Transparent;
            drawInfo.legGlowMaskColor = Color.Transparent;
            drawInfo.lowerArmorColor = Color.Transparent;
            drawInfo.middleArmorColor = Color.Transparent;
            drawInfo.pantsColor = Color.Transparent;
            drawInfo.shirtColor = Color.Transparent;
            drawInfo.shoeColor = Color.Transparent;
            drawInfo.underShirtColor = Color.Transparent;
            drawInfo.upperArmorColor = Color.Transparent;
        }

        internal void TurnHeadTransparent(ref PlayerDrawInfo drawInfo)
        {
            drawInfo.eyeColor = Color.Transparent;
            drawInfo.eyeWhiteColor = Color.Transparent;
            drawInfo.faceColor = Color.Transparent;
            drawInfo.hairColor = Color.Transparent;
            drawInfo.headGlowMaskColor = Color.Transparent;
        }

        internal void TurnBodyTransparent(ref PlayerDrawInfo drawInfo)
        {
            drawInfo.armGlowMaskColor = Color.Transparent;
            drawInfo.bodyColor = Color.Transparent;
            drawInfo.bodyGlowMaskColor = Color.Transparent;
            drawInfo.shirtColor = Color.Transparent;
            drawInfo.underShirtColor = Color.Transparent;
        }

        internal void TurnLegTransparent(ref PlayerDrawInfo drawInfo)
        {
            drawInfo.legColor = Color.Transparent;
            drawInfo.legGlowMaskColor = Color.Transparent;
            drawInfo.pantsColor = Color.Transparent;
        }
    }
}
