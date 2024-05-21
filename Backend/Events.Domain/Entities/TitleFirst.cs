

using Events.Domain.BaseEntities;
using Events.Domain.Enums;

namespace Events.Domain.Entities
{
    public class TitleFirst : BaseEntity
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public TitleFirst() { }
        public TitleFirst(string name)
        {
            Name = name;
        }
        public void UpdateTitleFirst(string name)
        {
            Name = name;
        }
    }
}
