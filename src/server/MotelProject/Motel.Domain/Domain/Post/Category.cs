using Motel.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Motel.Domain.Domain.Post
{
    public partial class Category : BaseEntity
    {
		public string Name {get;set;}
        public string MetaKeywords {get;set;}
        public string MetaTitle {get;set;}
        public string PriceRanges {get;set;}
        public string PageSizeOptions {get;set;}
        public string Description {get;set;}
        public int CategoryTemplateId {get;set;}
        public string MetaDescription {get;set;}
        public int ParentCategoryId {get;set;}
        public int PictureId {get;set;}
        public int PageSize {get;set;}
        public bool AllowCustomersToSelectPageSize {get;set;}
        public bool ShowOnHomepage {get;set;}
        public bool IncludeInTopMenu {get;set;}
        public bool Published {get;set;}
        public bool Deleted {get;set;}
        public int DisplayOrder {get;set;}
        public DateTime CreatedOnUtc {get;set;}
        public DateTime UpdatedOnUtc {get;set;}
    }
}
