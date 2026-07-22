# ADR-007: Domain Events com IDomainEvent e DomainEventBase

## Status

Aceita

## Data

2025-07-16

## Contexto

Entidades de domínio frequentemente precisam comunicar que algo significativo aconteceu (ex.: pedido criado, status alterado) sem acoplar-se diretamente a outros módulos ou camadas. O padrão Domain Events permite publicar notificações que handlers específicos tratam de forma desacoplada.

## Decisão

Adotar o contrato `IDomainEvent` e a implementação base `DomainEventBase` em `ForgeLabs.IA.Gateway.BuildingBlocks.Domain`:

- **`IDomainEvent`** — estende `INotification` do MediatR, exigindo `Id` (Guid) e `OccurredOn` (DateTime UTC).
- **`DomainEventBase`** — implementação concreta que gera automaticamente o `Id` e registra o timestamp UTC na construção.

```csharp
public interface IDomainEvent : INotification
{
    Guid Id { get; }
    DateTime OccurredOn { get; }
}

public class DomainEventBase : IDomainEvent
{
    public Guid Id { get; }
    public DateTime OccurredOn { get; }

    public DomainEventBase()
    {
        Id = Guid.NewGuid();
        OccurredOn = DateTime.UtcNow;
    }
}
```

Os eventos são registrados na `Entity` base via `AddDomainEvent` e despachados pela infraestrutura (tipicamente ao persistir o agregado).

## Consequências

- **Positivas:**
  - Desacoplamento total entre quem produz e quem consome o evento.
  - Integração nativa com MediatR via `INotification` — handlers são registrados por DI.
  - Rastreabilidade — cada evento possui `Id` e `OccurredOn` para auditoria.
  - Extensível para integração com event sourcing ou message brokers no futuro.

- **Negativas:**
  - Dependência direta do pacote MediatR no projeto de domínio.
  - Eventos são despachados in-process — para comunicação entre serviços, é necessário infraestrutura adicional (outbox, message bus).
  - Ordem de despacho dos handlers não é garantida pelo MediatR.
