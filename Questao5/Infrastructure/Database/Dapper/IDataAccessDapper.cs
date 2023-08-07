namespace Questao5.Infrastructure.Database.Dapper
{
    public interface IDataAccessDapper
    {
        public IEnumerable<T> IReturn<T>(string sql, object parm = null);
        public IEnumerable<T> IReturnStatic<T>(string sql, object parm = null);
        public Task<IEnumerable<T>> IReturnAsync<T>(string sql, object parm = null);
        public Task<IEnumerable<T>> IReturnAsyncStatic<T>(string sql, object parm = null);
        public int IntReturn(string sqlCount, object parm = null);
        public int InsertIntReturn(string sql, object parm = null);
        public long InsertLongReturn(string sql, object parm = null);
        public bool Update(string sql, object parm = null);
        public bool Delete(string sql, object parm = null);
        public string StringReturn(string sqlCount, object parm = null);

    }
}
