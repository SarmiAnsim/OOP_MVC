using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntityDataBases.Managers.CategorysParts
{
    public class CreateOrUpdateCategoryRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
