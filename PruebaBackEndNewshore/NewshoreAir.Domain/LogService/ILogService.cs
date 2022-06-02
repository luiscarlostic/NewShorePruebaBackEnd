namespace NewshoreAir.Domain.LogService
{
    //Enum de estados de Log
    public enum LogKey
    {
        Begin,
        Url,
        Request,
        MethodType,
        Response,
        Error,
        End,
        Msg
    }

    public interface ILogService
    {
        void Add(LogKey logKey, string msg);
        void Generate();
    }
}
