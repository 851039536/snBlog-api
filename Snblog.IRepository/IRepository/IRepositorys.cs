﻿using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Snblog.IRepository.IRepository
{
    public interface IRepositorys<T> : IDisposable where T : class
    {
        # region 显式开启数据上下文事务
        /// <summary>
        /// 显式开启数据上下文事务
        /// </summary>
        /// <param name="isolationLevel">指定连接的事务锁定行为</param>
        void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.Unspecified);
        # endregion

        # region 提交事务的更改
        /// <summary>
        /// 提交事务的更改
        /// </summary>
        void Commit();
        # endregion

        # region 显式回滚事务，仅在显式开启事务后有用
        /// <summary>
        /// 显式回滚事务，仅在显式开启事务后有用
        /// </summary>
        void Rollback();
        #endregion

        #region 提交当前单元操作的更改
        /// <summary>
        /// 提交当前单元操作的更改
        /// </summary>
        int SaveChanges();
        #endregion

        #region 提交当前单元操作的更改
        /// <summary>
        /// 提交当前单元操作的更改
        /// </summary>
        /// <returns></returns>
        Task<int> SaveChangesAsync();
        #endregion

        #region 获取 当前实体类型的查询数据集，数据将使用不跟踪变化的方式来查询，当数据用于展现时，推荐使用此数据集，如果用于新增，更新，删除时，请使用<see cref="TrackEntities"/>数据集
        /// <summary>
        /// 获取 当前实体类型的查询数据集，数据将使用不跟踪变化的方式来查询，当数据用于展现时，推荐使用此数据集，如果用于新增，更新，删除时，请使用<see cref="TrackEntities"/>数据集
        /// </summary>
        IQueryable<T> Entities { get; }
        #endregion

        #region 获取 当前实体类型的查询数据集，当数据用于新增，更新，删除时，使用此数据集，如果数据用于展现，推荐使用<see cref="Entities"/>数据集
        /// <summary>
        /// 获取 当前实体类型的查询数据集，当数据用于新增，更新，删除时，使用此数据集，如果数据用于展现，推荐使用<see cref="Entities"/>数据集
        /// </summary>
        IQueryable<T> TrackEntities { get; }
        #endregion

        #region 插入 - 通过实体对象添加
        /// <summary>
        /// 插入 - 通过实体对象添加
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="isSave">是否执行</param>
        /// /// <returns></returns>
        T Add(T entity, bool isSave = true);
        Task<T> AddAsync(T entity, bool isSave = true);
        /// <summary>
        /// 批量插入 - 通过实体对象集合添加
        /// </summary>
        /// <param name="entitys">实体对象集合</param>
        /// <param name="isSave">是否执行</param>
        void AddRange(IEnumerable<T> entitys, bool isSave = true);
        bool Insert(T entity, bool isSaveChange);
        Task<bool> InsertAsync(T entity, bool isSaveChange);
        bool Insert(List<T> entitys, bool isSaveChange = true);
        Task<bool> InsertAsync(List<T> entitys, bool isSaveChange);

        #endregion

        #region 删除(删除之前需要查询)

        bool Delete(List<T> entitys, bool isSaveChange);
        Task<bool> DeleteAsync(T entity, bool isSaveChange);
        Task<bool> DeleteAsync(List<T> entitys, bool isSaveChange = true);
        /// <summary>
        /// 删除 - 通过实体对象删除
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="isSave">是否执行</param>
        void Delete(T entity, bool isSave = true);

        /// <summary>
        /// 批量删除 - 通过实体对象集合删除
        /// </summary>
        /// <param name="entitys">实体对象集合</param>
        /// <param name="isSave">是否执行</param>
        void Delete(bool isSave = false, params T[] entitys);

        /// <summary>
        /// 删除 - 通过主键ID删除
        /// </summary>
        /// <param name="id">主键ID</param>
        Task<int> DelAsync(object id);
        int Delete(object id);
        /// <summary>
        /// 批量删除 - 通过条件删除
        /// </summary>
        /// <param name="where">过滤条件</param>
        /// <param name="isSave">是否执行</param>
        void Delete(Expression<Func<T, bool>> @where, bool isSave = true);
        #endregion

        #region 修改数据
        bool Update(T entity, bool isSaveChange, List<string> updatePropertyList);
        Task<bool> UpdateAsync(T entity, bool isSaveChange, List<string> updatePropertyList);
        /// <summary>
        /// 更新单个字段
        /// </summary>
        /// <param name="entity">实体类</param>
        /// <param name="isSaveChange">是否更新</param>
        /// <param name="updatePropert">更新的字段</param>
        /// <returns></returns>
        Task<bool> UpdateAsync1(T entity, bool isSaveChange, string updatePropert);
        bool Update(List<T> entitys, bool isSaveChange);
        Task<bool> UpdateAsync(List<T> entitys, bool isSaveChange);

        /// <summary>
        /// 修改 - 通过实体对象修改
        /// </summary>
        /// <param name="entity">实体对象</param>
        Task<int> UpdateAsync(T entity);
        int Update(T entity);
        /// <summary>
        /// 批量修改 - 通过实体对象集合修改
        /// </summary>
        /// <param name="entitys">实体对象集合</param>
        void Update(params T[] entitys);
        #endregion

        #region 查找数据

        /// <summary>
        /// 是否满足条件
        /// </summary>
        /// <param name="where">过滤条件</param>
        /// <returns></returns>
        bool Any(Expression<Func<T, bool>> @where);

        /// <summary>
        /// 返回总条数
        /// </summary>
        /// <returns></returns>
        int Count();
        Task<int> CountAsync();
        /// <summary>
        /// 返回总条数 - 通过条件过滤
        /// </summary>
        /// <param name="where">过滤条件</param>
        /// <returns></returns>
        int Count(Expression<Func<T, bool>> @where);

        /// <summary>
        /// 返回总条数 - 通过条件过滤
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        Task<int> CountAsync(Expression<Func<T, bool>> @where);
        /// <summary>
        /// 返回第一条记录
        /// </summary>
        /// <param name="where">过滤条件</param>
        /// <returns></returns>
        T FirstOrDefault(Expression<Func<T, bool>> @where);
        /// <summary>
        /// 返回第一条记录 - 通过条件过滤
        /// </summary>
        /// <typeparam name="TOrder">排序约束</typeparam>
        /// <param name="where">过滤条件</param>
        /// <param name="order">排序条件</param>
        /// <param name="isDesc">排序方式</param>
        /// <returns></returns>
        T FirstOrDefault<TOrder>(Expression<Func<T, bool>> @where, Expression<Func<T, TOrder>> order, bool isDesc = false);

        /// <summary>
        /// 去重查询
        /// </summary>
        /// <param name="where">过滤条件</param>
        /// <returns></returns>
        IQueryable<T> Distinct(Expression<Func<T, bool>> @where);


        /// <summary>
        /// 条件查询
        /// </summary>
        /// <param name="where">过滤条件</param>
        /// <returns></returns>
        IQueryable<T> Where(Expression<Func<T, bool>> @where);

        /// <summary>
        /// 条件查询
        /// </summary>
        /// <param name="where">过滤条件</param>
        /// <returns></returns>
        Task<IQueryable<T>> WhereAsync(Expression<Func<T, bool>> @where);

        /// <summary>
        /// 条件查询 - 支持排序
        /// </summary>
        /// <typeparam name="TOrder">排序约束</typeparam>
        /// <param name="where">过滤条件</param>
        /// <param name="order">排序条件</param>
        /// <param name="isDesc">排序方式</param>
        /// <returns></returns>
        IQueryable<T> Where<TOrder>(Expression<Func<T, bool>> @where, Expression<Func<T, TOrder>> order, bool isDesc = false);


        /// <summary>
        /// 条件分页查询 - 支持排序
        /// </summary>
        /// <typeparam name="TOrder">排序约束</typeparam>
        /// <param name="where">过滤条件</param>
        /// <param name="order">排序条件</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页记录条数</param>
        /// <param name="count">返回总条数</param>
        /// <param name="isDesc">是否倒序</param>
        IEnumerable<T> WherePage<TOrder>(Func<T, bool> @where, Func<T, TOrder> order, int pageIndex, int pageSize, out int count, bool isDesc);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TOrder">排序约束</typeparam>
        /// <param name="where">过滤条件</param>
        /// <param name="order">排序条件</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页记录条数</param>
        /// <param name="isDesc">是否倒序</param>
        /// <returns></returns>

        Task<IEnumerable<T>> WherePageAsync<TOrder>(Func<T, bool> @where, Func<T, TOrder> order, int pageIndex, int pageSize, bool isDesc);

        /// <summary>
        /// 条件分页查询 - 支持排序
        /// </summary>
        /// <typeparam name="TOrder">排序约束</typeparam>
        /// <param name="where">过滤条件</param>
        /// <param name="order">排序条件</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页记录条数</param>
        /// <param name="count">返回总条数</param>
        /// <param name="isDesc">是否倒序</param>
        /// <returns></returns>
        IEnumerable<T> Where<TOrder>(Func<T, bool> @where, Func<T, TOrder> order, int pageIndex, int pageSize, out int count, bool isDesc = false);

        /// <summary>
        /// 条件分页查询 - 支持排序 - 支持Select导航属性查询
        /// </summary>
        /// <typeparam name="TOrder">排序约束</typeparam>
        /// <param name="where">过滤条件</param>
        /// <param name="order">排序条件</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页记录条数</param>
        /// <param name="count">返回总条数</param>
        /// <param name="isDesc">是否倒序</param>
        /// <returns></returns>
        IQueryable<T> Where<TOrder>(Expression<Func<T, bool>> @where, Expression<Func<T, TOrder>> order, int pageIndex, int pageSize, out int count, bool isDesc = false);

        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <returns></returns>
        IQueryable<T> GetAll();

        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        Task<List<T>> GetAllAsync();

        /// <summary>
        /// 获取所有数据 - 支持排序
        /// </summary>
        /// <typeparam name="TOrder">排序约束</typeparam>
        /// <param name="order">排序条件</param>
        /// <param name="isDesc">排序方式</param>
        /// <returns></returns>
        IQueryable<T> GetAll<TOrder>(Expression<Func<T, TOrder>> order, bool isDesc = false);

        /// <summary>
        /// 根据ID查询
        /// </summary>
        /// <typeparam name="TType">字段类型</typeparam>
        /// <param name="id">主键ID</param>
        /// <returns></returns>
        T GetById<TType>(TType id);

        /// <summary>
        /// 根据ID查询
        /// </summary>
        /// <typeparam name="TType">字段类型</typeparam>
        /// <param name="id">主键</param>
        /// <returns></returns>
        Task<T> GetByIdAsync<TType>(TType id);
        /// <summary>
        /// 获取最大值
        /// </summary>
        /// <typeparam name="TType">字段类型</typeparam>
        /// <param name="column">字段条件</param>
        /// <returns></returns>
        TType Max<TType>(Expression<Func<T, TType>> column);

        /// <summary>
        /// 获取最大值
        /// </summary>
        /// <typeparam name="TType">字段类型</typeparam>
        /// <param name="column">字段条件</param>
        /// <param name="where">过滤条件</param>
        /// <returns></returns>
        TType Max<TType>(Expression<Func<T, TType>> column, Expression<Func<T, bool>> @where);

        /// <summary>
        /// 获取最小值
        /// </summary>
        /// <typeparam name="TType">字段类型</typeparam>
        /// <param name="column">字段条件</param>
        /// <returns></returns>
        TType Min<TType>(Expression<Func<T, TType>> column);

        /// <summary>
        /// 获取最小值
        /// </summary>
        /// <typeparam name="TType">字段类型</typeparam>
        /// <param name="column">字段条件</param>
        /// <param name="where">过滤条件</param>
        /// <returns></returns>
        TType Min<TType>(Expression<Func<T, TType>> column, Expression<Func<T, bool>> @where);

        /// <summary>
        /// 获取总数
        /// </summary>
        /// <typeparam name="TType">字段类型</typeparam>
        /// <param name="selector">字段条件</param>
        /// <param name="where">过滤条件</param>
        /// <returns></returns>
        TType Sum<TType>(Expression<Func<T, TType>> selector, Expression<Func<T, bool>> @where) where TType : new();
        #endregion

        #region 执行Sql语句
#pragma warning disable CS0693 // 类型参数与外部类型中的类型参数同名
        void BulkInsert<T>(List<T> entities);
#pragma warning restore CS0693 // 类型参数与外部类型中的类型参数同名
        //int ExecuteSql(string sql);
        //Task<int> ExecuteSqlAsync(string sql);
        //int ExecuteSql(string sql, List<DbParameter> spList);
        //Task<int> ExecuteSqlAsync(string sql, List<DbParameter> spList);
        DataTable GetDataTableWithSql(string sql);
        DataTable GetDataTableWithSql(string sql, List<DbParameter> spList);
        #endregion
    }
}
