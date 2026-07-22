# ADR-006: Interface marcadora IAggregateRoot para raízes de agregado

## Status

Aceita

## Data

2025-07-16

## Contexto

No DDD, o Aggregate Root é a entidade principal de um agregado — o único ponto de entrada para modificações e a unidade de consistência transacional. É necessário distinguir, em tempo de compilação e por convenção, quais entidades são raízes de agregado para que repositórios, regras de persistência e policies atuem apenas sobre elas.

## Decisão

Adotar a interface marcadora `IAggregateRoot` em `ForgeLabs.IA.Gateway.BuildingBlocks.Domain`. Entidades que representam raízes de agregado devem implementar esta interface.

```csharp
public interface IAggregateRoot { }
```

A interface é intencionalmente vazia (marker interface), servindo como:

- **Restrição genérica** — repositórios e serviços de infraestrutura podem exigir `where T : IAggregateRoot`.
- **Convenção explícita** — torna visível no código quais entidades são pontos de entrada de agregados.

## Consequências

- **Positivas:**
  - Impede que repositórios sejam criados para entidades internas de um agregado.
  - Facilita a aplicação de regras arquiteturais via analyzers ou testes de arquitetura.
  - Comunicação clara da intenção de design no modelo de domínio.

- **Negativas:**
  - Por ser uma interface vazia, não impõe nenhum comportamento — a disciplina depende da equipe.
  - Não há validação em tempo de compilação que impeça acesso direto a entidades internas.
