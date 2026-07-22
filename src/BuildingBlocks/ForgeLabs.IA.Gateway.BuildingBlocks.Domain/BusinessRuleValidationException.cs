namespace ForgeLabs.IA.Gateway.BuildingBlocks.Domain;

/// <summary>
/// Exceção lançada quando uma regra de negócio é violada.
/// Captura a regra quebrada e sua mensagem descritiva para facilitar o diagnóstico.
/// </summary>
public class BusinessRuleValidationException : Exception
{
    /// <summary>
    /// Regra de negócio que foi violada.
    /// </summary>
    public IBusinessRule BrokenRule { get; }

    /// <summary>
    /// Mensagem descritiva da violação da regra.
    /// </summary>
    public string Details { get; }

    /// <summary>
    /// Cria uma nova instância com base na regra de negócio violada.
    /// </summary>
    /// <param name="brokenRule">Regra de negócio que foi quebrada.</param>
    public BusinessRuleValidationException(IBusinessRule brokenRule)
        : base(brokenRule.Message)
    {
        BrokenRule = brokenRule;
        this.Details = brokenRule.Message;
    }

    /// <summary>
    /// Retorna o nome completo do tipo da regra e sua mensagem de violação.
    /// </summary>
    public override string ToString()
    {
        return $"{BrokenRule.GetType().FullName}: {BrokenRule.Message}";
    }
}
