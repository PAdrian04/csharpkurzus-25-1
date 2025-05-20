using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace HP21I8
{
    public interface IMatchRepository
    {
        List<Match> Load();
        void Save(IEnumerable<Match> matches);
    }

    public sealed class JsonFileMatchRepository : IMatchRepository
    {
        private readonly string _filePath;
        public JsonFileMatchRepository(string filePath) => _filePath = filePath;

        public List<Match> Load()
        {
            try
            {
                if (!File.Exists(_filePath))
                {
                    Console.Error.WriteLine($"[FIGYELEM] A '{_filePath}' fájl nem található. Létrehozok mintaadatot...");
                    var sample = SampleData();
                    Save(sample);
                    return sample;
                }

                var json = File.ReadAllText(_filePath);
                return JsonSerializer.Deserialize<List<Match>>(json) ?? new();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[HIBA] Sikertelen betöltés: {ex.Message}");
                return new();
            }
        }

        public void Save(IEnumerable<Match> matches)
        {
            try
            {
                var json = JsonSerializer.Serialize(matches, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(_filePath, json);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[HIBA] Sikertelen mentés: {ex.Message}");
            }
        }

        private static List<Match> SampleData() =>
            new()
            {
                new Match(new DateTime(2025, 3, 1), "2024/25", "Sasok",   "Tigrisek", 3, 1),
                new Match(new DateTime(2025, 3, 2), "2024/25", "Tigrisek", "Farkasok", 3, 2),
                new Match(new DateTime(2025, 3, 5), "2024/25", "Farkasok", "Sasok",   0, 3),
                new Match(new DateTime(2024, 3, 1), "2023/24", "Sasok",   "Tigrisek", 2, 3)
            };
    }

    public sealed class InMemoryMatchRepository : IMatchRepository
    {
        private readonly List<Match> _data;
        public InMemoryMatchRepository(IEnumerable<Match> seed) => _data = seed.ToList();
        public List<Match> Load() => new(_data);
        public void Save(IEnumerable<Match> matches)
        {
            _data.Clear();
            _data.AddRange(matches);
        }
    }
}
