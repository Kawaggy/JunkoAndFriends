using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace JunkoAndFriends.Items.UsadaPekoraVanity
{
    public class UsadaPekoraVanityExtra
    {
        public static readonly PlayerLayer UsadaPekoraHead = new PlayerLayer("JunkoAndFriends", "UsadaPekoraHead", PlayerLayer.Head, delegate (PlayerDrawInfo drawInfo)
        {
            if (drawInfo.shadow != 0 || drawInfo.drawPlayer.dead)
                return;

            Player drawPlayer = drawInfo.drawPlayer;
            Mod mod = ModLoader.GetMod("JunkoAndFriends");

            if (drawPlayer.head != mod.GetEquipSlot("PekoraHead", EquipType.Head))
                return;

            Texture2D headTexture = mod.GetTexture("ExtraTextures/UsadaPekoraHead");

            float drawHeadX = (int)drawInfo.position.X + drawPlayer.width / 2;
            float drawHeadY = (int)drawInfo.position.Y + drawPlayer.height - 34;
            if (drawPlayer.Friends().pekoraSmoll)
                drawHeadY += 18;

            SpriteEffects spriteEffects = drawInfo.spriteEffects;

            bool flag = ((int)spriteEffects == 0 || (int)spriteEffects == 1);
            if (JunkoAndFriends.TerrarianUpFrames.Contains(drawPlayer.bodyFrame.Y) && flag)
            {
                drawHeadY -= 2;
            }
            else if (JunkoAndFriends.TerrarianUpFrames.Contains(drawPlayer.bodyFrame.Y))
            {
                drawHeadY += 2;
            }

            if ((int)spriteEffects == 2 || (int)spriteEffects == 3)
            {
                drawHeadY += 20;
                if (drawPlayer.Friends().pekoraSmoll)
                    drawHeadY -= 18 * 2;
            }
            Vector2 origin = drawInfo.headOrigin;

            Vector2 headPosition = new Vector2(drawHeadX, drawHeadY) + drawPlayer.headPosition - Main.screenPosition;

            float headRotation = drawPlayer.headRotation;

            Rectangle headFrame = new Rectangle(0, 0, headTexture.Width, headTexture.Height);

            DrawData headDrawData = new DrawData(headTexture, headPosition, headFrame, drawPlayer.Friends().saveUpperArmorColor, headRotation, new Vector2(headTexture.Width / 2f, headTexture.Height / 2f), 1f, spriteEffects, 0)
            {
                shader = drawInfo.headArmorShader
            };

            Main.playerDrawData.Add(headDrawData);
        });

        public static readonly PlayerHeadLayer UsadaPekoraHeadMap = new PlayerHeadLayer("JunkoAndFriends", "UsadaPekoraHeadMap", delegate (PlayerHeadDrawInfo drawInfo)
        {
            Player drawPlayer = drawInfo.drawPlayer;
            Mod mod = ModLoader.GetMod("JunkoAndFriends");

            if (drawPlayer.head != mod.GetEquipSlot("PekoraHead", EquipType.Head))
                return;

            Texture2D texture = mod.GetTexture("ExtraTextures/UsadaPekoraHead");
            float drawX = (int)(drawPlayer.position.X - Main.screenPosition.X - drawPlayer.bodyFrame.Width / 2 +
                                 drawPlayer.width / 2);
            float drawY = (int)(drawPlayer.position.Y - Main.screenPosition.Y + drawPlayer.height -
                drawPlayer.bodyFrame.Height + 4);

            Vector2 position = new Vector2(drawX, drawY) + drawPlayer.headPosition + drawInfo.drawOrigin;

            Rectangle frame = texture.Frame();

            float rotation = drawPlayer.headRotation;

            Vector2 origin = drawInfo.drawOrigin;

            float scale = drawInfo.scale;

            SpriteEffects spriteEffects = drawInfo.spriteEffects;

            DrawData drawData = new DrawData(texture, position, frame, drawInfo.armorColor, rotation, origin, scale,
                spriteEffects, 0);

            GameShaders.Armor.Apply(drawInfo.armorShader, drawPlayer, drawData);

            drawData.Draw(Main.spriteBatch);

            Main.pixelShader.CurrentTechnique.Passes[0].Apply();
        });
    }
}
