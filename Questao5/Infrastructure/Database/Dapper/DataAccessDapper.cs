using System.Data;
using Dapper;
using Microsoft.Data.Sqlite;
using Questao5.Infrastructure.Sqlite;

namespace Questao5.Infrastructure.Database.Dapper
{
    public class DataAccessDapper: IDataAccessDapper
    {
        private const string NomeLog = "DataAccessDapper";
        private readonly DatabaseConfig _databaseConfig;

        public DataAccessDapper(DatabaseConfig databaseConfig)
        {
            _databaseConfig = databaseConfig; 
        }

        public IEnumerable<T> IReturn<T>(string sql, object parm = null)
        {
            SqliteConnection myConnection = new SqliteConnection(_databaseConfig.Name);
            try
            {
                var dt = default(IEnumerable<T>);
                using (myConnection)
                {
                    myConnection.Open();
                    dt = myConnection.Query<T>(sql, parm).ToArray();
                }
                return dt;
            }
            catch (SqliteException excp)
            {

                return null;
            }
            catch (Exception excp)
            {
                return null;
            }
            finally
            {
                myConnection.Close();
                myConnection.Dispose();
            }
        }       
        public IEnumerable<T> IReturnStatic<T>(string sql, object parm = null)
        {
            SqliteConnection myConnection = new SqliteConnection(_databaseConfig.Name);
            try
            {
                var dt = default(IEnumerable<T>);
                using (myConnection)
                {
                    myConnection.Open();
                    dt = myConnection.Query<T>(sql, parm).ToArray();
                }
                return dt;
            }
            catch (SqliteException excp)
            {
                return null;
            }
            catch (Exception excp)
            {
                return null;
            }
            finally
            {
                myConnection.Close();
                myConnection.Dispose();
            }
        }
        public async Task<IEnumerable<T>> IReturnAsync<T>(string sql, object parm = null)
        {
            SqliteConnection myConnection = new SqliteConnection(_databaseConfig.Name);
            try
            {
                var dt = default(IEnumerable<T>);
                using (myConnection)
                {
                    myConnection.Open();
                    dt = await myConnection.QueryAsync<T>(sql, parm);
                }
                return dt.ToList();
            }
            catch (SqliteException excp)
            {
                return null;
            }
            catch (Exception excp)
            {
                return null;
            }
            finally
            {
                myConnection.Close();
                myConnection.Dispose();
            }
        }
        public async Task<IEnumerable<T>> IReturnAsyncStatic<T>(string sql, object parm = null)
        {
            SqliteConnection myConnection = new SqliteConnection(_databaseConfig.Name);
            try
            {
                var dt = default(IEnumerable<T>);
                using (myConnection)
                {
                    myConnection.Open();
                    dt = await myConnection.QueryAsync<T>(sql, parm);
                }
                return dt.ToList();
            }
            catch (SqliteException excp)
            {
                return null;
            }
            catch (Exception excp)
            {
                return null;
            }
            finally
            {
                myConnection.Close();
                myConnection.Dispose();
            }
        }
        public int IntReturn(string sqlCount, object parm = null)
        {
            SqliteConnection myConnection = new SqliteConnection(_databaseConfig.Name);
            try
            {
                var intTotal = new int();
                using (myConnection)
                {
                    myConnection.Open();
                    var varRetornoCount = myConnection.ExecuteScalar(sqlCount, parm);
                    var strRetornoCount = "0";
                    if (varRetornoCount != null)
                        strRetornoCount = varRetornoCount.ToString();
                    intTotal = string.IsNullOrEmpty(strRetornoCount) ? 0 : int.Parse(strRetornoCount);
                }
                return intTotal;
            }
            catch (SqliteException excp)
            {
                return 0;
            }
            catch (Exception excp)
            {
                return 0;
            }
            finally
            {
                myConnection.Close();
                myConnection.Dispose();
            }
        }
        private SqliteTransaction myTransaction;
        public int InsertIntReturn(string sql, object parm = null)
        {
            SqliteConnection myConnection = new SqliteConnection(_databaseConfig.Name);
            var intUltimoInserido = new int();
            try
            {
                using (myConnection)
                {
                    myConnection.Open();
                    myTransaction = myConnection.BeginTransaction(IsolationLevel.ReadCommitted);
                    myConnection.Execute(sql, parm);
                    intUltimoInserido = int.Parse(myConnection.ExecuteScalar("SELECT LAST_INSERT_ID();").ToString());
                    myTransaction.Commit();
                }
                return intUltimoInserido;
            }
            catch (SqliteException excp)
            {

                myTransaction.Rollback();
                return 0;
            }
            catch (Exception excp)
            {

                myTransaction.Rollback();
                return 0;
            }
            finally
            {
                myConnection.Close();
                myConnection.Dispose();
            }
        }
        public long InsertLongReturn(string sql, object parm = null)
        {
            SqliteConnection myConnection = new SqliteConnection(_databaseConfig.Name);
            long intUltimoInserido = new long();
            try
            {
                using (myConnection)
                {
                    myConnection.Open();
                    myTransaction = myConnection.BeginTransaction(IsolationLevel.ReadCommitted);
                    myConnection.Execute(sql, parm);
                    intUltimoInserido = Convert.ToInt64(myConnection.ExecuteScalar("SELECT LAST_INSERT_ID();").ToString());
                    myTransaction.Commit();
                }
                return intUltimoInserido;
            }
            catch (SqliteException excp)
            {

                myTransaction.Rollback();
                return 0;
            }
            catch (Exception excp)
            {

                myTransaction.Rollback();
                return 0;
            }
            finally
            {
                myConnection.Close();
                myConnection.Dispose();
            }
        }
        public bool Update(string sql, object parm = null)
        {
            SqliteConnection myConnection = new SqliteConnection(_databaseConfig.Name);
            try
            {
                using (myConnection)
                {
                    myConnection.Open();
                    myTransaction = myConnection.BeginTransaction(IsolationLevel.ReadCommitted);
                    myConnection.Execute(sql, parm);
                    myTransaction.Commit();
                    return true;
                }
            }
            catch (SqliteException excp)
            {

                myTransaction.Rollback();
                return false;
            }
            catch (Exception excp)
            {

                myTransaction.Rollback();
                return false;
            }
            finally
            {
                myConnection.Close();
                myConnection.Dispose();
            }
        }
        public bool Delete(string sql, object parm = null)
        {
            SqliteConnection myConnection = new SqliteConnection(_databaseConfig.Name);
            try
            {
                using (myConnection)
                {
                    myConnection.Open();
                    myTransaction = myConnection.BeginTransaction(IsolationLevel.ReadCommitted);
                    myConnection.Execute(sql, parm);
                    myTransaction.Commit();
                    return true;
                }
            }
            catch (SqliteException excp)
            {
                myTransaction.Rollback();
                return false;
            }
            catch (Exception excp)
            {

                myTransaction.Rollback();
                return false;
            }
            finally
            {
                myConnection.Close();
                myConnection.Dispose();
            }
        }
        public string StringReturn(string sqlCount, object parm = null)
        {
            SqliteConnection myConnection = new SqliteConnection(_databaseConfig.Name);
            try
            {
                var _return = string.Empty;
                using (myConnection)
                {
                    myConnection.Open();
                    _return = myConnection.ExecuteScalar<string>(sqlCount, parm);
                }
                return _return;
            }
            catch (SqliteException excp)
            {
                return string.Empty;
            }
            catch (Exception excp)
            {

                return string.Empty;
            }
            finally
            {
                myConnection.Close();
                myConnection.Dispose();
            }
        }
    }
}
