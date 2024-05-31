namespace CAMT.Application.Modules.Catalog.Products.Update;

public class UpdateProductCmdValidator : AbstractValidator<UpdateProductCmd>
{
	public UpdateProductCmdValidator()
	{
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Price).GreaterThan(0);
    }
}