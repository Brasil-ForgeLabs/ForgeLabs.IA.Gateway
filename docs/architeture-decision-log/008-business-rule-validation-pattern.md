# ADR-008: Validação de regras de negócio com IBusinessRule e exceção dedicada

## Status

Aceita

## Data

2025-07-16

## Contexto

Invariantes de negócio precisam ser validadas dentro das entidades e Value Objects para garantir que o domínio nunca entre em estado inválido. Espalhar validações com `if/throw` genéricos dificulta a reutilização, o teste unitário e a identificação da regra violada.

## Decisão

Adotar o padrão de regras de negócio encapsuladas através de:

- **`IBusinessRule`** — contrato com `IsBroken()` (verifica violação) e `Message` (descrição do erro).
- **`BusinessRuleValidationException`** — exceção dedicada que captura a regra violada e expõe `BrokenRule` e `Details`.
- **`CheckRule(IBusinessRule)`** — método nas classes `Entity` e `ValueObject` que centraliza a invocação.

```csharp
public interface IBusinessRule
{
    bool IsBroken();
    string Message { get; }
}

public class BusinessRuleValidationException : Exception
{
    public IBusinessRule BrokenRule { get; }
    public string Details { get; }
}
```

Cada regra é implementada como uma classe própria, encapsulando a lógica de validação e sua mensagem:

```csharp
public class OrderMustHaveAtLeastOneItem : IBusinessRule
{
    private readonly List<OrderItem> _items;

    public OrderMustHaveAtLeastOneItem(List<OrderItem> items) => _items = items;

    public bool IsBroken() => !_items.Any();
    public string Message => "O pedido deve conter pelo menos um item.";
}
```

## Consequências

- **Positivas:**
  - Cada regra é uma classe isolada — fácil de testar, nomear e reutilizar.
  - `BusinessRuleValidationException` permite que camadas superiores identifiquem exatamente qual regra falhou.
  - O nome da classe da regra serve como documentação viva da invariante de negócio.
  - Padrão uniforme em `Entity` e `ValueObject` via `CheckRule`.

- **Negativas:**
  - Proliferação de classes — cada regra gera um arquivo/classe adicional.
  - Validações muito simples (ex.: null check) podem parecer over-engineering.
  - A exceção interrompe o fluxo na primeira regra violada — não agrega múltiplas violações em uma única resposta.
