﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Data
{
    public partial class CategorySubCategory
    {
        public int Id { get; set; }
        public int IdCategory { get; set; }
        public int IdSubcategory { get; set; }

        public virtual Category IdCategoryNavigation { get; set; }
        public virtual SubCategory IdSubcategoryNavigation { get; set; }
    }
}