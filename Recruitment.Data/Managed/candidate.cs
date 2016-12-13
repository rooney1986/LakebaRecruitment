using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recruitment.Data
{
    public partial class candidate
    {
        public static IEnumerable<candidate> GetAll()
        {
            return DataRepositoryFactory.CurrentRepository.Find<candidate>
                (x => !x.is_deleted)
                .OrderByDescending(x => x.created_date);
        }

        public static candidate GetById(int id)
        {
            return DataRepositoryFactory.CurrentRepository.First<candidate>
                (x => !x.is_deleted && x.id == id);
        }

        public void Insert()
        {
            this.created_date = DateTime.Now;
            this.lastupdated_date = DateTime.Now;
            this.is_deleted = false;
            DataRepositoryFactory.CurrentRepository.Create(this);
        }

        public void Update()
        {
            this.lastupdated_date = DateTime.Now;
            DataRepositoryFactory.CurrentRepository.Modify(this);
        }

        public void Delete()
        {
            this.lastupdated_date = DateTime.Now;
            this.is_deleted = true;
            //DataRepositoryFactory.CurrentRepository.Delete(this);
            DataRepositoryFactory.CurrentRepository.Modify(this);
        }
    }
}
