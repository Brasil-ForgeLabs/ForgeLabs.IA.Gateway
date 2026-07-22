# ADR-003: Comentários XML obrigatórios em membros públicos do Domain

## Status

Aceita

## Data

2025-07-16

## Contexto

Com `GenerateDocumentationFile` (ADR-001) e `TreatWarningsAsErrors` (ADR-002) habilitados globalmente, o projeto `ForgeLabs.IA.Gateway.BuildingBlocks.Domain` apresentava 23 erros CS1591 no build. Todos os erros eram causados por membros públicos sem comentários XML `/// <summary>`.

### Arquivos afetados

| Arquivo | Membros sem documentação |
|---|---|
| `BusinessRuleValidationException.cs` | classe, `BrokenRule`, `Details`, construtor, `ToString()` |
| `DomainEventBase.cs` | classe, `Id`, `OccurredOn`, construtor |
| `IBusinessRule.cs` | interface, `IsBroken()`, `Message` |
| `IDomainEvent.cs` | interface, `Id`, `OccurredOn` |
| `IgnoreMemberAttribute.cs` | classe |
| `ValueObject.cs` | classe, `operator ==`, `operator !=`, `Equals(ValueObject?)`, `Equals(object?)`, `GetHashCode()`, `CheckRule()` |

## Alternativas consideradas

1. **Adicionar comentários XML em todos os membros públicos** — Resolve a causa raiz, mantém a documentação completa.
2. **Suprimir CS1591 no `.csproj` do Domain** (`<NoWarn>CS1591</NoWarn>`) — Rápido, mas abre mão de documentação neste assembly.
3. **Relaxar CS1591 globalmente** (`<WarningsNotAsErrors>CS1591</WarningsNotAsErrors>`) — CS1591 voltaria a ser warning em todos os projetos.

## Decisão

Adotar a alternativa **1**: adicionar comentários XML (`/// <summary>`, `/// <param>`, `/// <inheritdoc />`) a todos os 23 membros públicos que estavam sem documentação.

### Convenções adotadas

- Usar `/// <inheritdoc />` quando o membro implementa ou sobrescreve um contrato já documentado (ex: `Equals`, `GetHashCode`, propriedades de interface).
- Usar `/// <summary>` com descrição própria para membros originais (classes, construtores, operadores, métodos estáticos).
- Usar `/// <param>` e `/// <exception>` quando aplicável para construtores e métodos com parâmetros.

## Consequências

- **Positivas:**
  - Build compila com 0 erros e 0 warnings.
  - Toda API pública do BuildingBlocks.Domain está documentada.
  - IntelliSense exibe descrições úteis ao consumir essas classes em outros projetos.
  - Estabelece o padrão para novos membros públicos adicionados ao projeto.

- **Negativas:**
  - Requer disciplina para manter os comentários atualizados conforme o código evolui.
