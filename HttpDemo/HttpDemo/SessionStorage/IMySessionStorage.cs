namespace HttpDemo.SessionStorage
{
    public interface IMySessionStorage
    {
        ISession Create(); // create a new session and it generates a new id
        ISession Get(string id); // get a session by id or create a new one if it doesn't exist
    }
}
