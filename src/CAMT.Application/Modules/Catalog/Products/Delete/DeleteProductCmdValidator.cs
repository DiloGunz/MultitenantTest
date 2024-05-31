namespace CAMT.Application.Modules.Catalog.Products.Delete;

public class DeleteProductCmdValidator : AbstractValidator<DeleteProductCmd>
{
	public DeleteProductCmdValidator()
	{
        RuleFor(x => x.Id).NotEmpty();

    }
}