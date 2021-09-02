using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Snblog.IRepository;
using Snblog.IRepository.IRepository;
using Snblog.Models;

namespace Snblog.Repository.Repository
{
    // Repository.cs仓储类，它是一个泛型类，并且拥有一个带有参数的构造方法，通过构造方法获得当前DbContext上下文对象，
    //泛型类为指定Model类型，通过DbContext.Set<T>()方法最终得到相应的DbSet<T>对象来操作工作单元。
    //实现了CRUD基本功能的封装
    public class Repositorys<T> : IRepositorys<T> where T : class
    {
        private readonly SnblogContext _dbContext;
        private readonly DbSet<T> _dbSet;
        private readonly string _connStr;

        public Repositorys(IConcardContext mydbcontext)
        {
            _dbContext = mydbcontext as SnblogContext;
            if (_dbContext == null)
            {
                return;
            }

            _dbSet = _dbContext.Set<T>();
            _connStr = _dbContext.Database.GetDbConnection().ConnectionString;
        }

        public void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.Unspecified)
        {
            if (_dbContext.Database.CurrentTransaction == null)
            {
                _dbContext.Database.BeginTransaction(isolationLevel);
            }
        }

        public void Commit()
        {
            var transaction = this._dbContext.Database.CurrentTransaction;
            if (transaction != null)
            {
                try
                {
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public void Rollback()
        {
            if (_dbContext.Database.CurrentTransaction != null)
            {
                _dbContext.Database.CurrentTransaction.Rollback();
            }
        }

        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }
        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }


        public IQueryable<T> Entities
        {
            get { return _dbSet.AsNoTracking(); }
        }

        public IQueryable<T> TrackEntities
        {
            get { return _dbSet; }
        }

        public T Add(T entity, bool isSave = true)
        {

            _dbSet.Add(entity);
            if (isSave)
            {
                SaveChanges();
            }
            return entity;
        }

        public async Task<T> AddAsync(T entity, bool isSave = true)
        {
            await _dbSet.AddAsync(entity);
            if (isSave)
            {
                await SaveChangesAsync();
            }
            return entity;
        }

        public void AddRange(IEnumerable<T> entitys, bool isSave = true)
        {
            _dbSet.AddRange(entitys);
            if (isSave)
            {
                SaveChanges();
            }
        }

        public void Delete(T entity, bool isSave = true)
        {
            this._dbSet.Remove(entity);
            if (isSave)
            {
                this.SaveChanges();
            }
        }

        public void Delete(bool isSave = true, params T[] entitys)
        {
            this._dbSet.RemoveRange(entitys);
            if (isSave)
            {
                this.SaveChanges();
            }
        }

        public async Task<int> DeleteAsync(object id)
        {
            //执行查询
            var todoItem = await _dbSet.FindAsync(id);
            int data;
            if (todoItem == null)
            {
                data = 0;
            }
            else
            {
                _dbSet.Remove(todoItem);
                data = SaveChanges();
            }
            return data;
        }

        public int Delete(object id)
        {
            //执行查询
            var todoItem = _dbSet.Find(id);
            int de;
            if (todoItem == null)
            {
                //return NotFound();
                de = 0;
            }
            else
            {
                _dbSet.Remove(todoItem);
                de = SaveChanges();
            }
            return de;
        }

        public void Delete(Expression<Func<T, bool>> @where, bool isSave = true)
        {
            T[] entitys = this._dbSet.Where(@where).ToArray();
            if (entitys.Length > 0)
            {
                this._dbSet.RemoveRange(entitys);
            }
            if (isSave)
            {
                this.SaveChanges();
            }
        }

        public async Task<int> UpdateAsync(T entity)
        {
            var entry = _dbContext.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                entry.State = EntityState.Modified;
            }
            var da = await Task.Run(SaveChangesAsync);
            return da;
        }

        public int Update(T entity)
        {

            var entry = _dbContext.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                entry.State = EntityState.Modified;
            }
            var da = SaveChanges();
            return da;
        }
        public void Update(params T[] entitys)
        {
            var entry = this._dbContext.Entry(entitys);
            if (entry.State == EntityState.Detached)
            {
                entry.State = EntityState.Modified;
            }
            SaveChanges();
        }

        public bool Any(Expression<Func<T, bool>> @where)
        {
            return _dbSet.AsNoTracking().Any(@where);
        }

        public int Count()
        {
            return _dbSet.AsNoTracking().Count();
        }

        public int Count(Expression<Func<T, bool>> @where)
        {
            return _dbSet.AsNoTracking().Count(@where);
        }

        public T FirstOrDefault(Expression<Func<T, bool>> @where)
        {
            return _dbSet.AsNoTracking().FirstOrDefault(@where);
        }

        public T FirstOrDefault<TOrder>(Expression<Func<T, bool>> @where, Expression<Func<T, TOrder>> order, bool isDesc = false)
        {
            if (isDesc)
            {
                return this._dbSet.AsNoTracking().OrderByDescending(order).FirstOrDefault(@where);
            }
            else
            {
                return this._dbSet.AsNoTracking().OrderBy(order).FirstOrDefault(@where);
            }
        }

        public IQueryable<T> Distinct(Expression<Func<T, bool>> @where)
        {
            return this._dbSet.AsNoTracking().Where(@where).Distinct();

        }


        public IQueryable<T> Where(Expression<Func<T, bool>> @where)
        {
            return this._dbSet.Where(@where);
        }

        public IQueryable<T> Where<TOrder>(Expression<Func<T, bool>> @where, Expression<Func<T, TOrder>> order, bool isDesc = false)
        {
            if (isDesc)
            {
                return this._dbSet.Where(@where).OrderByDescending(order);
            }
            else
            {
                return this._dbSet.Where(@where).OrderBy(order);
            }
        }


        public IEnumerable<T> Wherepage<TOrder>(Func<T, bool> @where, Func<T, TOrder> order, int pageIndex, int pageSize, out int count, bool isDesc)
        {
            count = Count();
            if (isDesc)
            {

                return _dbSet.Where(@where).OrderByDescending(order).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            }
            else
            {
                return this._dbSet.Where(@where).OrderBy(order).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            }
        }
        public async Task<IEnumerable<T>> WherepageAsync<TOrder>(Func<T, bool> @where, Func<T, TOrder> order, int pageIndex, int pageSize, bool isDesc)
        {
            if (isDesc)
            {
                return await Task.Run(() => _dbSet.Where(@where).OrderByDescending(order).Skip((pageIndex - 1) * pageSize).Take(pageSize));
            }
            else
            {
                return await Task.Run(() => _dbSet.Where(@where).OrderBy(order).Skip((pageIndex - 1) * pageSize).Take(pageSize));
            }
        }

        public IEnumerable<T> Where<TOrder>(Func<T, bool> @where, Func<T, TOrder> order, int pageIndex, int pageSize, out int count, bool isDesc = false)
        {
            count = Count();
            if (isDesc)
            {
                return this._dbSet.Where(@where).OrderByDescending(order).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            }
            else
            {
                return this._dbSet.Where(@where).OrderBy(order).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            }
        }

        public IQueryable<T> Where<TOrder>(Expression<Func<T, bool>> @where, Expression<Func<T, TOrder>> order, int pageIndex, int pageSize, out int count, bool isDesc = false)
        {
            count = Count();
            if (isDesc)
            {
                return this._dbSet.Where(@where).OrderByDescending(order).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            }
            else
            {
                return this._dbSet.Where(@where).OrderBy(order).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            }
        }

        public IQueryable<T> GetAll()
        {
            return _dbSet.AsNoTracking();
        }

        public IQueryable<T> GetAll<TOrder>(Expression<Func<T, TOrder>> order, bool isDesc = false)
        {
            if (isDesc)
            {
                return this._dbSet.AsNoTracking().OrderByDescending(order);
            }
            else
            {
                return this._dbSet.AsNoTracking().OrderBy(order);
            }
        }

        public T GetById<TType>(TType id)
        {
            return this._dbSet.Find(id);
        }
        public async Task<T> GetByIdAsync<TType>(TType id)
        {
            return await _dbSet.FindAsync(id);
        }


        public TType Max<TType>(Expression<Func<T, TType>> column)
        {
            if (this._dbSet.AsNoTracking().Any())
            {
                return this._dbSet.AsNoTracking().Max(column);
            }
            return default;
        }

        public TType Max<TType>(Expression<Func<T, TType>> column, Expression<Func<T, bool>> @where)
        {
            if (this._dbSet.AsNoTracking().Any(@where))
            {
                return this._dbSet.AsNoTracking().Where(@where).Max(column);
            }
            return default;
        }

        public TType Min<TType>(Expression<Func<T, TType>> column)
        {
            if (this._dbSet.AsNoTracking().Any())
            {
                return this._dbSet.AsNoTracking().Min(column);
            }
            return default;
        }

        public TType Min<TType>(Expression<Func<T, TType>> column, Expression<Func<T, bool>> @where)
        {
            if (this._dbSet.AsNoTracking().Any(@where))
            {
                return this._dbSet.AsNoTracking().Where(@where).Min(column);
            }
            return default;
        }

        public TType Sum<TType>(Expression<Func<T, TType>> selector, Expression<Func<T, bool>> @where) where TType : new()
        {
            object result = 0;

            if (new TType().GetType() == typeof(decimal))
            {
                result = this._dbSet.AsNoTracking().Where(where).Sum(selector as Expression<Func<T, decimal>>);
            }
            if (new TType().GetType() == typeof(decimal?))
            {
                result = this._dbSet.AsNoTracking().Where(where).Sum(selector as Expression<Func<T, decimal?>>);
            }
            if (new TType().GetType() == typeof(double))
            {
                result = this._dbSet.AsNoTracking().Where(where).Sum(selector as Expression<Func<T, double>>);
            }
            if (new TType().GetType() == typeof(double?))
            {
                result = this._dbSet.AsNoTracking().Where(where).Sum(selector as Expression<Func<T, double?>>);
            }
            if (new TType().GetType() == typeof(float))
            {
                result = this._dbSet.AsNoTracking().Where(where).Sum(selector as Expression<Func<T, float>>);
            }
            if (new TType().GetType() == typeof(float?))
            {
                result = this._dbSet.AsNoTracking().Where(where).Sum(selector as Expression<Func<T, float?>>);
            }
            if (new TType().GetType() == typeof(int))
            {
                result = this._dbSet.AsNoTracking().Where(where).Sum(selector as Expression<Func<T, int>>);
            }
            if (new TType().GetType() == typeof(int?))
            {
                result = this._dbSet.AsNoTracking().Where(where).Sum(selector as Expression<Func<T, int?>>);
            }
            if (new TType().GetType() == typeof(long))
            {
                result = this._dbSet.AsNoTracking().Where(where).Sum(selector as Expression<Func<T, long>>);
            }
            if (new TType().GetType() == typeof(long?))
            {
                result = this._dbSet.AsNoTracking().Where(where).Sum(selector as Expression<Func<T, long?>>);
            }
            return (TType)result;
        }

        public void Dispose()
        {
            this._dbContext.Dispose();
        }


        # region 插入数据

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="isSaveChange"></param>
        /// <returns></returns>
        public bool Insert(T entity, bool isSaveChange = true)
        {
            _dbSet.Add(entity);
            if (isSaveChange)
            {
                return SaveChanges() > 0;
            }
            return false;
        }

        /// <summary>
        /// 异步插入
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="isSaveChange"></param>
        /// <returns></returns>
        public async Task<bool> InsertAsync(T entity, bool isSaveChange = true)
        {
            _dbSet.Add(entity);
            if (isSaveChange)
            {
                return await SaveChangesAsync() > 0;
            }
            return false;
        }


        public bool Insert(List<T> entitys, bool isSaveChange = true)
        {
            _dbSet.AddRange(entitys);
            if (isSaveChange)
            {
                return SaveChanges() > 0;
            }
            return false;
        }

        public async Task<bool> InsertAsync(List<T> entitys, bool isSaveChange = true)
        {
            _dbSet.AddRange(entitys);
            if (isSaveChange)
            {
                return await SaveChangesAsync() > 0;
            }
            return false;
        }
        #endregion

        #region 删除
        public bool Delete(List<T> entitys, bool isSaveChange = true)
        {
            entitys.ForEach(entity =>
            {
                _dbSet.Attach(entity);
                _dbSet.Remove(entity);
            });
            return isSaveChange && SaveChanges() > 0;
        }

        public virtual async Task<bool> DeleteAsync(T entity, bool isSaveChange = true)
        {
            _dbSet.Attach(entity);
            _dbSet.Remove(entity);
            return isSaveChange && await SaveChangesAsync() > 0;
        }
        public virtual async Task<bool> DeleteAsync(List<T> entitys, bool isSaveChange = true)
        {
            entitys.ForEach(entity =>
            {
                _dbSet.Attach(entity);
                _dbSet.Remove(entity);
            });
            return isSaveChange && await SaveChangesAsync() > 0;
        }
        #endregion

        #region 更新数据
        public bool Update(T entity, bool isSaveChange = true, List<string> updatePropertyList = null)
        {
            if (entity == null)
            {
                return false;
            }
            _dbSet.Attach(entity);
            var entry = _dbContext.Entry(entity);
            if (updatePropertyList == null)
            {
                entry.State = EntityState.Modified;//全字段更新
            }
            else
            {

                updatePropertyList.ForEach(c =>
                {
                    entry.Property(c).IsModified = true; //部分字段更新的写法
                });


            }
            if (isSaveChange)
            {
                return SaveChanges() > 0;
            }
            return false;
        }
        public bool Update(List<T> entitys, bool isSaveChange = true)
        {
            if (entitys == null || entitys.Count == 0)
            {
                return false;
            }
            entitys.ForEach(c =>
            {
                Update(c, false);
            });
            if (isSaveChange)
            {
                return SaveChanges() > 0;
            }
            return false;
        }
        public async Task<bool> UpdateAsync(T entity, bool isSaveChange, List<string> updatePropertyList)
        {
            if (entity == null)
            {
                return false;
            }
            _dbSet.Attach(entity);
            var entry = _dbContext.Entry<T>(entity);
            if (updatePropertyList == null)
            {
                entry.State = EntityState.Modified;//全字段更新
            }
            else
            {
                updatePropertyList.ForEach(c =>
                {
                    entry.Property(c).IsModified = true; //部分字段更新的写法
                });

            }
            if (isSaveChange)
            {
                return await SaveChangesAsync() > 0;
            }
            return false;
        }
        public async Task<bool> UpdateAsync(List<T> entitys, bool isSaveChange = true)
        {
            if (entitys == null || entitys.Count == 0)
            {
                return false;
            }
            entitys.ForEach(c =>
            {
                _dbSet.Attach(c);
                _dbContext.Entry(c).State = EntityState.Modified;
            });
            if (isSaveChange)
            {
                return await SaveChangesAsync() > 0;
            }
            return false;
        }


        #endregion

        #region SQL语句
#pragma warning disable CS0693 // 类型参数“T”与外部类型“Repositorys<T>”中的类型参数同名
        public virtual void BulkInsert<T>(List<T> entities)
#pragma warning restore CS0693 // 类型参数“T”与外部类型“Repositorys<T>”中的类型参数同名
        { }

       // [Obsolete]
        //public int ExecuteSql(string sql)
        //{
        //    return _dbContext.Database.ExecuteSqlCommand(sql);
        //}

        //[Obsolete]
        //public Task<int> ExecuteSqlAsync(string sql)
        //{
        //    return _dbContext.Database.ExecuteSqlCommandAsync(sql);
        //}

        //[Obsolete]
        //public int ExecuteSql(string sql, List<DbParameter> spList)
        //{
        //    return _dbContext.Database.ExecuteSqlCommand(sql, spList.ToArray());
        //}

        //[Obsolete]
        //public Task<int> ExecuteSqlAsync(string sql, List<DbParameter> spList)
        //{
        //    return _dbContext.Database.ExecuteSqlCommandAsync(sql, spList.ToArray());
        //}


        public virtual DataTable GetDataTableWithSql(string sql)
        {
            throw new NotImplementedException();
        }

        public virtual DataTable GetDataTableWithSql(string sql, List<DbParameter> spList)
        {
            throw new NotImplementedException();
        }

      

        IQueryable<T> IRepositorys<T>.GetAll()
        {
            return  _dbSet.AsNoTracking();
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

#pragma warning disable CS1998 // 此异步方法缺少 "await" 运算符，将以同步方式运行。请考虑使用 "await" 运算符等待非阻止的 API 调用，或者使用 "await Task.Run(...)" 在后台线程上执行占用大量 CPU 的工作。
        public async Task<IQueryable<T>> WhereAsync(Expression<Func<T, bool>> where)
#pragma warning restore CS1998 // 此异步方法缺少 "await" 运算符，将以同步方式运行。请考虑使用 "await" 运算符等待非阻止的 API 调用，或者使用 "await Task.Run(...)" 在后台线程上执行占用大量 CPU 的工作。
        {
            return _dbSet.Where(@where);
        }

        public async Task<bool> UpdateAsync1(T entity, bool isSaveChange, string updatePropert)
        {
            if (entity == null)
            {
                return false;
            }
            _dbSet.Attach(entity);
            var entry = _dbContext.Entry(entity);
            if (updatePropert == null)
            {
                entry.State = EntityState.Modified;//全字段更新
            }
            else
            {
                entry.Property(updatePropert).IsModified = true; //部分字段更新的写法
            }
            if (isSaveChange)
            {
                return await SaveChangesAsync() > 0;
            }
            return false;
        }

        public async Task<int> CountAsync()
        {
            return await _dbSet.AsNoTracking().CountAsync();
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> where)
        {
            return await _dbSet.AsNoTracking().CountAsync(@where);
        }
        #endregion



    }
}
