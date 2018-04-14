namespace BaseDBLibrary
{
    public abstract class ConnectionBase : DBBaseClass
    {
        protected string connString;

        public abstract void setConnString(string connString);
        public abstract void Connect();
        public abstract void Disconnect();
    }
}
