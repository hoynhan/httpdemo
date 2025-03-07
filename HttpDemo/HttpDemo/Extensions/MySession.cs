﻿using HttpDemo.SessionStorage;
using System.Diagnostics.CodeAnalysis;

namespace HttpDemo.Extensions
{
    public class MySession(string id, IMySessionStorageEngine engine) : ISession
    {
        private readonly Dictionary<string, byte[]> _store = new Dictionary<string, byte[]>();
        // dictionary to store information inside the session
        public bool IsAvailable {
            get
            {
                LoadAsync(CancellationToken.None).Wait();
                return true;
            }
        }

        public string Id => id;

        public IEnumerable<string> Keys => _store.Keys;

        public void Clear()
        {
           _store.Clear();
        }

        public async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            await engine.CommitAsync(Id, _store, cancellationToken);
        }

        public async Task LoadAsync(CancellationToken cancellationToken = default)
        {
            _store.Clear();
            var loadedStore = await engine.LoadAsync(Id, cancellationToken);

            foreach (var pair in loadedStore)
            {
                _store[pair.Key] = pair.Value;
            }

        }

        public void Remove(string key)
        {
            _store.Remove(key);
        }

        public void Set(string key, byte[] value)
        {
            _store[key] = value;
        }

        public bool TryGetValue(string key, [NotNullWhen(true)] out byte[]? value)
        {
            return _store.TryGetValue(key, out value);
        }
    }
}
