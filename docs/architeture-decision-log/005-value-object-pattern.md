# ADR-005: Classe base ValueObject com igualdade estrutural

## Status

Aceita

## Data

2025-07-16

## Contexto

No DDD, Value Objects são objetos imutáveis definidos exclusivamente por seus atributos, sem identidade própria. Dois Value Objects com os mesmos valores são considerados iguais. Implementar corretamente `Equals`, `GetHashCode` e operadores de igualdade em cada Value Object é repetitivo e propenso a erros.

## Decisão

Adotar a classe abstrata `ValueObject` em `ForgeLabs.IA.Gateway.BuildingBlocks.Domain` como base para todos os Value Objects. A classe fornece:

- **Igualdade estrutural automática** — compara todas as propriedades públicas e campos (públicos e privados) via reflection.
- **Exclusão de membros** — propriedades e campos marcados com `IgnoreMemberAttribute` são excluídos da comparação.
- **Operadores `==` e `!=`** — sobrescritos para igualdade por valor.
- **Validação de regras** — método estático `CheckRule(IBusinessRule)` disponível para invariantes internas.

```csharp
public abstract class ValueObject : IEquatable<ValueObject>
{
    public override bool Equals(object? obj) { ... }
    public override int GetHashCode() { ... }
    protected static void CheckRule(IBusinessRule rule) { ... }
}
```

## Consequências

- **Positivas:**
  - Elimina código boilerplate de igualdade em cada Value Object.
  - Garante consistência na comparação — novos atributos são incluídos automaticamente.
  - `IgnoreMemberAttribute` oferece flexibilidade para excluir campos calculados ou de cache.

- **Negativas:**
  - Uso de reflection impacta performance em cenários de comparação massiva (hot paths).
  - Campos privados são incluídos na comparação — pode gerar resultados inesperados se não houver consciência desse comportamento.
  - Herança única em C# — Value Objects não podem herdar de outra classe base.
