using MAKHAZIN.Core.Application.CQRS;

namespace MAKHAZIN.Core.Application.Features.Products.Commands
{
    public class CreateProductCommand : ICommand<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string UnitOfMeasurement { get; set; }
    }
}
