﻿/*
 * This file is part of RNG Reporter
 * Copyright (C) 2012 by Bill Young, Mike Suleski, and Andrew Ringer
 *
 * This program is free software; you can redistribute it and/or
 * modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation; either version 2
 * of the License, or (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.
 */


using RNGReporter.Properties;

namespace RNGReporter.Objects
{
    public enum EncounterType
    {
        Wild,
        WildRadar,
        WildDarkGrass,
        WildSurfing,
        WildOldRod,
        WildGoodRod,
        WildSuperRod,
        WildWaterSpot,
        WildFishingSpot,
        WildSwarm,
        WildShakerGrass,
        WildCaveSpot,
        WildShadow,
        Roamer,
        Stationary,
        Gift,
        JellicentHA,
        LarvestaHappiny,
        Haxorus,
        GibleDratini,
        Eevee,
        Deerling,
        BugCatchingContest,
        SafariZone,
        Headbutt,
        Manaphy,
        Entralink,
        HiddenGrotto
    };

    public enum EncounterMod
    {
        None,
        Search,
        Synchronize,
        Compoundeyes,
        SuctionCups,
        CuteCharm,
        Everstone,
        CuteCharm50M,
        CuteCharm75M,
        CuteCharm25M,
        CuteCharm875M,
        CuteCharm50F,
        CuteCharm75F,
        CuteCharm25F,
        CuteCharm125F,
        CuteCharmFemale,
        CuteCharmAny,
    };

    internal class EncounterTypeCalc
    {
        public static string[] encounterStringENG =
            {
                "None",
                "Synchronize",
                "Cute Charm",
                "Suction Cups",
                "Compoundeyes",
                "Everstone",
                "Unknown"
            };

        public static string[] encounterStringJPN =
            {
                "何も",
                "シンクロ",
                "メロメロボディ",
                "きゅうばん",
                "ふくがん",
                "かわらずのいし",
                "未知"
            };

        public static string[] encounterStringGER =
            {
                "Keiner",
                "Synchro",
                "Charmebolzen",
                "Saugnapf",
                "Facettenauge",
                "Ewigstein",
                "Unbekannt"
            };

        public static string[] encounterStringSPA =
            {
                "Ninguno",
                "Sincronía",
                "Gran Encanto",
                "Ventosas",
                "Ojocompuesto",
                "Piedraeterna",
                "Desconocido"
            };

        public static string[] encounterStringFRA =
            {
                "Aucun",
                "Synchro",
                "Joli Sourire",
                "Ventouse",
                "Œil Composé",
                "Pierre Stase",
                "Inconnu"
            };

        public static string[] encounterStringITA =
            {
                "Nessuno",
                "Sincronismo",
                "Incantevole",
                "Ventose",
                "Insettocchi",
                "Pietrastante",
                "Sconosciuto"
            };

        public static string[] encounterStringKOR =
            {
                "없음",
                "싱크로",
                "헤롱헤롱 바디",
                "흡반",
                "복안",
                "변함없는돌",
                "알 수없는"
            };

        public static EncounterType EncounterString(string encounterType)
        {
            switch (encounterType)
            {
                case "Wild Pokémon":
                    return EncounterType.Wild;
                case "Wild Pokemon (Poké Radar)":
                    return EncounterType.WildRadar;
                case "Wild Pokémon (Dark Grass)":
                    return EncounterType.WildDarkGrass;
                case "Wild Pokémon (Surfing)":
                    return EncounterType.WildSurfing;
                case "Wild Pokémon (Fishing)":
                    return EncounterType.WildSuperRod;
                case "Wild Pokémon (Old Rod)":
                    return EncounterType.WildOldRod;
                case "Wild Pokémon (Good Rod)":
                    return EncounterType.WildGoodRod;
                case "Wild Pokémon (Super Rod)":
                    return EncounterType.WildSuperRod;
                case "Wild Pokémon (Swarm)":
                    return EncounterType.WildSwarm;
                case "Wild Pokémon (Shaking Grass)":
                    return EncounterType.WildShakerGrass;
                case "Wild Pokémon (Bubble Spot)":
                    return EncounterType.WildWaterSpot;
                case "Wild Pokémon (Fishing Spot)":
                    return EncounterType.WildFishingSpot;
                case "Wild Pokémon (Cave Spot)":
                    return EncounterType.WildCaveSpot;
                case "Wild Pokémon (Flying Shadow)":
                    return EncounterType.WildShadow;
                case "Roaming Pokémon":
                    return EncounterType.Roamer;
                case "Stationary Pokémon":
                    return EncounterType.Stationary;
                case "Gift Pokémon":
                    return EncounterType.Gift;
                case "Jellicent":
                    return EncounterType.JellicentHA;
                case "Larvesta/Happiny Egg":
                    return EncounterType.LarvestaHappiny;
                case "Haxorus (Forced Shiny)":
                    return EncounterType.Haxorus;
                case "Gible/Dratini (Forced Shiny)":
                    return EncounterType.GibleDratini;
                case "Eevee (Shiny Locked)":
                    return EncounterType.Eevee;
                case "Deerling (Shiny Locked)":
                    return EncounterType.Deerling;
                case "Entralink Pokémon":
                    return EncounterType.Entralink;
                case "Hidden Grotto (Shiny Locked)":
                    return EncounterType.HiddenGrotto;
                case "Bug-Catching Contest":
                    return EncounterType.BugCatchingContest;
                case "Safari Zone":
                    return EncounterType.SafariZone;
                case "Manaphy Egg":
                    return EncounterType.Manaphy;
                case "Headbutt":
                    return EncounterType.Headbutt;
                default:
                    return EncounterType.Wild;
            }
        }

        public static string StringMod(EncounterMod encounterMod)
        {
            switch (encounterMod)
            {
                case EncounterMod.Synchronize:
                    return EncounterString(1);
                case EncounterMod.CuteCharm:
                    return EncounterString(2);
                case EncounterMod.SuctionCups:
                    return EncounterString(3);
                case EncounterMod.Compoundeyes:
                    return EncounterString(4);
                case EncounterMod.CuteCharm50M:
                    return EncounterString(2) + " (50% M)";
                case EncounterMod.CuteCharm75M:
                    return EncounterString(2) + " (75% M)";
                case EncounterMod.CuteCharm25M:
                    return EncounterString(2) + " (25% M)";
                case EncounterMod.CuteCharm875M:
                    return EncounterString(2) + " (87.5% M)";
                case EncounterMod.CuteCharm50F:
                    return EncounterString(2) + " (50% F)";
                case EncounterMod.CuteCharm75F:
                    return EncounterString(2) + " (75% F)";
                case EncounterMod.CuteCharm25F:
                    return EncounterString(2) + " (25% F)";
                case EncounterMod.CuteCharm125F:
                    return EncounterString(2) + " (12.5% F)";
                case EncounterMod.CuteCharmFemale:
                    return EncounterString(2) + " (Female)";
                case EncounterMod.CuteCharmAny:
                    return EncounterString(2) + " (Any)";
                case EncounterMod.Everstone:
                    return EncounterString(5);
                case EncounterMod.None:
                    return EncounterString(0);
                default:
                    return "Unknown";
            }
        }

        public static string EncounterString(int index)
        {
            switch ((Language) Settings.Default.Language)
            {
                case (Language.Japanese):
                    return encounterStringJPN[index];
                case (Language.German):
                    return encounterStringGER[index];
                case (Language.French):
                    return encounterStringFRA[index];
                case (Language.Spanish):
                    return encounterStringSPA[index];
                case (Language.Italian):
                    return encounterStringITA[index];
                case (Language.Korean):
                    return encounterStringKOR[index];
                default:
                    return encounterStringENG[index];
            }
        }
    }
}