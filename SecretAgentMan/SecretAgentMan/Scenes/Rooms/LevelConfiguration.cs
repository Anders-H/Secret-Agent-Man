using System;
using System.Globalization;

namespace SecretAgentMan.Scenes.Rooms;

internal sealed class LevelConfiguration
{
    public const int RoomCount = 10;
    public string[] Names { get; } = new string[RoomCount];
    public int[] Innocents { get; private set; } = new int[RoomCount];
    public int[] Spies { get; private set; } = new int[RoomCount];
    public int[] Coins { get; private set; } = new int[RoomCount];
    public int[] Ammo { get; private set; } = new int[RoomCount];

    public static LevelConfiguration Load(int zeroBasedLevel, Func<string, string?> getSetting, Random random)
    {
        var result = new LevelConfiguration();
        var prefix = $"Level{zeroBasedLevel + 1}";
        if (zeroBasedLevel < 3)
        {
            var names = (getSetting(prefix + "RoomNames")
                ?? throw new FormatException($"Missing {prefix}RoomNames.")).Split(';', StringSplitOptions.TrimEntries);

            if (names.Length != RoomCount || Array.Exists(names, string.IsNullOrWhiteSpace))
                throw new FormatException($"{prefix}RoomNames must contain {RoomCount} nonempty names.");

            for (var i = 0; i < RoomCount; i++)
                result.Names[i] = names[i].ToLowerInvariant();

            result.Innocents = ParseCounts(prefix + "InnocentNpcCount", getSetting(prefix + "InnocentNpcCount"));
            result.Spies = ParseCounts(prefix + "SpyCount", getSetting(prefix + "SpyCount"));
        }
        else
        {
            var generator = new FantasyGameNameGenerator.NameGenerator();

            for (var i = 0; i < RoomCount; i++)
            {
                result.Names[i] = generator.GenerateName().ToLowerInvariant();
                result.Innocents[i] = random.Next(30) + 1;
                result.Spies[i] = random.Next(10 * i) + 1;
            }
        }

        result.Coins = LoadOptionalCounts(prefix + "Coins", getSetting, random, 10);
        result.Ammo = LoadOptionalCounts(prefix + "Ammo", getSetting, random, 4);
        return result;
    }

    private static int[] LoadOptionalCounts(string key, Func<string, string?> getSetting, Random random, int maximum)
    {
        var value = getSetting(key);

        if (value != null)
            return ParseCounts(key, value);

        var counts = new int[RoomCount];

        for (var i = 0; i < counts.Length; i++)
            counts[i] = random.Next(maximum);

        return counts;
    }

    internal static int[] ParseCounts(string key, string? value)
    {
        var parts = value?.Split(',', StringSplitOptions.TrimEntries);

        if (parts == null || parts.Length != RoomCount)
            throw new FormatException($"{key} must contain {RoomCount} counts.");

        var result = new int[RoomCount];

        for (var i = 0; i < result.Length; i++)
        {
            if (!int.TryParse(parts[i], NumberStyles.Integer, CultureInfo.InvariantCulture, out result[i]) || result[i] < 0)
                throw new FormatException($"{key} contains an invalid count at room {i + 1}.");
        }

        return result;
    }
}
