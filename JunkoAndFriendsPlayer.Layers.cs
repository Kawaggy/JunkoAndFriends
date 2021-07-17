using JunkoAndFriends.Items.BerserkerVanity;
using JunkoAndFriends.Items.GuraGawrVanity;
using JunkoAndFriends.Items.JunkoVanity;
using JunkoAndFriends.Items.RemiliaVanity;
using JunkoAndFriends.Items.UsadaPekoraVanity;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace JunkoAndFriends
{
    public partial class JunkoAndFriendsPlayer : ModPlayer
    {
        public override void ModifyDrawLayers(List<PlayerLayer> layers)
        {
            int headLayer = layers.FindIndex(l => l == PlayerLayer.Head);

            if (headLayer > -1)
            {
                layers.Insert(headLayer + 1, GuraGawrVanityExtra.GuraGawrA);
                layers.Insert(headLayer + 1, BerserkerVanityExtra.BerserkerHelmetBerserk);
                layers.Insert(headLayer + 1, UsadaPekoraVanityExtra.UsadaPekoraHead);
            }

            JunkoVanityExtra.JunkoAura.visible = true;
            layers.Insert(0, JunkoVanityExtra.JunkoAura);
            RemiliaVanityExtra.RemiliaWings.visible = true;
            layers.Insert(1, RemiliaVanityExtra.RemiliaWings);
            GuraGawrVanityExtra.GuraGawrTail.visible = true;
            layers.Insert(0, GuraGawrVanityExtra.GuraGawrTail);
            GuraGawrVanityExtra.GuraGawrTrident.visible = true;
            layers.Insert(0, GuraGawrVanityExtra.GuraGawrTrident);
            BerserkerVanityExtra.BerserkerCape.visible = true;
            layers.Insert(0, BerserkerVanityExtra.BerserkerCape);
        }

        public override void ModifyDrawHeadLayers(List<PlayerHeadLayer> layers)
        {
            int headLayer = layers.FindIndex(l => l == PlayerHeadLayer.Armor);

            if (headLayer > -1)
            {
                layers.Insert(headLayer + 1, UsadaPekoraVanityExtra.UsadaPekoraHeadMap);
                layers.Insert(headLayer + 1, BerserkerVanityExtra.BerserkerHeadMap);
            }
        }

        public override void ModifyDrawInfo(ref PlayerDrawInfo drawInfo)
        {
            if (drawInfo.shadow != 0)
                return;

            Player drawPlayer = drawInfo.drawPlayer;

            if (drawPlayer.legs == mod.GetEquipSlot("FlandreLeg", EquipType.Legs) ||
                drawPlayer.legs == mod.GetEquipSlot("RemiliaLeg", EquipType.Legs) ||
                drawPlayer.legs == mod.GetEquipSlot("GuraGawrLeg", EquipType.Legs) ||
                drawPlayer.legs == mod.GetEquipSlot("BerserkerLeg", EquipType.Legs) ||
                drawPlayer.legs == mod.GetEquipSlot("PekoraLeg", EquipType.Legs))
            {
                TurnLegTransparent(ref drawInfo);
            }

            if (drawPlayer.body == mod.GetEquipSlot("GuraGawrBody", EquipType.Body) ||
                drawPlayer.body == mod.GetEquipSlot("PekoraBody", EquipType.Body))
            {
                TurnBodyTransparent(ref drawInfo);
            }

            if (drawPlayer.head == mod.GetEquipSlot("GuraGawrHeadHair", EquipType.Head) ||
                drawPlayer.head == mod.GetEquipSlot("GuraGawrHeadHoodie", EquipType.Head))
            {
                TurnHeadTransparent(ref drawInfo);
            }

            if (drawPlayer.head == mod.GetEquipSlot("PekoraHead", EquipType.Head))
            {
                TurnHeadTransparent(ref drawInfo);
                saveUpperArmorColor = drawInfo.upperArmorColor;
                drawInfo.upperArmorColor = Color.Transparent;
            }
        }
    }
}
