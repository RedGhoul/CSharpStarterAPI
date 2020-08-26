namespace Application.Queries.Events
{
    public class EventSortFilterQuery
    {
        public decimal GreaterThanCost { get; set; } = -1;
        public decimal LessThanCost { get; set; } = -1;
        public string Name { get; set; } = null;
    }
}
