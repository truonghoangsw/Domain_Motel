using System.Collections.Generic;

namespace Motel.Services.Territories
{
    using Motel.Core;
    using  Motel.Domain.Domain.Territories;
    
    public interface ITerritoriesServices
    {
        IList<Territories> GetAll();
        IPagedList<Territories> GetAllFilter( bool? OrderIndex =false,int? StatusId = 0,
            string Name= "",  int PageIndex=0, int PageSize = int.MaxValue);
        IPagedList<Territories> GetAllParent(string Name, int? StatusId, int? ParentId, 
            bool? OrderIndex, bool? LevelObject, int? PageIndex = 0, int? PageSize = int.MaxValue);
        Territories GetById(int id);
        void Create(Territories territories);
        void Edit(Territories territories);
        void Delete(int id);
    }
}
