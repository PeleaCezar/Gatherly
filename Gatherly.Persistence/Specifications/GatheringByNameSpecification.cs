using Gatherly.Domain.Entities;

namespace Gatherly.Persistence.Specifications
{
    internal class GatheringByNameSpecification : Specification<Gathering>
    {
        public GatheringByNameSpecification(string name)
            : base(gathering => string.IsNullOrEmpty(name) ||
                                gathering.Name.Contains(name))
        {
            AddInclude(gathering => gathering.Creator);
            AddInclude(gathering => gathering.Attendees);

            AddOrderBy(gathering => gathering.Name);
        }
    }
}
