namespace ForgeLabs.IA.Gateway.BuildingBlocks.Domain;

/// <summary>
/// Classe base abstrata para identificadores fortemente tipados (Strongly Typed Ids).
/// Encapsula um <see cref="Guid"/> como valor de identidade, garantindo segurança de tipo
/// e prevenindo o uso acidental de IDs de entidades diferentes.
/// </summary>
/// <remarks>
/// <para>
/// Esta classe implementa o padrão <i>Strongly Typed Id</i>, que substitui o uso direto de
/// tipos primitivos (como <see cref="Guid"/>) por tipos concretos e específicos para cada entidade.
/// Isso elimina erros comuns como a troca acidental de IDs entre entidades distintas em tempo de compilação.
/// </para>
/// <para>
/// Classes derivadas devem herdar de <see cref="TypedIdValueBase"/> e expor um construtor que
/// receba um <see cref="Guid"/>, delegando à classe base.
/// </para>
/// <para>
/// <b>Exemplo de uso:</b>
/// <code>
/// public class OrderId : TypedIdValueBase
/// {
///     public OrderId(Guid value) : base(value) { }
/// }
/// </code>
/// </para>
/// </remarks>
/// <example>
/// <code>
/// var orderId = new OrderId(Guid.NewGuid());
/// var sameOrderId = new OrderId(orderId.Value);
/// 
/// bool areEqual = orderId == sameOrderId; // true
/// </code>
/// </example>
public abstract class TypedIdValueBase : IEquatable<TypedIdValueBase>
{
    /// <summary>
    /// Obtém o valor <see cref="Guid"/> subjacente do identificador.
    /// </summary>
    public Guid Value { get; }

    /// <summary>
    /// Inicializa uma nova instância de <see cref="TypedIdValueBase"/> com o valor de <see cref="Guid"/> especificado.
    /// </summary>
    /// <param name="value">O valor <see cref="Guid"/> do identificador. Não pode ser <see cref="Guid.Empty"/>.</param>
    /// <exception cref="InvalidOperationException">
    /// Lançada quando <paramref name="value"/> é <see cref="Guid.Empty"/>.
    /// </exception>
    protected TypedIdValueBase(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new InvalidOperationException("Id value cannot be empty!");
        }

        Value = value;
    }

    /// <summary>
    /// Determina se o objeto especificado é igual à instância atual,
    /// comparando pelo valor do <see cref="Guid"/> subjacente.
    /// </summary>
    /// <param name="obj">O objeto a ser comparado com a instância atual.</param>
    /// <returns>
    /// <c>true</c> se <paramref name="obj"/> for uma instância de <see cref="TypedIdValueBase"/>
    /// com o mesmo <see cref="Value"/>; caso contrário, <c>false</c>.
    /// </returns>
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj))
        {
            return false;
        }

        return obj is TypedIdValueBase other && Equals(other);
    }

    /// <summary>
    /// Retorna o código hash para esta instância, baseado no valor do <see cref="Guid"/> subjacente.
    /// </summary>
    /// <returns>Um código hash <see cref="int"/> derivado de <see cref="Value"/>.</returns>
    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    /// <summary>
    /// Determina se a instância de <see cref="TypedIdValueBase"/> especificada é igual à instância atual,
    /// comparando pelo valor do <see cref="Guid"/> subjacente.
    /// </summary>
    /// <param name="other">A instância de <see cref="TypedIdValueBase"/> a ser comparada.</param>
    /// <returns>
    /// <c>true</c> se <paramref name="other"/> possuir o mesmo <see cref="Value"/>; caso contrário, <c>false</c>.
    /// </returns>
    public bool Equals(TypedIdValueBase? other)
    {
        return this.Value == other?.Value;
    }

    /// <summary>
    /// Determina se duas instâncias de <see cref="TypedIdValueBase"/> são iguais.
    /// </summary>
    /// <param name="obj1">A primeira instância a comparar.</param>
    /// <param name="obj2">A segunda instância a comparar.</param>
    /// <returns>
    /// <c>true</c> se ambas as instâncias forem <c>null</c> ou possuírem o mesmo <see cref="Value"/>;
    /// caso contrário, <c>false</c>.
    /// </returns>
    public static bool operator ==(TypedIdValueBase obj1, TypedIdValueBase obj2)
    {
        if (object.Equals(obj1, null))
        {
            if (object.Equals(obj2, null))
            {
                return true;
            }

            return false;
        }

        return obj1.Equals(obj2);
    }

    /// <summary>
    /// Determina se duas instâncias de <see cref="TypedIdValueBase"/> são diferentes.
    /// </summary>
    /// <param name="x">A primeira instância a comparar.</param>
    /// <param name="y">A segunda instância a comparar.</param>
    /// <returns>
    /// <c>true</c> se as instâncias possuírem valores de <see cref="Value"/> diferentes;
    /// caso contrário, <c>false</c>.
    /// </returns>
    public static bool operator !=(TypedIdValueBase x, TypedIdValueBase y)
    {
        return !(x == y);
    }
}
