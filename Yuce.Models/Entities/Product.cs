using System;
using System.Collections.Generic;
using System.Text;

namespace Yuce.Models.Entities
{
    public class Product
    {
        // Entity annotions
        //[DataType(DataType.Text)]
        //[StringLength(100, ErrorMessage = "TestColumnName cannot be longer than 100 characters.")]
        //[Display(Name ="TestColumnName")]
        //[Required(ErrorMessage ="TestColumnName")]
        //[AllowHtml]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
