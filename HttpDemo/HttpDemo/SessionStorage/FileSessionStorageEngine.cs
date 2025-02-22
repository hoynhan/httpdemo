﻿
namespace HttpDemo.SessionStorage
{
    public class FileSessionStorageEngine : IMySessionStorageEngine
    {
        private readonly string _directoryPath;

        public FileSessionStorageEngine(string directoryPath)
        {
            _directoryPath = directoryPath;
        }

        public async Task CommitAsync(string id, Dictionary<string, byte[]> _store, CancellationToken cancellationToken)
        {
            string filePath = Path.Combine(_directoryPath, id);   

            using FileStream fileStream = new FileStream(filePath, FileMode.Create);
            using StreamWriter streamWriter = new StreamWriter(fileStream);
            streamWriter.Write(System.Text.Json.JsonSerializer.Serialize(_store));
            //using (var fileStream = new FileStream(filePath, FileMode.Create))
            //{
            //    foreach (var entry in _store) {
            //    fileStream.WriteAsync(entry.Value, 0, entry.Value.Length, cancellationToken);
            //}
        }

        public async Task<Dictionary<string, byte[]>> LoadAsync(string id, CancellationToken cancellationToken)
        {
            string filePath = Path.Combine(_directoryPath, id);
            if (!File.Exists(filePath))
            {
                return [];
            }
            using FileStream fileStream = new FileStream(filePath, FileMode.Open);
            using StreamReader streamReader = new StreamReader(fileStream);
            var json = await streamReader.ReadToEndAsync(cancellationToken);
            return System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, byte[]>>(json) ?? [];

        }
    }
}
