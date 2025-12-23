using MAKHAZIN.Application.CQRS;

namespace MAKHAZIN.Application.Features.Products.Commands
{
    public class DeleteProductCommand : ICommand<bool>
    {
        public int Id { get; set; }
        public DeleteProductCommand(int id) => Id = id;
    }
}
