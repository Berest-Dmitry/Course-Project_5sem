using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace course_proj_english_tutorial.Common
{
	public class SQLMethods
	{
		/// <summary>
		/// метод проверки наличия БД
		/// </summary>
		public bool CheckDatabase(string ConnectionString, string DBName, string serverName, string user, string password)
		{
			bool bRet = false;
			using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
			{

				var smoServer = new Server(new ServerConnection(serverName, user, password));
				bRet = smoServer.Databases.Contains(DBName);

			}
			return bRet;
		}

        public bool ExecuteScript(string ScriptContent, string ConnectionString, out Exception Error, bool DataBaseCreating = false, bool ReplaceBlock = false, string ExistingBlock = "", string NewBlock = "")
        {
            try
            {
                if (ReplaceBlock)
                {
                    ScriptContent = ScriptContent.Replace("[" + ExistingBlock + "]", "[" + NewBlock + "]");
                    ScriptContent = ScriptContent.Replace(ExistingBlock, NewBlock);
                }
                if (DataBaseCreating)
                {
                    var Builder = new SqlConnectionStringBuilder(ConnectionString);
                    var server = new Server(new ServerConnection(Builder.DataSource, Builder.UserID, Builder.Password));
                    server.ConnectionContext.ExecuteNonQuery(ScriptContent);
                }
                else
                {
                    using (var connection = new Microsoft.Data.SqlClient.SqlConnection(ConnectionString))
                    {
                        var server = new Server(new ServerConnection(sqlConnection: connection));
                        server.ConnectionContext.ExecuteNonQuery(ScriptContent);
                    }
                }
                Error = null;
                return true;
            }
            catch (Exception exp)
            {
                Error = exp;
                return false;
            }
        }
        /// <summary>
        /// метод выполнения скрипта из файла
        /// </summary>
        /// <param name="PathToFile"></param>
        /// <param name="ConnectionString"></param>
        /// <param name="Error"></param>
        /// <param name="DataBaseCreating"></param>
        /// <param name="ReplaceBlock"></param>
        /// <param name="ExistingBlock"></param>
        /// <param name="NewBlock"></param>
        /// <returns></returns>
        public bool ExecuteScriptFromFile(string PathToFile, string ConnectionString, out Exception Error, bool DataBaseCreating = false, bool ReplaceBlock = false, string ExistingBlock = "", string NewBlock = "")
        {
            try
            {
                var LoadedScriptContent = File.ReadAllText(PathToFile);
                return ExecuteScript(LoadedScriptContent, ConnectionString, out Error, DataBaseCreating, ReplaceBlock, ExistingBlock, NewBlock);
            }
            catch (Exception exp)
            {
                Error = exp;
                return false;
            }
        }
    }
}
