

using Events.Domain.BaseEntities;
using Events.Domain.Enums;

namespace Events.Domain.Entities
{
    public class PersonType : BaseEntity
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Color { get; private set; }
        public PersonType() { }
        public PersonType(string name,string color)
        {
             Name = name;
            Color = color;
        }
        public void UpdatePersonType(string name,string color)
        {
            Name = name;
            Color = color;
        }
    }
}
