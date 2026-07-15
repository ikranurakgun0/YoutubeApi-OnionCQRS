using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using YoutubeApi.Application.Interfaces.Repositories;
using YoutubeApi.Domain.Common;

namespace YoutubeApi.Persistence.Repositories
{
    public class ReadRepository<T> : IReadRepository<T> where T : class, IEntityBase, new()
    {
        private DbContext dbContext;

        public ReadRepository(DbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        private DbSet<T> Table { get => dbContext.Set<T>(); }

        public async Task<IList<T>> GetAllAsync(System.Linq.Expressions.Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>, Microsoft.EntityFrameworkCore.Query.IIncludableQueryable<T, object>>? include = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, bool enableTracking = false)
        {
            IQueryable<T> queryable = Table; // Şuan T nesnemiz bir queryable olarak işlev görecektir. 

            if (!enableTracking) queryable = queryable.AsNoTracking(); // Takip etme işlemi yapmayacak. Sadece okuma işlemi yapacak.//Bizim sorguyu çağırıp, save yapana kadar tüm yaptığımız işlemleri takip eder.Biz performansta hız istediğimiz için Tracking kısmı önemli değil. Okuma kısmı önemli.
            if (include is not null) queryable = include(queryable); //include null değilse, include fonksiyonunu çağır ve queryable'ı ona gönder.//include null değilse, include fonksiyonunu çağır ve queryable'ı ona gönder.//Herhangi birisinde bir veri var mı diye hepsini arıyoruz,
            if (predicate is not null) queryable = queryable.Where(predicate); //predicate null değilse, predicate fonksiyonunu çağır ve queryable'ı ona gönder.//Herhangi birisinde bir veri var mı diye hepsini arıyoruz,
            if(orderBy is not null)//querybale zaten zorunlu olmadığı için yukarıdakilere bir option yazmış olduk ama sıralama yapacağımız için alttaki zorunlu zaten.Bir sıralamaya göre listeleme yapacağız ve return döndüreceğiz.
               return await orderBy(queryable).ToListAsync(); //orderBy null değilse, orderBy fonksiyonunu çağır ve queryable'ı ona gönder.
            return await queryable.ToListAsync(); //Sorguyu çalıştır ve listeye çevir.//Sorguyu çalıştır ve listeye çevir.
        
        }
        public async Task<IList<T>> GetAllByPagingAsync(Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, bool enableTracking = false, int currentPage = 1, int pageSize = 3)
        {
            IQueryable<T> queryable = Table;
            if (!enableTracking) queryable = queryable.AsNoTracking();
            if (include is not null) queryable = include(queryable);
            if (predicate is not null) queryable = queryable.Where(predicate);
            if (orderBy is not null)
                return await orderBy(queryable).Skip((currentPage - 1) * pageSize).Take(pageSize).ToListAsync();

            return await queryable.Skip((currentPage - 1) * pageSize).Take(pageSize).ToListAsync();

        }
        public async Task<T> GetAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null, bool enableTracking = false)
        {
            IQueryable<T> queryable = Table;
            if (!enableTracking) queryable = queryable.AsNoTracking();
            if (include is not null) queryable = include(queryable);
           // queryable.Where(predicate);

            return await queryable.FirstOrDefaultAsync(predicate); //1 tane veri alacağımız için bu şekilde yapıyoruz yukarıdaki <T> de bunu anlatıyor.

        }
        public async Task<int> CountAsync(Expression<Func<T, bool>>? predicate)
        {
            Table.AsNoTracking();
            if(predicate is not null) Table.Where(predicate);

            return await Table.CountAsync();

        }

        public  IQueryable<T> Find(Expression<Func<T, bool>> predicate, bool enableTracking = false)
        {
            if (!enableTracking) Table.AsNoTracking();
            return  Table.Where(predicate);
        }

       

       
    }
}
