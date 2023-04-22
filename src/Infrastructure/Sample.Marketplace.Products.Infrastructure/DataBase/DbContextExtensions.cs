using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Data.Common;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;

namespace Sample.Marketplace.Products.Infrastructure.DataBase
{
    public static class DbContextExtensions
    {
        public static MultiResultSetReader MultiResultSetSqlQuery(this DbContext context, string query, params Microsoft.Data.SqlClient.SqlParameter[] parameters)
        {
            return new MultiResultSetReader(context, query, parameters);
        }
    }

    public class MultiResultSetReader : IDisposable
    {
        private readonly DbContext _context;
        private readonly DbCommand _command;
        private bool _connectionNeedsToBeClosed;
        private DbDataReader _reader;

        public MultiResultSetReader(DbContext context, string query, Microsoft.Data.SqlClient.SqlParameter[] parameters)
        {
            _context = context;
            _command = _context.Database.GetDbConnection().CreateCommand();
            _command.CommandText = query;

            if (parameters != null && parameters.Any())
            { 
                _command.Parameters.AddRange(parameters); 
            }
        }


        public void Dispose()
        {
            if (_reader != null)
            {
                _reader.Dispose();
                _reader = null;
            }

            if (_connectionNeedsToBeClosed)
            {
                _context.Database.GetDbConnection().Close();
                _connectionNeedsToBeClosed = false;
            }
        }

        public IList<T> ResultSetFor<T>()
        {
            if (_reader == null)
            {
                _reader = GetReader();
            }
            else
            {
                _reader.NextResult();
            }
            return DataReaderMapToList<T>(_reader);
        }

        private DbDataReader GetReader()
        {
            if (_context.Database.GetDbConnection().State != ConnectionState.Open)
            {
                _context.Database.GetDbConnection().Open();
                _connectionNeedsToBeClosed = true;
            }

            return _command.ExecuteReader();
        }

        public List<T> DataReaderMapToList<T>(IDataReader dr)
        {
            List<T> list = new List<T>();
            T obj = default(T);
            while (dr.Read())
            {
                obj = Activator.CreateInstance<T>();
                foreach (PropertyInfo prop in obj.GetType().GetProperties())
                {
                    
                    if (!object.Equals(dr[prop.Name], DBNull.Value))
                    {
                        prop.SetValue(obj, dr[prop.Name], null);
                    }
                }
                list.Add(obj);
            }
            return list;
        }
    }
}
