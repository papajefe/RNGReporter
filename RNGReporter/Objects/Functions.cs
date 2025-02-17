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


using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using RNGReporter.Properties;

namespace RNGReporter.Objects
{
    internal static class Functions
    {
        public static readonly int[] GenderThresholds = new[] {190, 126, 63, 30};
        public static readonly uint[] UnbiasedBuffer = new uint[] {0, 0x96, 0xC8, 0x4B, 0x32};

        public static readonly string[] buttonStrings = new[]
            {
                "None",
                "Start",
                "Select",
                "A",
                "B",
                "Right",
                "Left",
                "Up",
                "Down",
                "R",
                "L",
                "X",
                "Y"
            };

        public static readonly int[,] probabilityTable = new[,]
            {
                {50, 100, 100, 100, 100},
                {50, 50, 100, 100, 100},
                {30, 50, 100, 100, 100},
                {25, 30, 50, 100, 100},
                {20, 25, 33, 50, 100},
                {100, 100, 100, 100, 100}
            };

        public static string power(uint type)
        {
            switch ((Language) Settings.Default.Language)
            {
                case (Language.Japanese):
                    return Translations.powerJPN[type];
                case (Language.German):
                    return Translations.powerGER[type];
                case (Language.Spanish):
                    return Translations.powerSPA[type];
                case (Language.Italian):
                    return Translations.powerITA[type];
                case (Language.French):
                    return Translations.powerFRA[type];
                case (Language.Korean):
                    return Translations.powerKOR[type];
                default:
                    return Translations.powerENG[type];
            }
        }

        public static string NatureStrings(int nature)
        {
            if (nature == -2)
            {
                return "Any";
            }

            if (nature == -1)
            {
                return "None";
            }

            switch ((Language) Settings.Default.Language)
            {
                case (Language.Japanese):
                    return Translations.NatureStringJPN[nature];
                case (Language.German):
                    return Translations.NatureStringGER[nature];
                case (Language.French):
                    return Translations.NatureStringFRA[nature];
                case (Language.Spanish):
                    return Translations.NatureStringSPA[nature];
                case (Language.Italian):
                    return Translations.NatureStringITA[nature];
                case (Language.Korean):
                    return Translations.NatureStringKOR[nature];
                default:
                    return Translations.NatureStringENG[nature];
            }
        }

        public static int NatureNumber(String nature)
        {
            switch(nature)
            {
                case "Hardy":
                    return 0;
                case "Lonely":
                    return 1;
                case "Brave":
                    return 2;
                case "Adamant":
                    return 3;
                case "Naughty":
                    return 4;
                case "Bold":
                    return 5;
                case "Docile":
                    return 6;
                case "Relaxed":
                    return 7;
                case "Impish":
                    return 8;
                case "Lax":
                    return 9;
                case "Timid":
                    return 10;
                case "Hasty":
                    return 11;
                case "Serious":
                    return 12;
                case "Jolly":
                    return 13;
                case "Naive":
                    return 14;
                case "Modest":
                    return 15;
                case "Mild":
                    return 16;
                case "Quiet":
                    return 17;
                case "Bashful":
                    return 18;
                case "Rash":
                    return 19;
                case "Calm":
                    return 20;
                case "Gentle":
                    return 21;
                case "Sassy":
                    return 22;
                case "Careful":
                    return 23;
                default:
                    return 24;
            }
        }

        public static string encounterItems(int slot)
        {
            if (slot < 12)// || slot > 41)
                return slot.ToString();

            if (slot > 500)
            {
                switch (slot)
                {
                    case 0x235:
                        return "Health Wing";
                    case 0x236:
                        return "Muscle Wing";
                    case 0x237:
                        return "Resist Wing";
                    case 0x238:
                        return "Genius Wing";
                    case 0x239:
                        return "Clever Wing";
                    case 0x23A:
                        return "Swift Wing";
                    default:    // 0x23B
                        return "Pretty Wing";
                }
            }

            switch ((Language) Settings.Default.Language)
            {
                case (Language.Japanese):
                    return Translations.encounterItemsJPN[slot - 12];
                case (Language.German):
                    return Translations.encounterItemsGER[slot - 12];
                case (Language.French):
                    return Translations.encounterItemsFRA[slot - 12];
                case (Language.Spanish):
                    return Translations.encounterItemsSPA[slot - 12];
                case (Language.Italian):
                    return Translations.encounterItemsITA[slot - 12];
                case (Language.Korean):
                    return Translations.encounterItemsKOR[slot - 12];
                default:
                    return Translations.encounterItemsENG[slot - 12];
            }
        }

        public static string characteristicStrings(uint index)
        {
            switch ((Language) Settings.Default.Language)
            {
                case (Language.Japanese):
                    return Translations.characteristicStringsJPN[index];
                case (Language.German):
                    return Translations.characteristicStringsGER[index];
                case (Language.Spanish):
                    return Translations.characteristicStringsSPA[index];
                case (Language.French):
                    return Translations.characteristicStringsFRA[index];
                case (Language.Italian):
                    return Translations.characteristicStringsITA[index];
                case (Language.Korean):
                    return Translations.characteristicStringsKOR[index];
                default:
                    return Translations.characteristicStringsENG[index];
            }
        }

        public static string characteristicCalc(uint PID, uint[] IVarray)
        {
            uint characteristic = (PID%6);

            uint[] IVs = IVarray;

            uint max = 0;
            uint maxIndex = 6;
            uint realIndex = 0;
            uint innerCount = 0;

            for (uint i = characteristic; i < characteristic + 6; i++)
            {
                uint index = i%6;

                if (IVs[index] >= max)
                {
                    if ((IVs[index] == max && innerCount < maxIndex) || IVs[index] > max)
                    {
                        max = IVs[index];
                        maxIndex = innerCount;
                        realIndex = index;
                    }
                }
                innerCount++;
            }

            return characteristicStrings((realIndex*5) + (max%5));
        }

        // take an iv array and move the order from:
        // hp/atk/def/spa/spd/spe to:
        // hp/atk/def/spe/spa/spd
        public static uint[] moveSpeFromBack(uint[] IVarray)
        {
            // we make a copy to prevent changing the original array
            var ivs = (uint[]) IVarray.Clone();
            ivs[5] = IVarray[4];
            ivs[4] = IVarray[3];
            ivs[3] = IVarray[5];
            return ivs;
        }

        // note: to be removed later once profiles are used everywhere
        public static uint initialPIDRNG(ulong seed, Version version, bool memorylink)
        {
            switch (version)
            {
                case Version.Black2:
                case Version.White2:
                    var profile = new Profile {Version = version, MemoryLink = memorylink};
                    return initialPIDRNGBW2(seed, profile);
                default:
                    return initialPIDRNGBW(seed);
            }
        }

        public static uint initialPIDRNG(ulong seed, Profile profile)
        {
            switch (profile.Version)
            {
                case Version.Black2:
                case Version.White2:
                    return initialPIDRNGBW2(seed, profile);
                default:
                    return initialPIDRNGBW(seed);
            }
        }

        // note: we may want to support memorylink id abuse later needs research if it's reset
        public static uint initialPIDRNG_ID(ulong seed, Version version)
        {
            switch (version)
            {
                case Version.Black2:
                case Version.White2:
                    return initialPIDRNGBW2_ID(seed);
                default:
                    return initialPIDRNGBW_ID(seed);
            }
        }

        public static uint initialPIDRNGBW(ulong seed)
        {
            var rng = new BWRng(seed);
            uint frameCount = 1;

            for (int i = 0; i < 5; i++)
            {
                for (int k = 0; k < 6; k++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        if (probabilityTable[k, j] == 100)
                            break;

                        frameCount++;
                        uint rn = rng.GetNext32BitNumber(101);

                        if (rn <= probabilityTable[k, j])
                            break;
                    }
                }
            }

            return frameCount;
        }

        public static uint initialPIDRNGBW_ID(ulong seed)
        {
            var rng = new BWRng(seed);
            uint frameCount = 2;

            int rounds = 3;

            for (int i = 0; i < rounds; i++)
            {
                for (int k = 0; k < 6; k++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        if (probabilityTable[k, j] == 100)
                            break;

                        frameCount++;
                        uint rn = rng.GetNext32BitNumber(101);

                        if (rn <= probabilityTable[k, j])
                            break;
                    }
                }
            }

            return frameCount;
        }

        public static uint initialPIDRNGBW2(ulong seed, Profile profile)
        {
            var rng = new BWRng(seed);
            uint frameCount = 1;

            for (int i = 0; i < 5; i++)
            {
                for (int k = 0; k < 6; k++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        if (probabilityTable[k, j] == 100)
                            break;

                        frameCount++;
                        uint rn = rng.GetNext32BitNumber(101);

                        if (rn <= probabilityTable[k, j])
                            break;
                    }
                }
                if (i == 0)
                {
                    //BW2 has 3 advances after the first table call and before the rest
                    //it is only 2 advances after memorylink
                    int advances = profile.MemoryLink ? 2 : 3;
                    for (int j = 0; j < advances; ++j)
                    {
                        frameCount++;
                        rng.GetNext64BitNumber();
                    }
                }
            }

            frameCount = initialPIDRNGBW2_extra(rng, frameCount);
            return frameCount;
        }

        private static uint initialPIDRNGBW2_extra(BWRng rng, uint frameCount)
        {
            bool loop = true;
            for (int limit = 0; loop && limit < 100; ++limit)
            {
                loop = false;
                var tmp = new uint[3];
                frameCount += 3;
                for (int i = 0; i < 3; ++i)
                {
                    tmp[i] = rng.GetNext32BitNumber(15);
                }

                for (int i = 0; i < 3; ++i)
                    for (int j = 0; j < 3; ++j)
                    {
                        if (i == j) continue;
                        if (tmp[i] == tmp[j])
                        {
                            loop = true;
                        }
                    }
            }
            return frameCount;
        }

        public static uint initialPIDRNGBW2_ID(ulong seed)
        {
            // we are ignoring the saveFile command because it hasn't been properly researched without a save file
            var rng = new BWRng(seed);
            uint frameCount = 1;

            //int rounds = saveFile ? 4 : 3;
            const int rounds = 3;

            for (int i = 0; i < rounds; i++)
            {
                for (int k = 0; k < 6; k++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        if (probabilityTable[k, j] == 100)
                            break;

                        frameCount++;
                        uint rn = rng.GetNext32BitNumber(101);

                        if (rn <= probabilityTable[k, j])
                            break;
                    }
                }
                if (i == 0)
                {
                    //two advances after the first table call
                    for (int j = 0; j < 2; ++j)
                    {
                        frameCount++;
                        rng.GetNext64BitNumber();
                    }
                }
                if (i == 1)
                {
                    //BW2 has 4 advances after the second table call and before the rest
                    for (int j = 0; j < 4; ++j)
                    {
                        frameCount++;
                        rng.GetNext64BitNumber();
                    }
                }
            }
            // three more advances at the end
            return frameCount + 3;
        }

        public static string NullIV(uint IV)
        {
            if (IV <= 31)
                return IV.ToString();
            return "??";
        }

        public static string NullIV(uint IV, string nullString)
        {
            if (IV <= 31)
                return IV.ToString();
            return nullString;
        }

        //----------------------------------------------------------------------------------------------------------------------

        public static bool Shiny(uint PID, uint TID, uint SID)
        {
            return ((((PID & 0xFFFF) ^ (PID >> 16) ^ TID ^ SID) & 0xFFF8) == 0);
        }

        //----------------------------------------------------------------------------------------------------------------------

        public static int Gender(uint PID, int genderRatio)
        {
            //if selected gender ratio is fixed/genderless
            if (genderRatio > 3) return 2;

            //0 is male, 1 is female to match the combobox indices
            return (PID & 0xFF) <= GenderThresholds[genderRatio] ? 1 : 0;
        }

        //----------------------------------------------------------------------------------------------------------------------

        public static int Ability(uint PID)
        {
            //0 for first ability, 1 for second
            return (int) (PID & 0x1);
        }

        public static int Ability5thGen(uint PID)
        {
            //0 for first ability, 1 for second
            return (int) ((PID >> 16) & 0x1);
        }

        //----------------------------------------------------------------------------------------------------------------------

        public static uint Nature(uint PID)
        {
            return (PID%25);
        }

        //----------------------------------------------------------------------------------------------------------------------
        public static uint ChainedPIDLower(uint oldPIDLower, uint Call1, uint Call2, uint Call3, uint Call4, uint Call5,
                                           uint Call6, uint Call7, uint Call8, uint Call9, uint Call10, uint Call11,
                                           uint Call12, uint Call13)
        {
            return (oldPIDLower & 0x7) | (Call13 & 1) << 3 | (Call12 & 1) << 4 | (Call11 & 1) << 5 | (Call10 & 1) << 6 |
                   (Call9 & 1) << 7 | (Call8 & 1) << 8 | (Call7 & 1) << 9 | (Call6 & 1) << 10 | (Call5 & 1) << 11 |
                   (Call4 & 1) << 12 | (Call3 & 1) << 13 | (Call2 & 1) << 14 | (Call1 & 1) << 15;
        }

        public static uint ChainedPIDUpper(uint oldPIDUpper, uint LowerPID, uint TID, uint SID)
        {
            return ((LowerPID ^ TID ^ SID) & 0xFFF8 | oldPIDUpper & 0x7);
        }

        // unfinished
        public static string MinimumChain(uint frame1, uint frame2, uint frame3)
        {
            string result = "";
            if (frame1 < 328)
            {
                result = "Frame 1";
                result = result + " (40)";
            }

            if (frame2 < 328)
            {
                result = "Frame 2";
                result = result + " (40)";
            }

            if (frame3 < 328)
            {
                result = "Frame 3";
                result = result + " (40)";
            }

            return result;
        }

        //----------------------------------------------------------------------------------------------------------------------        

        public static uint CuteCharmModPID(uint pid, uint rngResult, int genderRatio)
        {
            uint genderAdjustment;

            switch (genderRatio)
            {
                // Female, 50% F ratio      ✔
                case -1:
                    genderAdjustment = (uint) ((0x7E * (ulong) rngResult) >> 32) + 1;
                    break;
                // Female, 75% F ratio      ✔
                case -2:
                    genderAdjustment = (uint) ((0xBE * (ulong) rngResult) >> 32) + 1;
                    break;
                // Female, 25% F ratio      ✔
                case -3:
                    genderAdjustment = (uint) ((0x3E * (ulong) rngResult) >> 32) + 1;
                    break;
                // Female, 12.5% F ratio    ✔
                case -4:
                    genderAdjustment = (uint) ((0x1E * (ulong) rngResult) >> 32) + 1;
                    break;
                // Female, 100% F ratio     ✔
                case -5:
                    genderAdjustment = (uint) ((8 * (ulong) rngResult) >> 32) + 1;
                    break;
                // Male, 50% M ratio        ✔
                case 1:
                    genderAdjustment = (uint) ((0x7F * (ulong) rngResult) >> 32) + 0x7F;
                    break;
                // Male, 75% M ratio        ✔
                case 2:
                    genderAdjustment = (uint) ((0xBF * (ulong) rngResult) >> 32) + 0x3F;
                    break;
                // Male, 25% M ratio        ✔
                case 3:
                    genderAdjustment = (uint) ((0x3F * (ulong) rngResult) >> 32) + 0xBF;
                    break;
                // Male, 87.5% M ratio      ✔
                case 4:
                    genderAdjustment = (uint) ((0xDF * (ulong) rngResult) >> 32) + 0x1F;
                    break;
                // Male, 100% M ratio       ✔
                case 5:
                    genderAdjustment = (uint) ((0xF6 * (ulong) rngResult) >> 32) + 8;
                    break;
                default:
                    genderAdjustment = 0;
                    break;
            }

            return (pid & 0xFFFFFF00) | genderAdjustment;
        }


        public static List<List<ButtonComboType>> KeypressCombos(int maxButtons, bool skipLR)
        {
            int keypress1 = 0;
            int keypress2 = 0;
            int keypress3 = 0;
            int keypress4 = 0;
            int keypress5 = 0;
            int keypress6 = 0;
            int keypress7 = 0;

            if (maxButtons > 0)
                keypress1 = 13;
            if (maxButtons > 1)
                keypress2 = 13;
            if (maxButtons > 2)
                keypress3 = 13;
            if (maxButtons > 3)
                keypress4 = 13;
            if (maxButtons > 4)
                keypress5 = 13;
            if (maxButtons > 5)
                keypress6 = 13;
            if (maxButtons > 6)
                keypress7 = 13;

            var buttons = new int[7];
            var keyPresses = new List<List<ButtonComboType>>();
            for (buttons[0] = 1; buttons[0] < keypress1; buttons[0]++)
            {
                if (skipLR && (buttons[0] == 9 || buttons[0] == 10))
                    continue;

                if (maxButtons == 1)
                {
                    var keyCombo = new List<ButtonComboType> {(ButtonComboType) buttons[0]};
                    keyPresses.Add(keyCombo);
                }

                for (buttons[1] = buttons[0] + 1; buttons[1] < keypress2; buttons[1]++)
                {
                    if (skipLR && (buttons[1] == 9 || buttons[1] == 10))
                        continue;

                    // Can't press Left and Right at the same time
                    if (buttons[1] == 6 && buttons[0] == 5)
                        continue;

                    // Can't press Up and Down at the same time
                    if (buttons[1] == 8 && buttons[0] == 7)
                        continue;

                    if (maxButtons == 2)
                    {
                        var keyCombo = new List<ButtonComboType>
                            {
                                (ButtonComboType) buttons[0],
                                (ButtonComboType) buttons[1]
                            };
                        keyPresses.Add(keyCombo);
                    }

                    for (buttons[2] = buttons[1] + 1; buttons[2] < keypress3; buttons[2]++)
                    {
                        if (skipLR && (buttons[2] == 9 || buttons[2] == 10))
                            continue;

                        // Can't press Left and Right at the same time
                        if (buttons[2] == 6 && buttons[1] == 5)
                            continue;

                        // Can't press Up and Down at the same time
                        if (buttons[2] == 8 && buttons[1] == 7)
                            continue;

                        // Can't press Start+Select+A or B
                        if (buttons[0] == 1 && buttons[1] == 2 &&
                            (buttons[2] == 3 || buttons[2] == 4))
                            continue;

                        if (maxButtons == 3)
                        {
                            var keyCombo = new List<ButtonComboType>
                                {
                                    (ButtonComboType) buttons[0],
                                    (ButtonComboType) buttons[1],
                                    (ButtonComboType) buttons[2]
                                };
                            keyPresses.Add(keyCombo);
                        }

                        for (buttons[3] = buttons[2] + 1; buttons[3] < keypress4; buttons[3]++)
                        {
                            if (skipLR && (buttons[3] == 9 || buttons[3] == 10))
                                continue;

                            // Can't press Left and Right at the same time
                            if (buttons[3] == 6 && buttons[2] == 5)
                                continue;

                            // Can't press Up and Down at the same time
                            if (buttons[3] == 8 && buttons[2] == 7)
                                continue;

                            // Can't press Start+Select+R+L
                            if (buttons[0] == 1 && buttons[1] == 2 &&
                                (buttons[2] == 9 && buttons[3] == 10))
                                continue;

                            if (maxButtons == 4)
                            {
                                var keyCombo = new List<ButtonComboType>
                                    {
                                        (ButtonComboType) buttons[0],
                                        (ButtonComboType) buttons[1],
                                        (ButtonComboType) buttons[2],
                                        (ButtonComboType) buttons[3]
                                    };
                                keyPresses.Add(keyCombo);
                            }

                            for (buttons[4] = buttons[3] + 1; buttons[4] < keypress5; buttons[4]++)
                            {
                                if (skipLR && (buttons[4] == 9 || buttons[4] == 10))
                                    continue;

                                // Can't press Left and Right at the same time
                                if (buttons[4] == 6 && buttons[3] == 5)
                                    continue;

                                // Can't press Up and Down at the same time
                                if (buttons[4] == 8 && buttons[3] == 7)
                                    continue;

                                // Can't press Start+Select+R+L
                                if (buttons[0] == 1 && buttons[1] == 2 &&
                                    (buttons[3] == 9 && buttons[4] == 10))
                                    continue;

                                if (maxButtons == 5)
                                {
                                    var keyCombo = new List<ButtonComboType>
                                        {
                                            (ButtonComboType) buttons[0],
                                            (ButtonComboType) buttons[1],
                                            (ButtonComboType) buttons[2],
                                            (ButtonComboType) buttons[3],
                                            (ButtonComboType) buttons[4]
                                        };
                                    keyPresses.Add(keyCombo);
                                }

                                for (buttons[5] = buttons[4] + 1; buttons[5] < keypress6; buttons[5]++)
                                {
                                    if (skipLR && (buttons[5] == 9 || buttons[5] == 10))
                                        continue;

                                    // Can't press Left and Right at the same time
                                    if (buttons[5] == 6 && buttons[4] == 5)
                                        continue;

                                    // Can't press Up and Down at the same time
                                    if (buttons[5] == 8 && buttons[4] == 7)
                                        continue;

                                    // Can't press Start+Select+R+L
                                    if (buttons[0] == 1 && buttons[1] == 2 &&
                                        (buttons[4] == 9 && buttons[5] == 10))
                                        continue;

                                    if (maxButtons == 6)
                                    {
                                        var keyCombo = new List<ButtonComboType>
                                            {
                                                (ButtonComboType) buttons[0],
                                                (ButtonComboType) buttons[1],
                                                (ButtonComboType) buttons[2],
                                                (ButtonComboType) buttons[3],
                                                (ButtonComboType) buttons[4],
                                                (ButtonComboType) buttons[5]
                                            };
                                        keyPresses.Add(keyCombo);
                                    }

                                    for (buttons[6] = buttons[5] + 1; buttons[6] < keypress7; buttons[6]++)
                                    {
                                        if (skipLR && (buttons[6] == 9 || buttons[6] == 10))
                                            continue;

                                        // Can't press Left and Right at the same time
                                        if (buttons[6] == 6 && buttons[5] == 5)
                                            continue;

                                        // Can't press Up and Down at the same time
                                        if (buttons[6] == 8 && buttons[5] == 7)
                                            continue;

                                        // Can't press Start+Select+R+L
                                        if (buttons[0] == 1 && buttons[1] == 2 &&
                                            (buttons[5] == 9 && buttons[6] == 10))
                                            continue;

                                        if (maxButtons == 7)
                                        {
                                            var keyCombo = new List<ButtonComboType>
                                                {
                                                    (ButtonComboType) buttons[0],
                                                    (ButtonComboType) buttons[1],
                                                    (ButtonComboType) buttons[2],
                                                    (ButtonComboType) buttons[3],
                                                    (ButtonComboType) buttons[4],
                                                    (ButtonComboType) buttons[5],
                                                    (ButtonComboType) buttons[6]
                                                };
                                            keyPresses.Add(keyCombo);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            if (maxButtons == 0)
                keyPresses.Add(new List<ButtonComboType> {ButtonComboType.None});

            return keyPresses;
        }

        public static uint buttonMashed(List<ButtonComboType> buttons)
        {
            uint buttonCode = 0xFF2F0000;
            foreach (ButtonComboType button in buttons)
            {
                switch (button)
                {
                    case ButtonComboType.None:
                        break;
                    case ButtonComboType.A:
                        buttonCode = buttonCode - 0x1000000;
                        break;
                    case ButtonComboType.B:
                        buttonCode = buttonCode - 0x2000000;
                        break;
                    case ButtonComboType.Select:
                        buttonCode = buttonCode - 0x4000000;
                        break;
                    case ButtonComboType.Start:
                        buttonCode = buttonCode - 0x8000000;
                        break;
                    case ButtonComboType.Right:
                        buttonCode = buttonCode - 0x10000000;
                        break;
                    case ButtonComboType.Left:
                        buttonCode = buttonCode - 0x20000000;
                        break;
                    case ButtonComboType.Up:
                        buttonCode = buttonCode - 0x40000000;
                        break;
                    case ButtonComboType.Down:
                        buttonCode = buttonCode - 0x80000000;
                        break;
                    case ButtonComboType.R:
                        buttonCode = buttonCode - 0x10000;
                        break;
                    case ButtonComboType.L:
                        buttonCode = buttonCode - 0x20000;
                        break;
                    case ButtonComboType.X:
                        buttonCode = buttonCode - 0x40000;
                        break;
                    case ButtonComboType.Y:
                        buttonCode = buttonCode - 0x80000;
                        break;
                }
            }
            return buttonCode;
        }


        public static uint buttonMashed(int[] buttons)
        {
            uint buttonCode = 0xFF2F0000;
            foreach (int button in buttons)
            {
                switch ((ButtonComboType) button)
                {
                    case ButtonComboType.None:
                        break;
                    case ButtonComboType.A:
                        buttonCode = buttonCode - 0x1000000;
                        break;
                    case ButtonComboType.B:
                        buttonCode = buttonCode - 0x2000000;
                        break;
                    case ButtonComboType.Select:
                        buttonCode = buttonCode - 0x4000000;
                        break;
                    case ButtonComboType.Start:
                        buttonCode = buttonCode - 0x8000000;
                        break;
                    case ButtonComboType.Right:
                        buttonCode = buttonCode - 0x10000000;
                        break;
                    case ButtonComboType.Left:
                        buttonCode = buttonCode - 0x20000000;
                        break;
                    case ButtonComboType.Up:
                        buttonCode = buttonCode - 0x40000000;
                        break;
                    case ButtonComboType.Down:
                        buttonCode = buttonCode - 0x80000000;
                        break;
                    case ButtonComboType.R:
                        buttonCode = buttonCode - 0x10000;
                        break;
                    case ButtonComboType.L:
                        buttonCode = buttonCode - 0x20000;
                        break;
                    case ButtonComboType.X:
                        buttonCode = buttonCode - 0x40000;
                        break;
                    case ButtonComboType.Y:
                        buttonCode = buttonCode - 0x80000;
                        break;
                }
            }
            return buttonCode;
        }

        public static uint buttonMashed(int button)
        {
            switch ((ButtonComboType) button)
            {
                case ButtonComboType.None:
                    return 0xFF2F0000;

                case ButtonComboType.A:
                    return 0xFE2F0000;

                case ButtonComboType.B:
                    return 0xFD2F0000;

                case ButtonComboType.Select:
                    return 0xFB2F0000;

                case ButtonComboType.Start:
                    return 0xF72F0000;

                case ButtonComboType.Right:
                    return 0xEF2F0000;

                case ButtonComboType.Left:
                    return 0xDF2F0000;

                case ButtonComboType.Up:
                    return 0xBF2F0000;

                case ButtonComboType.Down:
                    return 0x7F2F0000;

                case ButtonComboType.R:
                    return 0xFF2E0000;

                case ButtonComboType.L:
                    return 0xFF2D0000;

                case ButtonComboType.X:
                    return 0xFF2B0000;

                case ButtonComboType.Y:
                    return 0xFF270000;

                default:
                    return 0xFF2F0000;
            }
        }

        public static uint seedSecond(int second)
        {
            return (uint) (second/10*16 | second%10) << 8;
        }

        public static uint seedMinute(int minute)
        {
            return (uint) (minute/10*16 | minute%10) << 16;
        }

        public static uint seedHour(int hour, DSType dstype)
        {
            // All DSes return the hour in this dec->hex conversion
            // However, DSes prior to the 3DS add 0x40 past 12:00 PM
            if (dstype == DSType.DS_3DS)
            {
                switch (hour)
                {
                    case 10:
                        return 0x10000000;
                    case 11:
                        return 0x11000000;
                    case 12:
                        return 0x12000000;
                    case 13:
                        return 0x13000000;
                    case 14:
                        return 0x14000000;
                    case 15:
                        return 0x15000000;
                    case 16:
                        return 0x16000000;
                    case 17:
                        return 0x17000000;
                    case 18:
                        return 0x18000000;
                    case 19:
                        return 0x19000000;
                    case 20:
                        return 0x20000000;
                    case 21:
                        return 0x21000000;
                    case 22:
                        return 0x22000000;
                    case 23:
                        return 0x23000000;
                    default:
                        return (uint) hour << 24;
                }
            }
            switch (hour)
            {
                case 10:
                    return 0x10000000;
                case 11:
                    return 0x11000000;
                case 12:
                    return 0x52000000;
                case 13:
                    return 0x53000000;
                case 14:
                    return 0x54000000;
                case 15:
                    return 0x55000000;
                case 16:
                    return 0x56000000;
                case 17:
                    return 0x57000000;
                case 18:
                    return 0x58000000;
                case 19:
                    return 0x59000000;
                case 20:
                    return 0x60000000;
                case 21:
                    return 0x61000000;
                case 22:
                    return 0x62000000;
                case 23:
                    return 0x63000000;
                default:
                    return (uint) hour << 24;
            }
        }

        public static uint seedDay(int day)
        {
            return (uint) ((day/10*16) | day%10) << 8;
        }

        public static uint seedMonth(int month)
        {
            return (uint) ((month/10*16) | month%10) << 16;
        }

        public static uint seedYear(int year)
        {
            return (uint) ((year/10*16) | year%10) << 24;
        }

        public static uint seedTime(DateTime dateTime, DSType dstype)
        {
            return seedSecond(dateTime.Second) | seedMinute(dateTime.Minute) | seedHour(dateTime.Hour, dstype);
        }

        public static uint seedDate(DateTime dateTime)
        {
            return seedYear(dateTime.Year%100) | seedMonth(dateTime.Month) | seedDay(dateTime.Day) |
                   (uint) dateTime.DayOfWeek;
        }

        const uint K0 = 0x5A827999;
        const uint K1 = 0x6ED9EBA1;
        const uint K2 = 0x8F1BBCDC;
        const uint K3 = 0xCA62C1D6;

        const uint H0 = 0x67452301;
        const uint H1 = 0xEFCDAB89;
        const uint H2 = 0x98BADCFE;
        const uint H3 = 0x10325476;
        const uint H4 = 0xC3D2E1F0;

        private delegate void del();

        private delegate void del2(uint i);

        public static ulong EncryptSeed(DateTime dateTime, ulong MACaddress, Version version, Language language,
                                        DSType dstype,
                                        bool softReset, uint VCount, uint Timer0, uint GxStat, uint VFrame,
                                        uint buttonMashed)
        {
            var message = new uint[80];

            message[5] = Reorder((VCount << 16) | Timer0);
            message[6] = (uint) (MACaddress & 0xFFFF);
            if (softReset)
                message[6] = message[6] ^ 0x01000000;
            message[7] = (uint) ((MACaddress >> 16) ^ (VFrame << 24) ^ GxStat);
            message[8] = seedDate(dateTime);
            message[9] = seedTime(dateTime, dstype);
            message[12] = buttonMashed;
            message[13] = 0x80000000;
            message[15] = 0x000001A0;

            // Get the version-unique part of the message
            Array.Copy(Nazos.Nazo(version, language, dstype), message, 5);
            uint a = H0;
            uint b = H1;
            uint c = H2;
            uint d = H3;
            uint e = H4;
            uint temp = 0;

            del Section1Calc = () => temp = ((a << 5) | (a >> 27)) + ((b & c) | (~b & d)) + e + K0 + temp;
            del Section2Calc = () => temp = ((a << 5) | (a >> 27)) + (b ^ c ^ d) + e + K1 + temp;
            del Section3Calc = () => temp = ((a << 5) | (a >> 27)) + ((b & c) | (b & d) | (c & d)) + e + K2 + temp;
            del Section4Calc = () => temp = ((a << 5) | (a >> 27)) + (b ^ c ^ d) + e + K3 + temp;
            del UpdateVars = () =>
            {
                e = d;
                d = c;
                c = (b << 30) | (b >> 2);
                b = a;
                a = temp;
            };

            del2 CalcW = I =>
            {
                temp = message[I - 3] ^ message[I - 8] ^ message[I - 14] ^ message[I - 16];
                message[I] = temp = (temp << 1) | (temp >> 31);
            };

            // Section 1: 0-19
            temp = message[0]; Section1Calc(); UpdateVars();
            temp = message[1]; Section1Calc(); UpdateVars();
            temp = message[2]; Section1Calc(); UpdateVars();
            temp = message[3]; Section1Calc(); UpdateVars();
            temp = message[4]; Section1Calc(); UpdateVars();
            temp = message[5]; Section1Calc(); UpdateVars();
            temp = message[6]; Section1Calc(); UpdateVars();
            temp = message[7]; Section1Calc(); UpdateVars();
            temp = message[8]; Section1Calc(); UpdateVars();
            temp = message[9]; Section1Calc(); UpdateVars();
            temp = message[10]; Section1Calc(); UpdateVars();
            temp = message[11]; Section1Calc(); UpdateVars();
            temp = message[12]; Section1Calc(); UpdateVars();
            temp = message[13]; Section1Calc(); UpdateVars();
            temp = message[14]; Section1Calc(); UpdateVars();
            temp = message[15]; Section1Calc(); UpdateVars();
            CalcW(16); Section1Calc(); UpdateVars();
            CalcW(17); Section1Calc(); UpdateVars();
            CalcW(18); Section1Calc(); UpdateVars();
            CalcW(19); Section1Calc(); UpdateVars();

            // Section 2: 20 - 39
            CalcW(20); Section2Calc(); UpdateVars();
            CalcW(21); Section2Calc(); UpdateVars();
            CalcW(22); Section2Calc(); UpdateVars();
            CalcW(23); Section2Calc(); UpdateVars();
            CalcW(24); Section2Calc(); UpdateVars();
            CalcW(25); Section2Calc(); UpdateVars();
            CalcW(26); Section2Calc(); UpdateVars();
            CalcW(27); Section2Calc(); UpdateVars();
            CalcW(28); Section2Calc(); UpdateVars();
            CalcW(29); Section2Calc(); UpdateVars();
            CalcW(30); Section2Calc(); UpdateVars();
            CalcW(31); Section2Calc(); UpdateVars();
            CalcW(32); Section2Calc(); UpdateVars();
            CalcW(33); Section2Calc(); UpdateVars();
            CalcW(34); Section2Calc(); UpdateVars();
            CalcW(35); Section2Calc(); UpdateVars();
            CalcW(36); Section2Calc(); UpdateVars();
            CalcW(37); Section2Calc(); UpdateVars();
            CalcW(38); Section2Calc(); UpdateVars();
            CalcW(39); Section2Calc(); UpdateVars();

            // Section 3: 40 - 59
            CalcW(40); Section3Calc(); UpdateVars();
            CalcW(41); Section3Calc(); UpdateVars();
            CalcW(42); Section3Calc(); UpdateVars();
            CalcW(43); Section3Calc(); UpdateVars();
            CalcW(44); Section3Calc(); UpdateVars();
            CalcW(45); Section3Calc(); UpdateVars();
            CalcW(46); Section3Calc(); UpdateVars();
            CalcW(47); Section3Calc(); UpdateVars();
            CalcW(48); Section3Calc(); UpdateVars();
            CalcW(49); Section3Calc(); UpdateVars();
            CalcW(50); Section3Calc(); UpdateVars();
            CalcW(51); Section3Calc(); UpdateVars();
            CalcW(52); Section3Calc(); UpdateVars();
            CalcW(53); Section3Calc(); UpdateVars();
            CalcW(54); Section3Calc(); UpdateVars();
            CalcW(55); Section3Calc(); UpdateVars();
            CalcW(56); Section3Calc(); UpdateVars();
            CalcW(57); Section3Calc(); UpdateVars();
            CalcW(58); Section3Calc(); UpdateVars();
            CalcW(59); Section3Calc(); UpdateVars();

            // Section 3: 60 - 79
            CalcW(60); Section4Calc(); UpdateVars();
            CalcW(61); Section4Calc(); UpdateVars();
            CalcW(62); Section4Calc(); UpdateVars();
            CalcW(63); Section4Calc(); UpdateVars();
            CalcW(64); Section4Calc(); UpdateVars();
            CalcW(65); Section4Calc(); UpdateVars();
            CalcW(66); Section4Calc(); UpdateVars();
            CalcW(67); Section4Calc(); UpdateVars();
            CalcW(68); Section4Calc(); UpdateVars();
            CalcW(69); Section4Calc(); UpdateVars();
            CalcW(70); Section4Calc(); UpdateVars();
            CalcW(71); Section4Calc(); UpdateVars();
            CalcW(72); Section4Calc(); UpdateVars();
            CalcW(73); Section4Calc(); UpdateVars();
            CalcW(74); Section4Calc(); UpdateVars();
            CalcW(75); Section4Calc(); UpdateVars();
            CalcW(76); Section4Calc(); UpdateVars();
            CalcW(77); Section4Calc(); UpdateVars();
            CalcW(78); Section4Calc(); UpdateVars();
            CalcW(79); Section4Calc();

            // SHA-1 calculates all the h[x], but we only need the first two for seeds
            ulong part1 = Reorder(temp + H0);
            ulong part2 = Reorder(a + H1);

            ulong seed = (part2 << 32) | part1;
            seed = seed * 0x5d588b656c078965 + 0x269ec3;

            return seed;
        }

        public static uint[] AlphaEncrypt(uint[] message)
        {
            var alpha = new uint[5];
            uint temp = 0;
            uint a = H0;
            uint b = H1;
            uint c = H2;
            uint d = H3;
            uint e = H4;

            del Section1Calc = () => temp = ((a << 5) | (a >> 27)) + ((b & c) | (~b & d)) + e + K0 + temp;
            del UpdateVars = () =>
            {
                e = d;
                d = c;
                c = (b << 30) | (b >> 2);
                b = a;
                a = temp;
            };

            temp = message[0]; Section1Calc(); UpdateVars();
            temp = message[1]; Section1Calc(); UpdateVars();
            temp = message[2]; Section1Calc(); UpdateVars();
            temp = message[3]; Section1Calc(); UpdateVars();
            temp = message[4]; Section1Calc(); UpdateVars();
            temp = message[5]; Section1Calc(); UpdateVars();
            temp = message[6]; Section1Calc(); UpdateVars();
            temp = message[7]; Section1Calc(); UpdateVars();
            temp = message[8]; Section1Calc(); UpdateVars();

            alpha[0] = a;
            alpha[1] = b;
            alpha[2] = c;
            alpha[3] = d;
            alpha[4] = e;

            return alpha;
        }


        public static ulong EncryptSeed(uint[] message, uint[] alpha)
        {
            uint temp = 0;

            uint a = alpha[0];
            uint b = alpha[1];
            uint c = alpha[2];
            uint d = alpha[3];
            uint e = alpha[4];
            del Section1Calc = () => temp = ((a << 5) | (a >> 27)) + ((b & c) | (~b & d)) + e + K0 + temp;
            del Section2Calc = () => temp = ((a << 5) | (a >> 27)) + (b ^ c ^ d) + e + K1 + temp;
            del Section3Calc = () => temp = ((a << 5) | (a >> 27)) + ((b & c) | (b & d) | (c & d)) + e + K2 + temp;
            del Section4Calc = () => temp = ((a << 5) | (a >> 27)) + (b ^ c ^ d) + e + K3 + temp;
            del UpdateVars = () =>
                {
                    e = d;
                    d = c;
                    c = (b << 30) | (b >> 2);
                    b = a;
                    a = temp;
                };

            del2 CalcW = I =>
                {
                    temp = message[I - 3] ^ message[I - 8] ^ message[I - 14] ^ message[I - 16];
                    message[I] = temp = (temp << 1) | (temp >> 31);
                };

            // Section 1: 0-19
            temp = message[9]; Section1Calc(); UpdateVars();
            temp = message[10]; Section1Calc(); UpdateVars();
            temp = message[11]; Section1Calc(); UpdateVars();
            temp = message[12]; Section1Calc(); UpdateVars();
            temp = message[13]; Section1Calc(); UpdateVars();
            temp = message[14]; Section1Calc(); UpdateVars();
            temp = message[15]; Section1Calc(); UpdateVars();

            temp = message[16]; Section1Calc(); UpdateVars();
            CalcW(17); Section1Calc(); UpdateVars();
            temp = message[18]; Section1Calc(); UpdateVars();
            temp = message[19]; Section1Calc(); UpdateVars();

            // Section 2: 20 - 39
            CalcW(20); Section2Calc(); UpdateVars();
            temp = message[21]; Section2Calc(); UpdateVars();
            temp = message[22]; Section2Calc(); UpdateVars();
            CalcW(23); Section2Calc(); UpdateVars();
            temp = message[24]; Section2Calc(); UpdateVars();
            CalcW(25); Section2Calc(); UpdateVars();
            CalcW(26); Section2Calc(); UpdateVars();
            temp = message[27]; Section2Calc(); UpdateVars();
            CalcW(28); Section2Calc(); UpdateVars();
            CalcW(29); Section2Calc(); UpdateVars();
            CalcW(30); Section2Calc(); UpdateVars();
            CalcW(31); Section2Calc(); UpdateVars();
            CalcW(32); Section2Calc(); UpdateVars();
            CalcW(33); Section2Calc(); UpdateVars();
            CalcW(34); Section2Calc(); UpdateVars();
            CalcW(35); Section2Calc(); UpdateVars();
            CalcW(36); Section2Calc(); UpdateVars();
            CalcW(37); Section2Calc(); UpdateVars();
            CalcW(38); Section2Calc(); UpdateVars();
            CalcW(39); Section2Calc(); UpdateVars();

            // Section 3: 40 - 59
            CalcW(40); Section3Calc(); UpdateVars();
            CalcW(41); Section3Calc(); UpdateVars();
            CalcW(42); Section3Calc(); UpdateVars();
            CalcW(43); Section3Calc(); UpdateVars();
            CalcW(44); Section3Calc(); UpdateVars();
            CalcW(45); Section3Calc(); UpdateVars();
            CalcW(46); Section3Calc(); UpdateVars();
            CalcW(47); Section3Calc(); UpdateVars();
            CalcW(48); Section3Calc(); UpdateVars();
            CalcW(49); Section3Calc(); UpdateVars();
            CalcW(50); Section3Calc(); UpdateVars();
            CalcW(51); Section3Calc(); UpdateVars();
            CalcW(52); Section3Calc(); UpdateVars();
            CalcW(53); Section3Calc(); UpdateVars();
            CalcW(54); Section3Calc(); UpdateVars();
            CalcW(55); Section3Calc(); UpdateVars();
            CalcW(56); Section3Calc(); UpdateVars();
            CalcW(57); Section3Calc(); UpdateVars();
            CalcW(58); Section3Calc(); UpdateVars();
            CalcW(59); Section3Calc(); UpdateVars();

            // Section 3: 60 - 79
            CalcW(60); Section4Calc(); UpdateVars();
            CalcW(61); Section4Calc(); UpdateVars();
            CalcW(62); Section4Calc(); UpdateVars();
            CalcW(63); Section4Calc(); UpdateVars();
            CalcW(64); Section4Calc(); UpdateVars();
            CalcW(65); Section4Calc(); UpdateVars();
            CalcW(66); Section4Calc(); UpdateVars();
            CalcW(67); Section4Calc(); UpdateVars();
            CalcW(68); Section4Calc(); UpdateVars();
            CalcW(69); Section4Calc(); UpdateVars();
            CalcW(70); Section4Calc(); UpdateVars();
            CalcW(71); Section4Calc(); UpdateVars();
            CalcW(72); Section4Calc(); UpdateVars();
            CalcW(73); Section4Calc(); UpdateVars();
            CalcW(74); Section4Calc(); UpdateVars();
            CalcW(75); Section4Calc(); UpdateVars();
            CalcW(76); Section4Calc(); UpdateVars();
            CalcW(77); Section4Calc(); UpdateVars();
            CalcW(78); Section4Calc(); UpdateVars();
            CalcW(79); Section4Calc();

            ulong part1 = Reorder(temp + H0);
            ulong part2 = Reorder(a + H1);

            ulong seed = (part2 << 32) | part1;
            seed = seed * 0x5d588b656c078965 + 0x269ec3;

            return seed;
        }

        // speedup by forcing these to be inlined

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint RotateLeft(uint value, int count)
        {
            return (value << count) | (value >> (32 - count));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint RotateRight(uint value, int count)
        {
            return (value >> count) | (value << (32 - count));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint Reorder(uint value)
        {
            value = ((value << 8) & 0xFF00FF00) | ((value >> 8) & 0xFF00FF);
            return (value << 16) | (value >> 16);
        }

        public static string NumericFilter(string input)
        {
            //replace empty strings with 0
            if (input.Length == 0) return "0";

            string returnString = "";

            foreach (char current in input)
            {
                switch (current)
                {
                    case '0':
                    case '1':
                    case '2':
                    case '3':
                    case '4':
                    case '5':
                    case '6':
                    case '7':
                    case '8':
                    case '9':
                        returnString += current;
                        break;
                }
            }

            //if all the input was garbage, set it to 0
            if (returnString.Length == 0) return "0";

            return returnString;
        }

        //----------------------------------------------------------------------------------------------------------------------

        public static int Clip(int value, int min, int max)
        {
            if (value < min) value = min;
            if (value > max) value = max;

            return value;
        }

        public static uint CalculateSeedGen3(DateTime time)
        {
            DateTime start = new DateTime(1999, 12, 31);
            TimeSpan span = time - start;
            var d = (uint) span.TotalDays;
            var h = (uint) time.Hour;
            var m = (uint) time.Minute;

            uint v = 1440*d + 960*(h/10) + 60*(h%10) + 16*(m/10) + m%10;
            uint x = v >> 16;
            uint y = v & 0xFFFF;

            return x ^ y;
        }

        public static uint RNGRange(ulong result, uint max)
        {
            return (uint) ((result*max) >> 32);
        }

        internal static ulong EncryptSeed(DateTime seedTime, Profile profile, uint timer0, uint buttonMashed)
        {
            return EncryptSeed(seedTime, profile.MAC_Address, profile.Version, profile.Language, profile.DSType,
                               profile.SoftReset,
                               profile.VCount, timer0, profile.GxStat, profile.VFrame, buttonMashed);
        }
    }
}