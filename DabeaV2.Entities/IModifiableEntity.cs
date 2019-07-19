using System.Collections.Generic;

namespace DabeaV2.Entities
{
    public interface IModifiableEntity
    {
        ICollection<Modification> Modifications { get; set; }
    }
}
