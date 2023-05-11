using Snblog.Enties.Models;
using System;
using System.Collections.Generic;

namespace Snblog.Util.components
{

    /// <summary>
    /// 自动属性自动属性
    /// 使用默认值避免在使用它们之前进行 null 检查 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EntityData<T>
    {
        public T Entity { get; set; } = default!;
        public List<T> EntityList { get; set; } = new List<T>();
        public int EntityCount { get; set; }


    }
    /// <summary>
    /// 自动属性自动属性
    /// 使用默认值避免在使用它们之前进行 null 检查 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EntityDataDto<T> 
    {
        public T Entity { get; set; } = default!;
        public List<T> EntityList { get; set; } = new List<T>();
        public string[] Name { get; set; } 
    }


}
