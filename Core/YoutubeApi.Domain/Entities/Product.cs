using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeApi.Domain.Common;

namespace YoutubeApi.Domain.Entities
{
    public class Product : EntityBase
    {
        public required string Title { get; set; }

        public required string Description { get; set; }

        public required int BrandId { get; set; }
        public required decimal Price { get; set; }
        public required decimal Discount { get; set; }
        public Brand Brand { get; set; }//yine BrandId dediğimiz için Brand'ı tekil olarak veriyoruz.
        public ICollection<Category> Categories { get; set; } //bir ürün birden fazla kategoriye ait olabilir. Bu yüzden ICollection olarak veriyoruz.
        //public required string ImagePath { get; set; }
    }
}
