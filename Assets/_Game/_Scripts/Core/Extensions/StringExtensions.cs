namespace Game.Core.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Retorna a substring entre duas marcações, se ambas existirem.
        /// </summary>
        public static string GetSubstringBetween(this string source, string startTag, string endTag)
        {
            if (string.IsNullOrEmpty(source))
                return string.Empty;

            // 1. Encontra a posição do início, ajustando para não incluir a tag
            int startIndex = source.IndexOf(startTag);
            if (startIndex == -1)
                return string.Empty;

            // Ajusta o índice para começar DEPOIS da tag de início
            startIndex += startTag.Length;

            // 2. Encontra a posição do fim, começando a busca a partir do startIndex
            int endIndex = source.IndexOf(endTag, startIndex);
            if (endIndex == -1)
                return string.Empty;

            // 3. Calcula o comprimento da substring
            int length = endIndex - startIndex;

            // 4. Retorna a substring
            return source.Substring(startIndex, length);
        }
    }
}
