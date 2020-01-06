using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Internal.Models.Domain;

namespace Internal.Repository
{
    public interface IRepositoryBase<T>
    {
        List<Movie> GetAll();

        Movie Create(Movie obj);

        Movie Update(Movie obj);

        Movie Delete(Movie obj); //This would normally be by id but ids are not used so full obj would probs be safest if it were done.
    }
}
