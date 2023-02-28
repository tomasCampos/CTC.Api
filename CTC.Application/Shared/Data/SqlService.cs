﻿using CTC.Application.Shared.Request;
using Dapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CTC.Application.Shared.Data
{
    internal sealed class SqlService : ISqlService
    {
        private readonly IDataContext _context;

        public SqlService(IDataContext context)
        {
            _context = context;
        }

        public async Task<int> CountAsync(string sql, object? param = default)
        {
            using var connection = _context.GetConnection();
            return await connection.QueryFirstAsync<int>(sql, param);
        }

        public async Task<int> ExecuteAsync(string sql, object? param = default)
        {
            using var connection = _context.GetConnection();
            return await connection.ExecuteAsync(sql, param);
        }

        public async Task<IEnumerable<T>> SelectAsync<T>(string sql, object? param = default)
        {
            using var connection = _context.GetConnection();
            return await connection.QueryAsync<T>(sql, param);
        }

        public async Task<T> SelectSingleAsync<T>(string sql, object? param = default)
        {
            using var connection = _context.GetConnection();
            return await connection.QuerySingleAsync<T>(sql, param);
        }

        public async Task<bool> ExecuteWithTransactionAsync(IDictionary<string, object?> commands)
        {          
            using var connection = _context.GetConnection();
            connection.Open();

            using var transaction = connection.BeginTransaction();
            try
            {
                foreach (var command in commands)
                {
                    var rowsAffected = await connection.ExecuteAsync(command.Key, command.Value, transaction);
                    if (rowsAffected == 0)
                    {
                        transaction.Rollback();
                        return false;
                    }
                }
            }
            catch
            {
                transaction.Rollback();
                return false;
            }

            transaction.Commit();
            return true;
        }

        public async Task<PaginatedQueryResult<T>> SelectPaginated<T>(QueryRequest queryRequest, string selectStatement, string fromAndJoinsStatements, string whereStatement = "") where T : class
        {
            selectStatement += " {0} {1} {2}";
            if (queryRequest.SearchParam == null)
                whereStatement = string.Empty;
            else
                whereStatement = whereStatement.Replace("@search_param", queryRequest.SearchParam);

            var sqlCount = string.Format("SELECT COUNT(*) {0} {1}", fromAndJoinsStatements, whereStatement);
            var startRow = queryRequest.PageSize * (queryRequest.PageNumber - 1);
            var limitStatement = $"LIMIT {startRow}, {queryRequest.PageSize}";
            var sqlQuery = string.Format(selectStatement, fromAndJoinsStatements, whereStatement, limitStatement);

            using var connection = _context.GetConnection();
            var count = await connection.QueryFirstAsync<int>(sqlCount);
            var data = await connection.QueryAsync<T>(sqlQuery);

            var result = new PaginatedQueryResult<T>(data, count);
            return result;
        }
    }
}
