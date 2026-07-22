namespace ForgeLabs.IA.Gateway.BuildingBlocks.Domain;

/// <summary>
/// Implementação base para eventos de domínio.
/// Gera automaticamente um identificador único e registra o momento (UTC) da ocorrência.
/// </summary>
public class DomainEventBase : IDomainEvent
{
    /// <inheritdoc />
    public Guid Id { get; }

    /// <inheritdoc />
    public DateTime OccurredOn { get; }

    /// <summary>
    /// Inicializa o evento com um novo <see cref="Guid"/> e a data/hora UTC atual.
    /// </summary>
    public DomainEventBase()
    {
        this.Id = Guid.NewGuid();
        this.OccurredOn = DateTime.UtcNow;
    }
}
