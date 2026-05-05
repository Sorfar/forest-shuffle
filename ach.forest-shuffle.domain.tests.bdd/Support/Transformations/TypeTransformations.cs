using Ach.Forest_Shuffle.Domain.Tests.Bdd.Support.Resources;
using forest;
using System.Text;

namespace Ach.Forest_Shuffle.Domain.Tests.Bdd.Support.Transformations
{
    [Binding]
    public static class TypeTransformations
    {
        [StepArgumentTransformation]
        public static Type TransformToType(string typeName)
        {
            typeName = typeName.Trim();

            var type = GetTypeFromName(typeName) ?? GetTypeFromName(GetResxNameByValue(typeName));

            return type ?? throw new Exception($"Type {typeName} introuvable");
        }

        private static string KeepOnlyAsciiLetters(string input)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            var result = new StringBuilder();

            foreach (var c in input)
            {
                if ((c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z'))
                {
                    result.Append(c);
                }
            }

            return result.ToString();
        }

        private static Type? GetTypeFromName(string typeName)
        {
            var assembly = typeof(Forest).Assembly;
            return assembly.GetTypes().FirstOrDefault(t => t.Name.Equals(KeepOnlyAsciiLetters(typeName), StringComparison.InvariantCultureIgnoreCase));
        }

        private static string GetResxNameByValue(string value)
        {
            return LivingOrganismsNameHelper.LivingOrganismsFrToTypeDictionnary.FirstOrDefault(e => e.Key.Equals(value, StringComparison.CurrentCultureIgnoreCase)).Value ?? throw new InvalidCastException($"Nom d'espèce non trouvé : {value}.");
        }
    }
}
