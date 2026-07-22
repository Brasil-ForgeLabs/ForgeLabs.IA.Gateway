# ADR-002: Tratamento de warnings como erros de compilação

## Status

Aceita

## Data

2025-07-16

## Contexto

Para manter a qualidade e a higiene do código, é desejável que warnings do compilador não sejam ignorados ao longo do tempo. Warnings acumulados tendem a mascarar problemas reais e degradar a base de código.

## Decisão

Manter a propriedade `TreatWarningsAsErrors` habilitada (`true`) no `Directory.Build.props` global, de forma que qualquer warning do compilador C# seja tratado como erro e impeça o build.

```xml
<TreatWarningsAsErrors>true</TreatWarningsAsErrors>
```

## Consequências

- **Positivas:**
  - Warnings não se acumulam — são resolvidos imediatamente.
  - O código em `main` estará sempre livre de warnings.
  - Força boas práticas como documentação XML (CS1591), nullability (CS8618) e uso correto de APIs.

- **Negativas:**
  - Qualquer warning, mesmo informativo, bloqueia o build até ser corrigido.
  - Pode aumentar o tempo de desenvolvimento em casos onde o warning não representa risco real.
  - Exige que toda a equipe esteja ciente da política para evitar frustração.
