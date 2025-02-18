namespace HttpDemo.SessionStorage
{
    public class MySessionStorage : IMySessionStorage
    {
        private readonly IMySessionStorageEngine _engine;
        private readonly Dictionary<string, ISession> sessions = new Dictionary<string, ISession>(); 
        // dictionary to store sessions

        public MySessionStorage(IMySessionStorageEngine engine)
        {
            _engine = engine;
        }

        public ISession Create()
        {
            var newSession = new MySession(Guid.NewGuid().ToString("N"), _engine);
            sessions[newSession.Id] = newSession;
            return newSession;
        }

        public ISession Get(string id)
        {
            return sessions[id] ?? Create();
        }
    }
}
