using System;
using System.Collections.Generic;
using Contracts;
using Entities.ExtendedModels;

namespace Entities.Models
{
    public interface IOwnerRepository : IRepositoryBase<Owner>
    {
        IEnumerable<Owner> GetAllOwners();
        Owner GetOwnerById(Guid ownerId);
        OwnerExtended GetOwnerWithDetails(Guid ownerId);
        void CreateOwner(Owner o);
    }
}