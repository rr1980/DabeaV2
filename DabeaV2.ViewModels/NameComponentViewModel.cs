using DabeaV2.Common.Enums;

namespace DabeaV2.ViewModels
{
    public class RequestComponentViewModel
    {
        public long? Id { get; set; }
        public EntityType? EntityType { get; set; }
    }

    public class ComponentViewModel<T>
    {
        public long? Id { get; set; }
        public EntityType? EntityType { get; set; }
        public T Result { get; set; }
    }

    public abstract class BaseComponentViewModel
    {
        public long? Id { get; set; }
        public bool IsActive { get; set; }

    }

    public class NamePersonComponentViewModel : BaseComponentViewModel
    {
        public string Name { get; set; }
        public string VorName { get; set; }
        public string FullName { get; set; }
    }
    public class NameBenutzerComponentViewModel : NamePersonComponentViewModel
    {
        public string UserName { get; set; }
    }
}
