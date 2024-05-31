namespace CAMT.Domain.Base;

public abstract class EntityBase
{
	public EntityBase()
	{
		Id = Guid.NewGuid();
	}

    public Guid Id { get; set; }
}