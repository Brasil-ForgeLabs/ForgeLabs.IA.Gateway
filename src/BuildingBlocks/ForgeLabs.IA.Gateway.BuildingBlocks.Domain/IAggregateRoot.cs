namespace ForgeLabs.IA.Gateway.BuildingBlocks.Domain;

/// <summary>
/// Interface marcadora que identifica uma entidade como raiz de agregado (Aggregate Root).
/// Apenas raízes de agregado devem ser persistidas e referenciadas diretamente por outros agregados.
/// </summary>
public interface IAggregateRoot
{
    
}
