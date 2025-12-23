using MAKHAZIN.Application.CQRS;

namespace MAKHAZIN.Application.Features.Products.Commands
{
    public class UpdateProductCommand : ICommand<bool>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string UnitOfMeasurement { get; set; }
        public string? ImageUrl { get; set; }
    }
}
