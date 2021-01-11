using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB
{
    internal class ToLog : IDisposable
    {
        internal SqlConnection sqlConnection = null;

        private SqlCommand sqlAddCommand = null;
        private bool disposedValue;

        internal ToLog(SqlConnection sqlConnection)
        {
            this.sqlConnection = sqlConnection;
            sqlAddCommand = new SqlCommand("insert logs (id_admin, severity, msg) values(@id_admin, @severity, @msg)", sqlConnection);
            sqlAddCommand.Parameters.Add("@id_admin", SqlDbType.Int);
            sqlAddCommand.Parameters.Add("@severity", SqlDbType.Int);
            sqlAddCommand.Parameters.Add("@msg", SqlDbType.NVarChar,200);
        }

        internal void Add(int adminId, EnumSeverity severity, string msg)
        {
            sqlAddCommand.Parameters["@id_admin"].Value = adminId;
            sqlAddCommand.Parameters["@severity"].Value = severity;
            sqlAddCommand.Parameters["@msg"].Value = msg;
            sqlAddCommand.ExecuteNonQuery(); 
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    sqlAddCommand.Dispose();
                }

                // TODO: освободить неуправляемые ресурсы (неуправляемые объекты) и переопределить метод завершения
                // TODO: установить значение NULL для больших полей
                disposedValue = true;
            }
        }

 
        public void Dispose()
        {
            // Не изменяйте этот код. Разместите код очистки в методе "Dispose(bool disposing)".
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
