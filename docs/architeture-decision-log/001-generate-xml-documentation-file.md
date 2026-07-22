# ADR-001: Geração de arquivo de documentação XML habilitada globalmente

## Status

Aceita

## Data

2025-07-16

## Contexto

O projeto utiliza um `Directory.Build.props` compartilhado por todos os projetos da solution. É necessário garantir que toda API pública esteja documentada para suportar ferramentas como IntelliSense, Swagger/OpenAPI e geradores de documentação.

## Decisão

Manter a propriedade `GenerateDocumentationFile` habilitada (`true`) no `Directory.Build.props` global, exigindo que todos os membros públicos possuam comentários XML `/// <summary>`.

```xml
<GenerateDocumentationFile>true</GenerateDocumentationFile>
```

## Consequências

- **Positivas:**
  - Toda API pública terá documentação acessível via IntelliSense em IDEs (Visual Studio, Rider, VS Code).
  - Ferramentas de documentação automática (Swagger, DocFX) podem consumir o XML gerado.
  - Garante consistência e qualidade da documentação do código.

- **Negativas:**
  - Todo membro público sem comentário XML gerará o warning CS1591.
  - Combinado com `TreatWarningsAsErrors`, membros não documentados causam falha de build (ver ADR-002).
  - Maior esforço ao criar novas classes, interfaces e membros públicos.
