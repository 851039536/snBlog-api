using Microsoft.EntityFrameworkCore;
using Snblog.IService.IService;
using Snblog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snblog.Service.Service
{
  public  class SnInterfaceService : ISnInterfaceService
    {
        private readonly snblogContext _service;

        public SnInterfaceService(snblogContext service)
        {
            _service = service;
        }

        public async Task<List<SnInterface>> GetTypeAsync(int userId , int type)
        {
          return    await  _service.SnInterface.Where(s => s.TypeId == type && s.UserId == userId).ToListAsync();
        }
    }
}
