using MediatR;

namespace ForgeLabs.IA.Gateway.BuildingBlocks.Domain;

/// <summary>
/// Contrato para eventos de domínio. Estende <see cref="INotification"/> do MediatR
/// para suportar publicação e tratamento desacoplado.
/// </summary>
public interface IDomainEvent : INotification
{
    /// <summary>
    /// Identificador único do evento.
    /// </summary>
    Guid Id { get; }

    /// <summary>
    /// Data e hora (UTC) em que o evento ocorreu.
    /// </summary>
    DateTime OccurredOn { get; }
}
