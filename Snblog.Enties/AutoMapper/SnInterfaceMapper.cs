using AutoMapper;
using Snblog.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Snblog.Enties.AutoMapper
{
    public class SnInterfaceMapper : Profile
    {
        /// <summary>
        /// 配置构造函数，用来创建关系映射
        /// </summary>
        public SnInterfaceMapper()
        {
            //构建实体映射规则添加映射对象  
            //如两个实体字段一致可直接映射关系
            //SnUser原对象类型，SnUserDto 目标对象类型  ReverseMap，可相互转换
            CreateMap<SnInterface, SnInterfaceDto>().ReverseMap(); 

        }
    }
}
