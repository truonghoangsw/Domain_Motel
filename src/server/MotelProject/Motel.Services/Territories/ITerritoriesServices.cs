using System.Collections.Generic;

namespace Motel.Services.Territories
{
    using Motel.Core;
    using  Motel.Domain.Domain.Territories;
    
    public interface ITerritoriesServices
    {
        IList<Territories> GetAll();
        IPagedList<Territories> GetAllFilter(string Name,int StatusId,string PackageName,int Ten,bool OrderIndex
            ,int? PageIndex,int? PageSize 
            , out int totalItem);
        IPagedList<Territories> GetAllParent(string Name,int StatusId,string ParentId,bool OrderIndex,bool LevelObject
            ,int? PageIndex,int? PageSize , out int totalItem);
        Territories GetById(int id);
        void Create(Territories territories);
        void Edit(Territories territories);
        void Delete(int id);
    }
}
