

using Events.Domain.BaseEntities;
using Events.Domain.Enums;

namespace Events.Domain.Entities
{
    public class EventPlace : BaseEntity
    {
        public int Id { get; private set; }
        public string Name { get; private set; } = null!;
        public SeatingChart SeatingChart { get; private set; }
        public string? SeatingChartImagePath { get; private set; }
        public int? Columns { get; private set; }
        public int? Rows { get; private set; }
        public virtual List<Event> Events { get; set; }
        public ICollection<PlaceSeat> PlaceSeats { get; set; }
        public EventPlace()
        {

        }
        public EventPlace(string name,SeatingChart seatingChart,string? seatingChartImagePath,int? columns = 0,int? rows = 0)
        {
            Name = name;
            SeatingChart = seatingChart;
            SeatingChartImagePath = seatingChartImagePath;
            Columns = columns;
            Rows = rows;
        }
        public void UpdateEventPlace(string name, SeatingChart seatingChart, string? seatingChartImagePath, int? columns = 0, int? rows = 0)
        {
            Name = name;
            SeatingChart = seatingChart;
            SeatingChartImagePath = seatingChartImagePath;
            Columns = columns;
            Rows = rows;
        }
    }
}
