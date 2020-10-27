using Microsoft.EntityFrameworkCore;
using Snblog.IRepository;
using Snblog.IService;
using Snblog.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Snblog.Service
{
    public class SnVideoTypeService : BaseService, ISnVideoTypeService
    {
        public SnVideoTypeService(IRepositoryFactory repositoryFactory, IconcardContext mydbcontext) : base(repositoryFactory, mydbcontext)
        {
        }

        public async Task<List<SnVideoType>> AsyGetTest()
        {
            var data = CreateService<SnVideoType>();
            return await data.GetAll().ToListAsync();
        }
    }
}
