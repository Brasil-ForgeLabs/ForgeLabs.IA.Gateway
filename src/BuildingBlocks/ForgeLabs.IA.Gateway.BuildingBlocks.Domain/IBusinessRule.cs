namespace ForgeLabs.IA.Gateway.BuildingBlocks.Domain;

/// <summary>
/// Contrato para regras de negócio validáveis no domínio.
/// Cada implementação encapsula uma invariante de negócio específica.
/// </summary>
public interface IBusinessRule
{
    /// <summary>
    /// Verifica se a regra de negócio foi violada.
    /// </summary>
    /// <returns><c>true</c> se a regra foi violada; caso contrário, <c>false</c>.</returns>
    bool IsBroken();

    /// <summary>
    /// Mensagem descritiva da violação da regra.
    /// </summary>
    string Message { get; }
}
