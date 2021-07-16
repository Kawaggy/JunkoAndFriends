using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace JunkoAndFriends.Items.RemiliaVanity
{
    public class RemiliaVanityExtra
    {
        public static readonly PlayerLayer RemiliaWings = new PlayerLayer("JunkoAndFriends", "RemiWings_Layer", PlayerLayer.MiscEffectsBack, delegate (PlayerDrawInfo drawInfo)
        {
            if (drawInfo.shadow != 0 || drawInfo.drawPlayer.dead)
                return;

            Player drawPlayer = drawInfo.drawPlayer;
            Mod mod = ModLoader.GetMod("JunkoAndFriends");

            if (drawPlayer.wings != mod.GetEquipSlot("RemiliaWings", EquipType.Wings))
                return;

            Texture2D texture1 = mod.GetTexture("ExtraTextures/RemiliaWings_L");
            Texture2D texture2 = mod.GetTexture("ExtraTextures/RemiliaWings_R");
            SpriteEffects spriteEffects = drawInfo.spriteEffects;
            float drawX1 = (int)drawInfo.position.X + drawPlayer.width + 14;
            float drawY1 = (int)drawInfo.position.Y + drawPlayer.height - 5;

            float drawX2 = (int)drawInfo.position.X - 8;
            float drawY2 = (int)drawInfo.position.Y + drawPlayer.height - 10;

            if (spriteEffects == SpriteEffects.FlipHorizontally || (int)spriteEffects == 3)
            {
                drawX1 = (int)drawInfo.position.X + 8;
                drawY1 = (int)drawInfo.position.Y + drawPlayer.height - 5;

                drawX2 = (int)drawInfo.position.X + drawPlayer.width + 14 + 8;
                drawY2 = (int)drawInfo.position.Y + drawPlayer.height - 10;
            }

            Vector2 origin = drawInfo.bodyOrigin;
            Vector2 position1 = new Vector2(drawX1, drawY1) + drawPlayer.bodyPosition - Main.screenPosition;
            Vector2 position2 = new Vector2(drawX2, drawY2) + drawPlayer.bodyPosition - Main.screenPosition;

            Rectangle frame1 = new Rectangle(0, drawPlayer.Friends().remiWingFrame * (texture2.Height / 7), texture2.Width, texture2.Height / 7);
            Rectangle frame2 = new Rectangle(0, drawPlayer.Friends().remiWingFrame * (texture1.Height / 7), texture1.Width, texture1.Height / 7);
            float rotation = drawPlayer.bodyRotation;

            DrawData drawDataR = new DrawData(texture2, position1, frame1, drawInfo.middleArmorColor, rotation, origin, 1f, spriteEffects, 0)
            {
                shader = drawInfo.wingShader
            };

            DrawData drawDataL = new DrawData(texture1, position2, frame2, drawInfo.middleArmorColor, rotation, origin, 1f, spriteEffects, 0)
            {
                shader = drawInfo.wingShader
            };

            Main.playerDrawData.Add(drawDataR);
            Main.playerDrawData.Add(drawDataL);
        });
    }
}
