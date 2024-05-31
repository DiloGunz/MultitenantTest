namespace CAMT.Application.Modules.Catalog.Products.Create;

public class CreateProductCmdValidator : AbstractValidator<CreateProductCmd>
{
    public CreateProductCmdValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Price).GreaterThan(0);
    }
}