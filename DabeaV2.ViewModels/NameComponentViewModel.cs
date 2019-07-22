using DabeaV2.Common.Enums;

namespace DabeaV2.ViewModels
{
    public class ComponentViewModel<T>
    {
        public long? Id { get; set; }
        public EntityType? EntityType { get; set; }
        public T Result { get; set; }
    }

    public class BaseComponentViewModel
    {
        public long? Id { get; set; }
        public bool IsActive { get; set; }

    }

    public class NameComponentViewModel : BaseComponentViewModel
    {
        public string Name { get; set; }
        public string VorName { get; set; }
        public string FullName { get; set; }
    }
}
