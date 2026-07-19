using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeApi.Application.Interfaces.Repositories;
using YoutubeApi.Application.Interfaces.UnitOfWorks;
using YoutubeApi.Persistence.Context;
using YoutubeApi.Persistence.Repositories;

namespace YoutubeApi.Persistence.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext dbContext;

        public UnitOfWork(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async ValueTask DisposeAsync() => await dbContext.DisposeAsync(); //tek 1 return ettireceğimiz değer varsa bu şekilde kullanabiliriz.


        public int Save() => dbContext.SaveChanges(); //SaveChanges() metodu geriye int döndürür. Bu int değeri kaç adet satırın etkilendiğini gösterir.


        public async Task<int> SaveAsync() => await dbContext.SaveChangesAsync(); //Asenkron halini çağırdık.

        IReadRepository<T> IUnitOfWork.GetReadRepository<T>() => new ReadRepository<T>(dbContext);// Hangi repositorye aitse bu şekilde tanımlarız. DBcontext türünden olacağını bu şekilde belirtmiş oluyoruz. Interface hangi türü kullanıyorsa onu veriyoruz daha doğrusu.
        

        IWriteRepository<T> IUnitOfWork.GetWriteRepository<T>() => new WriteRepository<T>(dbContext);

    }
}
