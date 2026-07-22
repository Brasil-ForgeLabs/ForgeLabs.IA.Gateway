namespace ForgeLabs.IA.Gateway.BuildingBlocks.Domain;

/// <summary>
/// Classe base para entidades de domínio.
/// Gerencia eventos de domínio e oferece validação de regras de negócio.
/// </summary>
public abstract class Entity
{
    private List<IDomainEvent> _domainEvents = new();

    /// <summary>
    /// Coleção somente leitura dos eventos de domínio registrados na entidade.
    /// </summary>
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    /// <summary>
    /// Remove todos os eventos de domínio da entidade.
    /// </summary>
    public void ClearDomainEvents()
    {
        _domainEvents?.Clear();
    }

    /// <summary>
    /// Registra um evento de domínio na entidade.
    /// </summary>
    /// <param name="domainEvent">Evento de domínio a ser adicionado.</param>
    protected void AddDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents ??= [];

        this._domainEvents.Add(domainEvent);
    }

    /// <summary>
    /// Valida uma regra de negócio e lança <see cref="BusinessRuleValidationException"/> caso seja violada.
    /// </summary>
    /// <param name="rule">Regra de negócio a ser validada.</param>
    /// <exception cref="BusinessRuleValidationException">Lançada quando a regra é violada.</exception>
    protected void CheckRule(IBusinessRule rule)
    {
        if (rule.IsBroken())
        {
            throw new BusinessRuleValidationException(rule);
        }
    }
}
