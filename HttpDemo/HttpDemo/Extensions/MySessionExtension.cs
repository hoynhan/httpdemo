using HttpDemo.SessionStorage;

namespace HttpDemo.Extensions
{
    public static class MySessionExtension
    {
        public const string SessionIDCookieName = "MY_SESSION_ID";

        public static ISession GetSession(this HttpContext context)
        {
            var sessionContainer = context.RequestServices.GetRequiredService<MySessionScopedContainer>();
            if (sessionContainer.Session != null)
            {
                return sessionContainer.Session;
            }
            else
            {
                string? sessionId = context.Request.Cookies[SessionIDCookieName];
                ISession session;

                if (IsSessionIdFormatValid(sessionId))
                {
                    session = context.RequestServices.GetRequiredService<IMySessionStorage>().Get(sessionId!);
                    context.Response.Cookies.Append(SessionIDCookieName, session.Id);

                    sessionContainer.Session = session;

                    return session;

                }
                else
                {
                    session = context.RequestServices.GetRequiredService<IMySessionStorage>().Create();
                    context.Response.Cookies.Append(SessionIDCookieName, session.Id);

                    sessionContainer.Session = session;

                    return session;
                }
            }
        }

        public static bool IsSessionIdFormatValid(string sessionId)
        {
            return !string.IsNullOrEmpty(sessionId) && Guid.TryParse(sessionId, out var _); 
        }
    }
}
