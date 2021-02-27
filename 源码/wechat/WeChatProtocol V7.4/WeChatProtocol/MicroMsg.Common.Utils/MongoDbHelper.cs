using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using MongoDB.Configuration;
using MongoDB.Linq;
using MongoDB.Attributes;
using MongoDB;

namespace DBUtility
{
    public class MongodbHelper<T> where T : class
    {
        string connectionString = string.Empty;
        private static object dbLock = new object();


        string databaseName = string.Empty;

        string collectionName = string.Empty;


        //static MongodbHelper<T> mongodb;

        #region 初始化操作
        /// <summary>
        /// 初始化操作
        /// </summary>
        public MongodbHelper(string collection)
        {
            connectionString = "Servers=192.168.2.5:1000;ConnectTimeout=30000;ConnectionLifetime=300000;MinimumPoolSize=10;MaximumPoolSize=64;Pooled=true";
            databaseName = "wx";
            collectionName = collection;
        }
        #endregion

        #region 实现linq查询的映射配置
        /// <summary>
        /// 实现linq查询的映射配置
        /// </summary>
        public MongoConfiguration configuration
        {
            get
            {
                var config = new MongoConfigurationBuilder();

                config.Mapping(mapping =>
                {
                    mapping.DefaultProfile(profile =>
                    {
                        profile.SubClassesAre(t => t.IsSubclassOf(typeof(T)));
                    });
                    mapping.Map<T>();
                    mapping.Map<T>();
                });

                config.ConnectionString(connectionString);

                return config.BuildConfiguration();
            }
        }
        #endregion

        #region 插入操作
        /// <summary>
        /// 插入操作
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        public void Insert(T t)
        {
            return;
            lock (dbLock)
            {
                using (Mongo mongo = new Mongo(configuration))
                {
                    try
                    {
                        mongo.Connect();

                        var db = mongo.GetDatabase(databaseName);

                        var collection = db.GetCollection<T>(collectionName);

                        collection.Insert(t, true);

                        mongo.Disconnect();

                    }
                    catch (Exception)
                    {
                        mongo.Disconnect();
                        throw;
                    }
                } 
            }
        }
        #endregion

        #region 更新操作
        /// <summary>
        /// 更新操作
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        public void Update(T t, Expression<Func<T, bool>> func)
        {
            return;
            //return;
            lock (dbLock)
            {
                using (Mongo mongo = new Mongo(configuration))
                {
                    try
                    {
                        mongo.Connect();

                        var db = mongo.GetDatabase(databaseName);

                        var collection = db.GetCollection<T>(collectionName);
                        //long count = collection.Linq().Count();
                        collection.Update<T>(t, func, UpdateFlags.Upsert, true);

                        mongo.Disconnect();

                    }
                    catch (Exception)
                    {
                        mongo.Disconnect();
                        throw;
                    }
                } 
            }
        }
        #endregion

        #region 获取集合
        /// <summary>
        ///获取集合
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        public List<T> List(int pageIndex, int pageSize, Expression<Func<T, bool>> func, out int pageCount)
        {
            
            lock (dbLock)
            {
                pageCount = 0;

                using (Mongo mongo = new Mongo(configuration))
                {
                    try
                    {
                        mongo.Connect();

                        var db = mongo.GetDatabase(databaseName);

                        var collection = db.GetCollection<T>(collectionName);

                        pageCount = Convert.ToInt32(collection.Count());

                        var personList = collection.Linq().Where(func).Skip(pageSize * (pageIndex - 1))
                                                       .Take(pageSize).Select(i => i).ToList();

                        mongo.Disconnect();

                        return personList;

                    }
                    catch (Exception)
                    {
                        mongo.Disconnect();

                        throw;
                    }
                } 
            }
        }
        /// <summary>
        /// 按条件 指定字段 降序排列 空列表返回为0
        /// </summary>
        /// <param name="count">查询数据总数</param>
        /// <param name="func">where 查询条件</param>
        /// <param name="order">order 查询字段</param>
        /// <returns></returns>
        public List<T> List(int count, Expression<Func<T, bool>> func, Expression<Func<T, int>> order)
        {

            
            lock (dbLock)
            {
                using (Mongo mongo = new Mongo(configuration))
                {
                    try
                    {
                        mongo.Connect();

                        var db = mongo.GetDatabase(databaseName);

                        var collection = db.GetCollection<T>(collectionName);

                        var personList = collection.Linq().OrderByDescending(order).Where(func).Take(count).ToList();

                        mongo.Disconnect();

                        return personList;

                    }
                    catch (Exception)
                    {
                        mongo.Disconnect();

                        throw;
                    }
                } 
            }
        }
        #endregion

        #region 读取单条记录
        /// <summary>
        ///读取单条记录
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        public T Single(Expression<Func<T, bool>> func)
        {
            
            lock (dbLock)
            {
                using (Mongo mongo = new Mongo(configuration))
                {
                    try
                    {
                        mongo.Connect();

                        var db = mongo.GetDatabase(databaseName);

                        var collection = db.GetCollection<T>(collectionName);

                        var single = collection.Linq().FirstOrDefault(func);

                        mongo.Disconnect();

                        return single;

                    }
                    catch (Exception)
                    {
                        mongo.Disconnect();
                        throw;
                    }
                } 
            }
        }
        #endregion

        #region 删除操作
        /// <summary>
        /// 删除操作
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        public void Delete(Expression<Func<T, bool>> func)
        {
            return;
            lock (dbLock)
            {
                using (Mongo mongo = new Mongo(configuration))
                {
                    try
                    {
                        mongo.Connect();

                        var db = mongo.GetDatabase(databaseName);

                        var collection = db.GetCollection<T>(collectionName);

                        //这个地方要注意，一定要加上T参数，否则会当作object类型处理
                        //导致删除失败
                        collection.Remove<T>(func);

                        mongo.Disconnect();

                    }
                    catch (Exception)
                    {
                        mongo.Disconnect();
                        throw;
                    }
                } 
            }
        }
        #endregion


    }

}
