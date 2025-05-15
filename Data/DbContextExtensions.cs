using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace ErpMobile.Api.Data
{
    public static class DbContextExtensions
    {
        /// <summary>
        /// Executes a SQL command and returns a scalar value
        /// </summary>
        public static async Task<T> ExecuteScalarAsync<T>(this DbContext context, string sql, params SqlParameter[] parameters)
        {
            using var command = context.Database.GetDbConnection().CreateCommand();
            command.CommandText = sql;
            command.CommandType = CommandType.Text;
            
            if (parameters != null)
            {
                foreach (var parameter in parameters)
                {
                    command.Parameters.Add(parameter);
                }
            }

            if (command.Connection.State != ConnectionState.Open)
            {
                await command.Connection.OpenAsync();
            }

            var result = await command.ExecuteScalarAsync();
            return result == DBNull.Value ? default : (T)result;
        }

        /// <summary>
        /// Executes a SQL command and returns the number of rows affected
        /// </summary>
        public static async Task<int> ExecuteNonQueryAsync(this DbContext context, string sql, params SqlParameter[] parameters)
        {
            using var command = context.Database.GetDbConnection().CreateCommand();
            command.CommandText = sql;
            command.CommandType = CommandType.Text;
            
            if (parameters != null)
            {
                foreach (var parameter in parameters)
                {
                    command.Parameters.Add(parameter);
                }
            }

            if (command.Connection.State != ConnectionState.Open)
            {
                await command.Connection.OpenAsync();
            }

            return await command.ExecuteNonQueryAsync();
        }

        /// <summary>
        /// Executes a SQL command and returns a data reader
        /// </summary>
        public static async Task<IDataReader> ExecuteReaderAsync(this DbContext context, string sql, params SqlParameter[] parameters)
        {
            var command = context.Database.GetDbConnection().CreateCommand();
            command.CommandText = sql;
            command.CommandType = CommandType.Text;
            
            if (parameters != null)
            {
                foreach (var parameter in parameters)
                {
                    command.Parameters.Add(parameter);
                }
            }

            if (command.Connection.State != ConnectionState.Open)
            {
                await command.Connection.OpenAsync();
            }

            return await command.ExecuteReaderAsync(CommandBehavior.CloseConnection);
        }
    }
}
