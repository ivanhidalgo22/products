using System.Collections.Generic;

namespace Sample.Marketplace.Products.Domain.Entities.Features.Product
{
    public partial class Term
    {
        public Term()
        {
            Availabilities = new HashSet<Availability>();
        }

        public string Duration { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Availability> Availabilities { get; set; }
    }
}
