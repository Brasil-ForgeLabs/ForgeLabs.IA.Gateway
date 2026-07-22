using System.Reflection;

namespace ForgeLabs.IA.Gateway.BuildingBlocks.Domain;

/// <summary>
/// Classe base para Value Objects com igualdade estrutural.
/// Compara todas as propriedades e campos públicos/privados, exceto os marcados com <see cref="IgnoreMemberAttribute"/>.
/// </summary>
public abstract class ValueObject : IEquatable<ValueObject>
    {
        private List<PropertyInfo> _properties = new();

        private List<FieldInfo> _fields = new();

        /// <summary>
        /// Verifica se dois Value Objects são iguais por igualdade estrutural.
        /// </summary>
        public static bool operator ==(ValueObject obj1, ValueObject obj2)
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
        /// Verifica se dois Value Objects são diferentes por igualdade estrutural.
        /// </summary>
        public static bool operator !=(ValueObject obj1, ValueObject obj2)
        {
            return !(obj1 == obj2);
        }

        /// <inheritdoc />
        public bool Equals(ValueObject? obj)
        {
            return Equals(obj as object);
        }

        /// <summary>
        /// Compara por igualdade estrutural: tipo idêntico, propriedades e campos com mesmos valores.
        /// </summary>
        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            return GetProperties().All(p => PropertiesAreEqual(obj, p))
                && GetFields().All(f => FieldsAreEqual(obj, f));
        }

        /// <summary>
        /// Gera o hash code combinando todas as propriedades e campos elegíveis.
        /// </summary>
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                foreach (var prop in GetProperties())
                {
                    var value = prop.GetValue(this, null) ?? 0;
                    hash = HashValue(hash, value);
                }

                foreach (var field in GetFields())
                {
                    var value = field.GetValue(this) ?? 0;
                    hash = HashValue(hash, value);
                }

                return hash;
            }
        }

        /// <summary>
        /// Valida uma regra de negócio e lança <see cref="BusinessRuleValidationException"/> caso seja violada.
        /// </summary>
        /// <param name="rule">Regra de negócio a ser validada.</param>
        /// <exception cref="BusinessRuleValidationException">Lançada quando a regra é violada.</exception>
        protected static void CheckRule(IBusinessRule rule)
        {
            if (rule.IsBroken())
            {
                throw new BusinessRuleValidationException(rule);
            }
        }

        private bool PropertiesAreEqual(object obj, PropertyInfo p)
        {
            return object.Equals(p.GetValue(this, null), p.GetValue(obj, null));
        }

        private bool FieldsAreEqual(object obj, FieldInfo f)
        {
            return object.Equals(f.GetValue(this), f.GetValue(obj));
        }

        private IEnumerable<PropertyInfo> GetProperties()
        {
            if (!this._properties.Any())
            {
                this._properties = GetType()
                    .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                    .Where(p => p.GetCustomAttribute(typeof(IgnoreMemberAttribute)) == null)
                    .ToList();

                // Not available in Core
                // !Attribute.IsDefined(p, typeof(IgnoreMemberAttribute))).ToList();
            }

            return this._properties;
        }

        private IEnumerable<FieldInfo> GetFields()
        {
            if (!this._fields.Any())
            {
                this._fields = GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                    .Where(p => p.GetCustomAttribute(typeof(IgnoreMemberAttribute)) == null)
                    .ToList();
            }

            return this._fields;
        }

        private int HashValue(int seed, object value)
        {
            var currentHash = value?.GetHashCode() ?? 0;

            return (seed * 23) + currentHash;
        }
    }
