using MAKHAZIN.Core.Entities;

namespace MAKHAZIN.Core.Sepecification
{
    /// <summary>
    /// Specification for getting all auctions with navigation properties (Product, User, Bids)
    /// Supports filtering by active status, search by product name, and pagination
    /// </summary>
    public class AuctionsWithNavigationSpecification : BaseSepecification<Auction>
    {
        public AuctionsWithNavigationSpecification(string? search, bool? activeOnly, int pageIndex, int pageSize)
        {
            // Include navigation properties
            Includes.Add(a => a.Product);
            Includes.Add(a => a.User);
            Includes.Add(a => a.Bids);
            AddInclude("Bids.User"); // Include User for each Bid

            // Apply criteria for filtering
            if (activeOnly.HasValue && activeOnly.Value)
            {
                Criteria = a => a.ExpirationTime > DateTime.UtcNow;
            }

            if (!string.IsNullOrEmpty(search))
            {
                var searchLower = search.ToLower();
                if (Criteria != null)
                {
                    var existingCriteria = Criteria;
                    Criteria = a => existingCriteria.Compile()(a) && a.Product.Name.ToLower().Contains(searchLower);
                }
                else
                {
                    Criteria = a => a.Product.Name.ToLower().Contains(searchLower);
                }
            }

            // Order by Id descending (newest first)
            AddOrderByDesc(a => a.Id);

            // Apply pagination
            ApplyPagination((pageIndex - 1) * pageSize, pageSize);
        }
    }

    /// <summary>
    /// Specification for getting auctions by user ID with navigation properties
    /// </summary>
    public class AuctionsByUserSpecification : BaseSepecification<Auction>
    {
        public AuctionsByUserSpecification(int userId, int pageIndex, int pageSize)
            : base(a => a.UserId == userId)
        {
            // Include navigation properties
            Includes.Add(a => a.Product);
            Includes.Add(a => a.User);
            Includes.Add(a => a.Bids);
            AddInclude("Bids.User"); // Include User for each Bid

            // Order by Id descending (newest first)
            AddOrderByDesc(a => a.Id);

            // Apply pagination
            ApplyPagination((pageIndex - 1) * pageSize, pageSize);
        }
    }

    /// <summary>
    /// Specification for counting auctions (without pagination)
    /// </summary>
    public class AuctionsCountSpecification : BaseSepecification<Auction>
    {
        public AuctionsCountSpecification(string? search, bool? activeOnly)
        {
            if (activeOnly.HasValue && activeOnly.Value)
            {
                Criteria = a => a.ExpirationTime > DateTime.UtcNow;
            }

            if (!string.IsNullOrEmpty(search))
            {
                var searchLower = search.ToLower();
                if (Criteria != null)
                {
                    var existingCriteria = Criteria;
                    Criteria = a => existingCriteria.Compile()(a) && a.Product.Name.ToLower().Contains(searchLower);
                }
                else
                {
                    Criteria = a => a.Product.Name.ToLower().Contains(searchLower);
                }
            }
        }
    }

    /// <summary>
    /// Specification for counting user's auctions
    /// </summary>
    public class AuctionsByUserCountSpecification : BaseSepecification<Auction>
    {
        public AuctionsByUserCountSpecification(int userId)
            : base(a => a.UserId == userId)
        {
        }
    }
}
