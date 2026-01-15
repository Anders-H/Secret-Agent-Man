using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using RetroGame.RetroTextures;
using SecretAgentMan.AdaptedTextures;

// ReSharper disable InconsistentNaming

namespace SecretAgentMan.Scenes.Rooms;

public class RoomBackground
{
    public static QuadTexture SkySources { get; }
    public static QuadTexture BgSources { get; }
    public const int Count = 62;
    public static RetroTexture? Building1_18_35 { get; private set; }
    public static RetroTexture? Building2_24_19 { get; private set; }
    public static RetroTexture? Building3_18_61 { get; private set; }
    public static RetroTexture? Building4_3_50 { get; private set; }
    public static RetroTexture? Building5_18_58 { get; private set; }
    public static RetroTexture? Building6_37_26 { get; private set; }
    public static RetroTexture? Building7_18_58 { get; private set; }
    public static RetroTexture? Building8_17_69 { get; private set; }
    public static RetroTexture? Building9_21_50 { get; private set; }
    public static RetroTexture? Building10_33_34 { get; private set; }
    public static RetroTexture? Building11_33_30 { get; private set; }
    public static RetroTexture? Building12_11_58 { get; private set; }
    public static RetroTexture? Building13_24_18 { get; private set; }
    public static RetroTexture? Building14_11_26 { get; private set; }
    public static RetroTexture? Building15_24_50 { get; private set; }
    public static RetroTexture? Building16_18_46 { get; private set; }
    public static RetroTexture? building17_19_55 { get; private set; }
    public static RetroTexture? Building18_18_61 { get; private set; }
    public static RetroTexture? Building19_18_63 { get; private set; }
    public static RetroTexture? Building20_32_22 { get; private set; }
    public static RetroTexture? Building21_11_27 { get; private set; }
    public static RetroTexture? Building22_18_77 { get; private set; }
    public static RetroTexture? Building23_32_18 { get; private set; }
    public static RetroTexture? Building24_19_25 { get; private set; }
    public static RetroTexture? Building25_41_19 { get; private set; }
    public static RetroTexture? Building26_9_18 { get; private set; }
    public static RetroTexture? Building27_47_27 { get; private set; }
    public static RetroTexture? Building28_18_62 { get; private set; }
    public static RetroTexture? Building29_6_2 { get; private set; }
    public static RetroTexture? Building30_74_27 { get; private set; }
    public static RetroTexture? Building31_26_28 { get; private set; }
    public static RetroTexture? Building32_27_30 { get; private set; }
    public static RetroTexture? Building33_11_8 { get; private set; }
    public static RetroTexture? Building34_32_28 { get; private set; }
    public static RetroTexture? Building35_20_15 { get; private set; }
    public static RetroTexture? Building36_55_26 { get; private set; }
    public static RetroTexture? Building37_9_3 { get; private set; }
    public static RetroTexture? Building38_11_26 { get; private set; }
    public static RetroTexture? Building39_17_10 { get; private set; }
    public static RetroTexture? Building40_18_17 { get; private set; }
    public static RetroTexture? Building41_46_16 { get; private set; }
    public static RetroTexture? Building42_11_36 { get; private set; }
    public static RetroTexture? Building43_18_39 { get; private set; }
    public static RetroTexture? Building44_71_77 { get; private set; }
    public static RetroTexture? Building45_26_31 { get; private set; }
    public static RetroTexture? Building46_25_6 { get; private set; }
    public static RetroTexture? Building47_40_50 { get; private set; }
    public static RetroTexture? Building48_19_26 { get; private set; }
    public static RetroTexture? Building49_24_18 { get; private set; }
    public static RetroTexture? Building50_23_26 { get; private set; }
    public static RetroTexture? Building51_48_22 { get; private set; }
    public static RetroTexture? Building52_10_28 { get; private set; }
    public static RetroTexture? Building53_10_32 { get; private set; }
    public static RetroTexture? Building54_10_24 { get; private set; }
    public static RetroTexture? Building55_30_34 { get; private set; }
    public static RetroTexture? Building56_18_53 { get; private set; }
    public static RetroTexture? Building57_27_48 { get; private set; }
    public static RetroTexture? Building58_37_92 { get; private set; }
    public static RetroTexture? Building59_19_11 { get; private set; }
    public static RetroTexture? Building60_61_53 { get; private set; }
    public static RetroTexture? Building61_21_38 { get; private set; }
    public static RetroTexture? Building62_22_46 { get; private set; }
    public RetroTexture? Sky { get; set; }
    public RetroTexture? Bg { get; set; }
    public List<Building> RoomBuildings { get; }

    static RoomBackground()
    {
        SkySources = new QuadTexture();
        BgSources = new QuadTexture();
    }

    public RoomBackground()
    {
        RoomBuildings = [];
    }

    public static void LoadContent(GraphicsDevice graphicsDevice, ContentManager content)
    {
        SkySources.LoadResources(graphicsDevice, content, 640, 91, 1, "bg/sky/Sky1_640-91", "bg/sky/Sky2_640-91", "bg/sky/Sky3_640-91", "bg/sky/Sky4_640-91");
        BgSources.LoadResources(graphicsDevice, content, 640, 91, 1, "bg/bg/BG1_640-91", "bg/bg/BG2_640-91", "bg/bg/BG3_640x91", "bg/bg/BG4_640-91");
        Building1_18_35 = RetroTexture.LoadContent(graphicsDevice, content, 18, 35, 1, "bg/Building1_18-35");
        Building2_24_19 = RetroTexture.LoadContent(graphicsDevice, content, 24, 19, 1, "bg/Building2_24-19");
        Building3_18_61 = RetroTexture.LoadContent(graphicsDevice, content, 18, 61, 1, "bg/Building3_18-61");
        Building4_3_50 = RetroTexture.LoadContent(graphicsDevice, content, 33, 50, 1, "bg/Building4_3-50");
        Building5_18_58 = RetroTexture.LoadContent(graphicsDevice, content, 18, 58, 1, "bg/Building5_18-58");
        Building6_37_26 = RetroTexture.LoadContent(graphicsDevice, content, 37, 26, 1, "bg/Building6_37-26");
        Building7_18_58 = RetroTexture.LoadContent(graphicsDevice, content, 18, 58, 1, "bg/Building7_18-58");
        Building8_17_69 = RetroTexture.LoadContent(graphicsDevice, content, 17, 69, 1, "bg/Building8_17-69");
        Building9_21_50 = RetroTexture.LoadContent(graphicsDevice, content, 21, 50, 1, "bg/Building9_21-50");
        Building10_33_34 = RetroTexture.LoadContent(graphicsDevice, content, 33, 34, 1, "bg/Building10_33-34");
        Building11_33_30 = RetroTexture.LoadContent(graphicsDevice, content, 33, 30, 1, "bg/Building11_33-30");
        Building12_11_58 = RetroTexture.LoadContent(graphicsDevice, content, 11, 58, 1, "bg/Building12_11-58");
        Building13_24_18 = RetroTexture.LoadContent(graphicsDevice, content, 24, 18, 1, "bg/Building13_24-18");
        Building14_11_26 = RetroTexture.LoadContent(graphicsDevice, content, 11, 26, 1, "bg/Building14_11-26");
        Building15_24_50 = RetroTexture.LoadContent(graphicsDevice, content, 24, 50, 1, "bg/Building15_24-50");
        Building16_18_46 = RetroTexture.LoadContent(graphicsDevice, content, 18, 46, 1, "bg/Building16_18-46");
        building17_19_55 = RetroTexture.LoadContent(graphicsDevice, content, 19, 55, 1, "bg/building17_19-55");
        Building18_18_61 = RetroTexture.LoadContent(graphicsDevice, content, 18, 61, 1, "bg/Building18_18-61");
        Building19_18_63 = RetroTexture.LoadContent(graphicsDevice, content, 18, 63, 1, "bg/Building19_18-63");
        Building20_32_22 = RetroTexture.LoadContent(graphicsDevice, content, 32, 22, 1, "bg/Building20_32-22");
        Building21_11_27 = RetroTexture.LoadContent(graphicsDevice, content, 11, 27, 1, "bg/Building21_11-27");
        Building22_18_77 = RetroTexture.LoadContent(graphicsDevice, content, 18, 77, 1, "bg/Building22_18-77");
        Building23_32_18 = RetroTexture.LoadContent(graphicsDevice, content, 32, 18, 1, "bg/Building23_32-18");
        Building24_19_25 = RetroTexture.LoadContent(graphicsDevice, content, 19, 25, 1, "bg/Building24_19-25");
        Building25_41_19 = RetroTexture.LoadContent(graphicsDevice, content, 41, 19, 1, "bg/Building25_41-19");
        Building26_9_18 = RetroTexture.LoadContent(graphicsDevice, content, 9, 18, 1, "bg/Building26_9-18");
        Building27_47_27 = RetroTexture.LoadContent(graphicsDevice, content, 47, 27, 1, "bg/Building27_47-27");
        Building28_18_62 = RetroTexture.LoadContent(graphicsDevice, content, 18, 62, 1, "bg/Building28_18-62");
        Building29_6_2 = RetroTexture.LoadContent(graphicsDevice, content, 6, 2, 1, "bg/Building29_6-2");
        Building30_74_27 = RetroTexture.LoadContent(graphicsDevice, content, 74, 27, 1, "bg/Building30_74-27");
        Building31_26_28 = RetroTexture.LoadContent(graphicsDevice, content, 26, 68, 1, "bg/Building31_26-28");
        Building32_27_30 = RetroTexture.LoadContent(graphicsDevice, content, 27, 30, 1, "bg/Building32_27-30");
        Building33_11_8 = RetroTexture.LoadContent(graphicsDevice, content, 11, 8, 1, "bg/Building33_11-8");
        Building34_32_28 = RetroTexture.LoadContent(graphicsDevice, content, 32, 18, 1, "bg/Building34_32-28");
        Building35_20_15 = RetroTexture.LoadContent(graphicsDevice, content, 20, 15, 1, "bg/Building35_20-15");
        Building36_55_26 = RetroTexture.LoadContent(graphicsDevice, content, 55, 26, 1, "bg/Building36_55-26");
        Building37_9_3 = RetroTexture.LoadContent(graphicsDevice, content, 9, 3, 1, "bg/Building37_9-3");
        Building38_11_26 = RetroTexture.LoadContent(graphicsDevice, content, 11, 26, 1, "bg/Building38_11-26");
        Building39_17_10 = RetroTexture.LoadContent(graphicsDevice, content, 17, 10, 1, "bg/Building39_17-10");
        Building40_18_17 = RetroTexture.LoadContent(graphicsDevice, content, 18, 17, 1, "bg/Building40_18-17");
        Building41_46_16 = RetroTexture.LoadContent(graphicsDevice, content, 46, 16, 1, "bg/Building41_46-16");
        Building42_11_36 = RetroTexture.LoadContent(graphicsDevice, content, 11, 36, 1, "bg/Building42_11-36");
        Building43_18_39 = RetroTexture.LoadContent(graphicsDevice, content, 18, 39, 1, "bg/Building43_18-39");
        Building44_71_77 = RetroTexture.LoadContent(graphicsDevice, content, 71, 77, 1, "bg/Building44_71-77");
        Building45_26_31 = RetroTexture.LoadContent(graphicsDevice, content, 26, 31, 1, "bg/Building45_26-31");
        Building46_25_6 = RetroTexture.LoadContent(graphicsDevice, content, 25, 6, 1, "bg/Building46_25-6");
        Building47_40_50 = RetroTexture.LoadContent(graphicsDevice, content, 40, 50, 1, "bg/Building47_40-50");
        Building48_19_26 = RetroTexture.LoadContent(graphicsDevice, content, 19, 26, 1, "bg/Building48_19-26");
        Building49_24_18 = RetroTexture.LoadContent(graphicsDevice, content, 24, 18, 1, "bg/Building49_24-18");
        Building50_23_26 = RetroTexture.LoadContent(graphicsDevice, content, 23, 26, 1, "bg/Building50_23-26");
        Building51_48_22 = RetroTexture.LoadContent(graphicsDevice, content, 48, 22, 1, "bg/Building51_48-22");
        Building52_10_28 = RetroTexture.LoadContent(graphicsDevice, content, 10, 28, 1, "bg/Building52_10-28");
        Building53_10_32 = RetroTexture.LoadContent(graphicsDevice, content, 10, 32, 1, "bg/Building53_10-32");
        Building54_10_24 = RetroTexture.LoadContent(graphicsDevice, content, 10, 24, 1, "bg/Building54_10-24");
        Building55_30_34 = RetroTexture.LoadContent(graphicsDevice, content, 30, 34, 1, "bg/Building55_30-34");
        Building56_18_53 = RetroTexture.LoadContent(graphicsDevice, content, 18, 53, 1, "bg/Building56_18-53");
        Building57_27_48 = RetroTexture.LoadContent(graphicsDevice, content, 27, 48, 1, "bg/Building57_27-48");
        Building58_37_92 = RetroTexture.LoadContent(graphicsDevice, content, 37, 92, 1, "bg/Building58_37-92");
        Building59_19_11 = RetroTexture.LoadContent(graphicsDevice, content, 19, 11, 1, "bg/Building59_19-11");
        Building60_61_53 = RetroTexture.LoadContent(graphicsDevice, content, 61, 53, 1, "bg/Building60_61-53");
        Building61_21_38 = RetroTexture.LoadContent(graphicsDevice, content, 21, 38, 1, "bg/Building61_21-38");
        Building62_22_46 = RetroTexture.LoadContent(graphicsDevice, content, 22, 46, 1, "bg/Building62_22-46");
    }

    public static RetroTexture? GetByIndex(int index) =>
        index switch
        {
            0 => Building1_18_35,
            1 => Building2_24_19,
            2 => Building3_18_61,
            3 => Building4_3_50,
            4 => Building5_18_58,
            5 => Building6_37_26,
            6 => Building7_18_58,
            7 => Building8_17_69,
            8 => Building9_21_50,
            9 => Building10_33_34,
            10 => Building11_33_30,
            11 => Building12_11_58,
            12 => Building13_24_18,
            13 => Building14_11_26,
            14 => Building15_24_50,
            15 => Building16_18_46,
            16 => building17_19_55,
            17 => Building18_18_61,
            18 => Building19_18_63,
            19 => Building20_32_22,
            20 => Building21_11_27,
            21 => Building22_18_77,
            22 => Building23_32_18,
            23 => Building24_19_25,
            24 => Building25_41_19,
            25 => Building26_9_18,
            26 => Building27_47_27,
            27 => Building28_18_62,
            28 => Building29_6_2,
            29 => Building30_74_27,
            30 => Building31_26_28,
            31 => Building32_27_30,
            32 => Building33_11_8,
            33 => Building34_32_28,
            34 => Building35_20_15,
            35 => Building36_55_26,
            36 => Building37_9_3,
            37 => Building38_11_26,
            38 => Building39_17_10,
            39 => Building40_18_17,
            40 => Building41_46_16,
            41 => Building42_11_36,
            42 => Building43_18_39,
            43 => Building44_71_77,
            44 => Building45_26_31,
            45 => Building46_25_6,
            46 => Building47_40_50,
            47 => Building48_19_26,
            48 => Building49_24_18,
            49 => Building50_23_26,
            50 => Building51_48_22,
            51 => Building52_10_28,
            52 => Building53_10_32,
            53 => Building54_10_24,
            54 => Building55_30_34,
            55 => Building56_18_53,
            56 => Building57_27_48,
            57 => Building58_37_92,
            58 => Building59_19_11,
            59 => Building60_61_53,
            60 => Building61_21_38,
            61 => Building62_22_46,
            _ => null
        };

    public void AddBuilding(RetroTexture building, int x) =>
        RoomBuildings.Add(new Building(building, x));

    public void DrawDecorationBackground(SpriteBatch spriteBatch)
    {
        Sky?.Draw(spriteBatch, 0, 0, 0);
        Bg?.Draw(spriteBatch, 0, 0, 0);

        foreach (var roomBuilding in RoomBuildings)
            roomBuilding.Texture.Draw(spriteBatch, 0, roomBuilding.X, roomBuilding.Y);
    }
}