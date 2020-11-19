using System.Collections.Generic;

namespace Motel.Services.Territories
{
    using Motel.Core;
    using  Motel.Domain.Domain.Territories;
    
    public interface ITerritoriesServices
    {
        IList<Territories> GetAllByName(string nameAdrress);
        IList<Territories> GetAll();
        IPagedList<Territories> GetAllFilter( bool? OrderIndex =false,int? StatusId = 0,
            string Name= "",  int PageIndex=0, int PageSize = int.MaxValue);
        IPagedList<Territories> GetAllParent(int? ParentId ,int? StatusId = null, 
            int? PageIndex = 0, int? PageSize = int.MaxValue,string Name ="");
        Territories GetById(int id);
        void Create(Territories territories);
        void Edit(Territories territories);
        void Delete(int id);
    }
}
