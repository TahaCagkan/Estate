using Estate.DataAccessLayer.Abstract;
using Estate.DataAccessLayer.Data;
using Estate.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estate.DataAccessLayer.Concrete
{
    public class EfAdvertRepository : EfGenericRepository<Advert, DataContext>, IAdvertRepository
    {
        private DataContext context;
        public EfAdvertRepository(DataContext context) : base(context)
        {
            this.context= context;
        }
    }
}
