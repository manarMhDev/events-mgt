

using Events.Domain.BaseEntities;
using Events.Domain.Enums;
using System.Drawing;

namespace Events.Domain.Entities
{
    public class SeatsType : BaseEntity
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Color { get; private set; }
        public string ColorText { get; private set; }
        public string? ImagePath { get; private set; }
        public ICollection<PlaceSeat> PlaceSeats { get; set; }
        public SeatsType() { }
        public SeatsType(string name,string color,string colorText,string imagePath) 
        {
            Name = name;
            Color = color;
            ColorText = colorText;
            ImagePath = imagePath;
        }
        public void UpdateSeatsType(string name,string color,string colorText,string imagePath)
        {
            Name = name;
            Color = color;
            ColorText = colorText;
            ImagePath = imagePath;
        }
    }
}
