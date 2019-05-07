using System.Collections.Generic;
using Contracts;

namespace Entities.Models
{
    public interface IOwnerRepository : IRepositoryBase<Owner>
    {
        IEnumerable<Owner> GetAllOwners();
    }
}