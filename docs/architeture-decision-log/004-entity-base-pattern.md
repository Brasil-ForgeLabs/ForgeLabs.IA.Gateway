# ADR-004: Classe base Entity para entidades de domínio

## Status

Aceita

## Data

2025-07-16

## Contexto

No Domain-Driven Design (DDD), entidades são objetos com identidade própria e ciclo de vida contínuo. É necessário um ponto central para comportamentos transversais como registro de eventos de domínio e validação de regras de negócio, evitando duplicação em cada entidade concreta.

## Decisão

Adotar a classe abstrata `Entity` em `ForgeLabs.IA.Gateway.BuildingBlocks.Domain` como base obrigatória para todas as entidades de domínio. A classe fornece:

- **Registro de eventos de domínio** — lista interna de `IDomainEvent` com métodos `AddDomainEvent` e `ClearDomainEvents`.
- **Validação de regras de negócio** — método `CheckRule(IBusinessRule)` que lança `BusinessRuleValidationException` quando a regra é violada.

```csharp
public abstract class Entity
{
    private List<IDomainEvent> _domainEvents = new();

    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void ClearDomainEvents() { ... }
    protected void AddDomainEvent(IDomainEvent domainEvent) { ... }
    protected void CheckRule(IBusinessRule rule) { ... }
}
```

## Consequências

- **Positivas:**
  - Centraliza o gerenciamento de eventos de domínio em um único ponto.
  - Padroniza a validação de invariantes de negócio via `CheckRule`.
  - Entidades concretas focam apenas na lógica de negócio específica.
  - Facilita a integração com infraestrutura de despacho de eventos (MediatR).

- **Negativas:**
  - Herança única em C# — entidades não podem herdar de outra classe base.
  - Toda entidade carrega a lista de eventos, mesmo quando não os utiliza.
