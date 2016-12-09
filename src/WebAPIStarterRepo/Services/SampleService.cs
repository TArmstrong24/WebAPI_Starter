using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using StarterAPI.Models;
using StarterAPI.Repository;

namespace StarterAPI.Services
{
    public interface ISampleService: IService<Sample>
    {
    }

    public class SampleService : BaseService<Sample>, ISampleService
    {
        public SampleService(IRepository<Sample> repository): base(repository)
        {
        }
       
    }
}
