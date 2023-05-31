﻿namespace Snblog.Enties.AutoMapper
    {
        /// <summary>
        /// SnippetTag对象映射关系
        /// </summary>
    public class SnippetTagMapper : Profile
    {
        /// <summary>
        /// 配置构造函数，用来创建关系映射
        /// </summary>
        public SnippetTagMapper()
        {
            //构建实体映射规则添加映射对象  
            //如两个实体字段一致可直接映射关系
            //SnUser原对象类型，SnUserDto 目标对象类型  ReverseMap，可相互转换
            CreateMap<SnippetTag, SnippetTagDto>().ReverseMap(); 

        }
    }
}
