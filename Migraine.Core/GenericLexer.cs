using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Migraine.Core
{
    public class GenericLexer
    {
        IEnumerable<TokenDefinition> tokenDefinitions;

        public GenericLexer(IEnumerable<TokenDefinition> tokenDefinitions)
        {
            this.tokenDefinitions = tokenDefinitions;
        }

        public IEnumerable<Token> Tokenize(String input)
        {
            var tokens = new List<Token>();

            int currentPosition = 0;

            while (currentPosition < input.Length)
            {
                int previousPosition = currentPosition;

                foreach (TokenDefinition tokenDef in tokenDefinitions)
                {
                    Match match = tokenDef.Match(input.Substring(currentPosition));

                    if (match.Success && match.Index == 0)
                    {
                        tokens.Add(new Token(match.Value, tokenDef.Type));
                        currentPosition += match.Length;
                        break;
                    }
                }

                if (previousPosition == currentPosition)
                {
                    throw new Exception(String.Format("Unexpected token at position {0}", currentPosition));
                }
            }

            return tokens;
        }
    }
}
