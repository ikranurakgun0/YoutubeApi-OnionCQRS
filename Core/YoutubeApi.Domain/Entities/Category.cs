using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeApi.Domain.Common;

namespace YoutubeApi.Domain.Entities
{
    public class Category : EntityBase
    {
        public Category()
        {
            
        }

        public Category(int parentId, string name, int priority )
        {
            ParentId = parentId;
            Name = name;
            Priority = priority;
        }
        public  int ParentId { get; set; }

        public  string Name { get; set; }

        public  int Priority { get; set; }

        public ICollection<Detail> Details { get; set; }
        public ICollection<Product> Products { get; set; }//Product ile Category arasında çoktan çoğa bir ilişki vardır. Bu yüzden ICollection olarak veriyoruz. Ve iki tarafada bu 1-N lik ilişkiyi kurmak için ICollection olarak veriyoruz. Yani bir kategoriye birden fazla ürün eklenebilir ve bir ürün birden fazla kategoriye ait olabilir. Bu yüzden ICollection olarak karşılıklı veriyoruz. 1-N ler çoka çok oluyor yanlış anlamadıysam.

    }
}
