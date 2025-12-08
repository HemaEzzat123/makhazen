using MAKHAZIN.Core.Application.CQRS;

namespace MAKHAZIN.Core.Application.Features.Products.Commands
{
    public class DeleteProductCommand : ICommand<bool>
    {
        public int Id { get; set; }
        public DeleteProductCommand(int id) => Id = id;
    }
}
