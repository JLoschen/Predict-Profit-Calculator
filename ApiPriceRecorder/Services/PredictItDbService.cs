using ApiPriceRecorder.DataAccess;
using ApiPriceRecorder.Model;
using ApiPriceRecorder.Services.Abstractions;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;

namespace ApiPriceRecorder.Services
{
    public class PredictItDbService : IPredictItDbService
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;
        public PredictItDbService(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public bool InsertMarket(MarketDbModel market)
        {
            //select * from Market;
            var sql = @"INSERT INTO Market (Name, url) values (@Name, @Url);";
            using(var con = _dbConnectionFactory.CreateConnection())
            {
                try
                {
                    con.Execute(sql, market);
                    return true;
                }
                catch(Exception e)
                {
                    Debug.WriteLine(e.Message);
                    return false;
                }
            }
        }

        public IEnumerable<MarketDbModel> GetMarkets()
        {
            using (var con = _dbConnectionFactory.CreateConnection())
            {
                try
                {
                    var sql = "select * from Market;";
                    return con.Query<MarketDbModel>(sql).ToList();
                }
                catch (Exception e)
                {
                    return new List<MarketDbModel>();
                }
            }
        }
    }
}