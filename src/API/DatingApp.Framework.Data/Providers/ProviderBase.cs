using Dapper;
using DatingApp.Common.Configuration;
using DatingApp.Framework.Data.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Framework.Data.Providers
{
    public abstract class ProviderBase(DatingProviderConfiguration configuration, ILoggerFactory loggerFactory) : IProviderBase
    {
        protected readonly DatingProviderConfiguration Configuration = configuration;
        protected readonly ILoggerFactory LoggerFactory = loggerFactory;
        private readonly ILogger<ProviderBase> _logger = loggerFactory.CreateLogger<ProviderBase>();

        protected CommandType CommandType = CommandType.StoredProcedure;

        protected async Task ExecuteNonQueryAsync(string spName, DynamicParameters parameters)
        {
            using var connection = new SqlConnection(Configuration.ConnectionString);
            try
            {
                await connection.OpenAsync();
                await connection.ExecuteAsync(spName, parameters, commandType: CommandType, commandTimeout: Configuration.CommandTimeOut);
            }
            catch (SqlException sqlException)
            {
                HandleSqlException(sqlException);
            }
        }

        protected virtual void LogSqlException(SqlException sqlException)
        {
            _logger.LogError("");
        }

        protected Exception HandleSqlException(SqlException sqlException)
        {
            LogSqlException(sqlException);
            return sqlException;
        }

        private void LogStoredProcedureParams(string spName, Dictionary<string, object> dapperValuePairs)
        {
            _logger.LogInformation("{type} {spName} {dapperValuePairs}", GetType().Name, spName, dapperValuePairs);
        }
    }
}
