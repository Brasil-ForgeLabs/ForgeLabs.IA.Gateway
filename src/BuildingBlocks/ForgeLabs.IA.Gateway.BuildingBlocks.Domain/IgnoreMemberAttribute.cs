namespace ForgeLabs.IA.Gateway.BuildingBlocks.Domain;

/// <summary>
/// Atributo que marca uma propriedade ou campo para ser ignorado na comparação
/// de igualdade estrutural do <see cref="ValueObject"/>.
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
public class IgnoreMemberAttribute : Attribute
{
    
}
