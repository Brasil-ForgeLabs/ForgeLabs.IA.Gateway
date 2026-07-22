# ADR-009: Strongly Typed Ids com TypedIdValueBase

## Status

Aceita

## Data

2025-07-16

## Contexto

Utilizar `Guid` diretamente como identificador de entidades permite erros sutis em tempo de compilação, como passar o `Id` de um `Customer` onde se espera o `Id` de uma `Order`. Esses erros só são detectados em tempo de execução (ou nunca), gerando bugs difíceis de rastrear.

## Decisão

Adotar a classe abstrata `TypedIdValueBase` em `ForgeLabs.IA.Gateway.BuildingBlocks.Domain` como base para identificadores fortemente tipados. Cada entidade define seu próprio tipo de Id:

```csharp
public abstract class TypedIdValueBase : IEquatable<TypedIdValueBase>
{
    public Guid Value { get; }

    protected TypedIdValueBase(Guid value)
    {
        if (value == Guid.Empty)
            throw new InvalidOperationException("Id value cannot be empty!");
        Value = value;
    }
}
```

Exemplo de uso:

```csharp
public class OrderId : TypedIdValueBase
{
    public OrderId(Guid value) : base(value) { }
}

public class CustomerId : TypedIdValueBase
{
    public CustomerId(Guid value) : base(value) { }
}
```

Com isso, `OrderId` e `CustomerId` são tipos distintos — atribuir um ao outro gera erro de compilação.

## Consequências

- **Positivas:**
  - Segurança de tipo em tempo de compilação — impossível trocar IDs de entidades diferentes.
  - Validação embutida — `Guid.Empty` é rejeitado no construtor.
  - Igualdade por valor implementada (`IEquatable`, operadores `==`/`!=`, `GetHashCode`).
  - Melhora a legibilidade de assinaturas de métodos e construtores.

- **Negativas:**
  - Requer configuração adicional no ORM (ex.: EF Core Value Converters) para persistência.
  - Serialização JSON pode exigir custom converters.
  - Uma classe extra por entidade para o tipo do Id.
  - Comparação de igualdade entre tipos diferentes (ex.: `OrderId == CustomerId`) compila e retorna `true` se o `Guid` for igual, pois a base compara apenas `Value`.
