using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeApi.Domain.Common;

namespace YoutubeApi.Application.Interfaces.Repositories
{
    public interface IWriteRepository<T> where T: class, IEntityBase, new()
    {
        Task AddAsync(T Entity);//Veritabanına sadece 1 adet kayıt ekler.
                                     //save işlemimizin sonucunda oldu mu olmadı mı diye 1-0 gibi değerler dönmesi için int ekledik. İleri ki zamanlarda gerekmeyebilir.

        //Veritabanına bir liste dolusu (belki 10, belki 1000 adet) kaydı tek bir hamlede ekler.
        Task AddRangeAsync(IList<T> entities);
        Task<T> UpdateAsync(T entity);
       
        Task HardDeleteAsync(T entity);
        
    }
}
