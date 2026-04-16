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
            var assembly = typeof(Forest).Assembly;
            var type = assembly.GetTypes().FirstOrDefault(t => t.Name.Equals(KeepOnlyAsciiLetters(typeName), StringComparison.InvariantCultureIgnoreCase));
            return type == null ? throw new Exception($"Type {typeName} introuvable") : type;
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
    }
}
